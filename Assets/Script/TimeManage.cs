using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import the SceneManager namespace

public class TimeManage : MonoBehaviour
{
    public Image linearTimer;
    float time_remaining;
    public float time_max = 5.0f;

    // Reference to the CarrotManager script
    public CarrotManager carrotManager;

    void Start()
    {
        time_remaining = time_max;
    }

    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            linearTimer.fillAmount = time_remaining / time_max;
        }
        else
        {
            // When the timer reaches 0, decrement the carrot count
            if (carrotManager != null)
            {
                carrotManager.ChangCarrots(-1);

                // If the carrot count is 0, load Scene 4
                if (carrotManager.carrots < 0)
                {
                    // Load Scene 4 (you need to implement this part)
                    SceneManager.LoadScene(0);
                }

                // Reset the timer
                time_remaining = time_max;
            }
        }
    }
}
