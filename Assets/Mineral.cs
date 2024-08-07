using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    private bool hoveredOver = false;
    private LayerMask mask;
    private SpriteRenderer sprite;
    private Color originalColor;
    private MineralType mineral;
    private float currentHitPoints;

    private static TMP_Text selectedMineralText = null;
    private static InventoryManager inventory = null;
    
    [SerializeField] SpriteRenderer damageOverlay;
    [SerializeField] List<Sprite> damageStages;
    private AudioSource breakSound;
    [SerializeField] private float destroyAfterSeconds = 30.0f;
    private BoxCollider2D boxCollider;

    public MineralType GetMineralType()
    {
        return mineral;
    }

    public void ApplyDamage(float damage)
    {
        if (!mineral.Mineable)
        {
            return;
        }

        currentHitPoints -= damage;
        SetDamageStage();
        if (currentHitPoints <= 0) {
            Break();
        }
    }

    private void SetDamageStage()
    {
        float percentageHpRemaining = GetCurrentHitPoints() / mineral.HitPoints;

        // Mineral is either undamaged or about to be destroyed, return.
        if (percentageHpRemaining >= 1 || percentageHpRemaining <= 0) {
            return;
        }

        int stage = (int)(percentageHpRemaining * damageStages.Count);
        damageOverlay.sprite = damageStages[stage];
    }

    public float GetCurrentHitPoints()
    {
        return currentHitPoints;
    }

    void PickRandomMineral()
    {
        // Learn what kind of mineral I am...
        mineral = MineralDistribution.GetRandomMineral(transform.position.y);

        // A valid mineral was not found at this particular Y-level, fill in some defaults and abort.
        if (mineral == null) {
            sprite.color = Color.white;
            originalColor = Color.white;
            return;
        }

        originalColor = mineral.Color;
        sprite.color = originalColor;
        currentHitPoints = mineral.HitPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (selectedMineralText == null) {
            GameObject textObj = GameObject.FindWithTag("SelectedMineralText");
            selectedMineralText = textObj.GetComponent<TMP_Text>();
            selectedMineralText.text = "";
        }

        if (inventory == null) {
            GameObject invObj = GameObject.FindWithTag("InventoryManager");
            inventory = invObj.GetComponent<InventoryManager>();
        }

        mask = LayerMask.GetMask("Terrain");
        sprite = GetComponent<SpriteRenderer>();
        breakSound = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();

        PickRandomMineral();

        if (mineral == null)
        {
            return;
        }

        if (!mineral.Collision)
        {
            boxCollider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (!hoveredOver) {
            hoveredOver = true;
            sprite.color = originalColor + new Color(0.1f, 0.1f, 0.1f, 1f);
            selectedMineralText.enabled = true;
            selectedMineralText.text = mineral?.Name ?? "Unknown";
        }
    }

    void OnMouseExit()
    {
        if (hoveredOver) {
            hoveredOver = false;
            sprite.color = originalColor;
            selectedMineralText.enabled = false;
        }
    }

    private void Break()
    {
        // inventory.AddMineral(mineral, 1);
        if (mineral != null && mineral == MineralType.Gold) {
            inventory.AddGold(1);
        }

        breakSound.Play();
        WorldManager.GetInstance().MarkDestroyed(transform.position);
        sprite.enabled = false;
        damageOverlay.enabled = false;
        boxCollider.enabled = false;

        Destroy(gameObject, destroyAfterSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Vehicle vehicle = collider.gameObject.GetComponent<Vehicle>();
            
        }
    }
}
