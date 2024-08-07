using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float toolPivotMoveSpeed = 0.25f;
    [SerializeField] private float cameraZoomSpeed = 0.25f;

    [Header("References")]
    [SerializeField] private Vehicle vehicle;
    [SerializeField] private Tool tool;
    [SerializeField] private Drill drill;
    [SerializeField] private CustomPivot toolPivot;
    [SerializeField] private Blimp blimp;
    [SerializeField] private PauseMenu pauseMenu;

    private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Terrain");
        StartCoroutine(PointToolAtCursorRoutine());
    }

    private bool ControlsDisabled()
    {
        if (TimeManager.GetInstance().IsPaused() || UIManager.GetInstance().IsUIOpen())
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene("MainMenu");
            //TimeManager.GetInstance().TogglePause();
            pauseMenu.Toggle();
        }

        if (ControlsDisabled())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(JumpRoutine());
        }

        float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scrollWheel != 0)
        {
            if (Input.GetMouseButton(1)) {
                toolPivot.MoveAlongX(toolPivotMoveSpeed * scrollWheel);
            } else {
                Camera.main.orthographicSize += cameraZoomSpeed * (scrollWheel * -1);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            drill.SpinUp();
        } else if (Input.GetMouseButtonUp(0)) {
            drill.PowerDown();
        }

        if (Input.GetKeyDown(KeyCode.Home)) {
            blimp.MoveTowardsRig();
        }

        // DEBUG
        // if (Input.GetKeyDown(KeyCode.Delete))
        // {
        //     WorldManager.GetInstance().WriteWorldData();
        // }
    }

    IEnumerator JumpRoutine()
    {
        yield return new WaitForFixedUpdate();
        vehicle.Jump();
    }

    void FixedUpdate()
    {
        if (ControlsDisabled())
        {
            return;
        }

        if (Input.GetKey(KeyCode.A)) {
            vehicle.Move(new Vector2(-1, 0));
        } else if (Input.GetKey(KeyCode.D)) {
            vehicle.Move(new Vector2(1, 0));
        }
    }

    private IEnumerator PointToolAtCursorRoutine()
    {
        while (true)
        {
            if (Input.GetMouseButton(1) && !ControlsDisabled())
            {
                Vector3 mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tool.PointAt(mouseCoords);
            }
            
            yield return null;
        }
    }
}
