using TMPro;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    public static CarrotManager instance;
    public int carrots;
    [SerializeField] private TMP_Text carrotDisplay;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    private void OnGUI()
    {
        carrotDisplay.text = carrots.ToString();
    }
    public void ChangCarrots(int amount)
    {
        carrots += amount;
    }
}
