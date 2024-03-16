using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearTimer : MonoBehaviour
{
    public Image NewLinearTimer;

    private void Start()
    {
        if (TimeManage.instance != null && NewLinearTimer != null)
        {
            TimeManage.instance.SetLinearTimer(NewLinearTimer);
        }
    }
}
