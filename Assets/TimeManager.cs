using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    private bool isPaused;

    void Awake()
    {
        instance = this;
    }

    public static TimeManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void Resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        } else {
            Pause();
        }
    }
}
