using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManage : MonoBehaviour
{
    public static TimeManage instance;
    public Image linearTimer;
    float time_remaining;
    public float time_max = 5.0f;

    // Reference to the CarrotManager script
    public CarrotManager carrotManager;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        time_remaining = time_max;
    }
    public void SetLinearTimer(Image newTimer)
    {
        linearTimer = newTimer;
    }

    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            if (linearTimer != null)
            {
                linearTimer.fillAmount = time_remaining / time_max;
            }
        }
        else
        {
            // When the timer reaches 0, decrement the carrot count
            if (carrotManager != null)
            {
                carrotManager.ChangCarrots(-1);

                // If the carrot count is 0, load Scene 4
                if (carrotManager.carrots == -1)
                {
                    SceneManager.LoadScene(5);
                }

                // Reset the timer
                time_remaining = time_max;
            }
        }
    }
    public void Reset()
    {
        time_remaining = time_max;
        if (linearTimer != null)
        {
            linearTimer.fillAmount = 1;
        }
    }
}
