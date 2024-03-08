using TMPro;
using UnityEngine;

public class CarrotDisplay : MonoBehaviour
{
    public TMP_Text CarrotCount;

    private void Start()
    {
        CarrotManager.instance.SetCarrotDisplay(CarrotCount);
    }
}