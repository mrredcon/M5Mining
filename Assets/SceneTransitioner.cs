using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Canvas canvas;
    [SerializeField] float fadeSpeed = 5f;
    [SerializeField] float rotationDegreesPerSecond = 60f;
    [SerializeField] TMP_Dropdown saveDropdown;

    void Awake()
    {
        StartCoroutine(FadeOutRoutine());
    }

    public IEnumerator FadeOutRoutine()
    {
        var sizeDeltaX = 1.0f * Screen.width / canvas.scaleFactor;
        var sizeDeltaY = 1.0f * Screen.height / canvas.scaleFactor;
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDeltaX);
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDeltaY);

        img.color = Color.black;

        float timer = 0;
        while (timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            //img.color = new Color(0, 0, 0, 1 - (timer / fadeSpeed));
            img.rectTransform.Rotate(0, 0, Time.deltaTime * rotationDegreesPerSecond);

            sizeDeltaX = (1 - timer / fadeSpeed) * Screen.width / canvas.scaleFactor;
            sizeDeltaY = (1 - timer / fadeSpeed) * Screen.height / canvas.scaleFactor;

            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDeltaX);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDeltaY);
            yield return null;
        }

        img.color = Color.clear;
    }

    public IEnumerator FadeInRoutine()
    {
        var sizeDeltaX = 0f * Screen.width / canvas.scaleFactor;
        var sizeDeltaY = 0f * Screen.height / canvas.scaleFactor;
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDeltaX);
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDeltaY);

        img.color = Color.black;

        float timer = 0;
        while (timer < fadeSpeed)
        {
            img.rectTransform.Rotate(0, 0, Time.deltaTime * rotationDegreesPerSecond);

            sizeDeltaX = timer / fadeSpeed * Screen.width / canvas.scaleFactor;
            sizeDeltaY = timer / fadeSpeed * Screen.height / canvas.scaleFactor;

            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDeltaX);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDeltaY);
            timer += Time.deltaTime;
            yield return null;
        }

        img.rectTransform.Rotate(0, 0, 0, Space.World);
    }

    private IEnumerator FadeInAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeInRoutine());
        yield return new WaitForSeconds(fadeSpeed);
        
        GameInitialization.LoadSaveSlot = saveDropdown.value;
        SceneManager.LoadScene(sceneName);
    }

    public void TransitionTo(string sceneName)
    {
        StartCoroutine(FadeInAndLoadScene(sceneName));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
