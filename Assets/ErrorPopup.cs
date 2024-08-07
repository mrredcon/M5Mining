using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorPopup : MonoBehaviour
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text errorText;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        Acknowledge();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Acknowledge()
    {
        canvas.enabled = false;
        canvasGroup.interactable = false;
    }

    public void ShowError(string message)
    {
        errorText.text = message;
        canvas.enabled = true;
        canvasGroup.interactable = true;
    }
}
