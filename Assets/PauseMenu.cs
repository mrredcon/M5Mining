using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Canvas uiCanvas;
    private CanvasGroup canvasGroup;

    [SerializeField] private SaveMenu saveMenu;
    [SerializeField] private OptionsMenu optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        uiCanvas.enabled = false;
        canvasGroup.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        canvasGroup.interactable = !canvasGroup.interactable;
        uiCanvas.enabled = !uiCanvas.enabled;
        UIManager.GetInstance().SetUIOpen(uiCanvas.enabled);
        TimeManager.GetInstance().TogglePause();
    }

    public void OpenSaveUI()
    {
        saveMenu.Show();
    }

    public void OpenOptionsUI()
    {
        optionsMenu.Show();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
