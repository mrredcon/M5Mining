using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text slot1Text;
    [SerializeField] private TMP_Text slot2Text;
    [SerializeField] private TMP_Text slot3Text;
    [SerializeField] private SaveManager saveManager;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        canvas.enabled = false;
        canvasGroup.interactable = false;

        RefreshLabels();
    }

    public void RefreshLabels()
    {
        slot1Text.text = saveManager.GetLabel(1);
        slot2Text.text = saveManager.GetLabel(2);
        slot3Text.text = saveManager.GetLabel(3);
    }

    public void SaveGame(int slot)
    {
        saveManager.SaveGame(slot);
        RefreshLabels();
    }

    public void Show()
    {
        canvasGroup.interactable = true;
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
        canvasGroup.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
