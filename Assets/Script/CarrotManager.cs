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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnGUI()
    {
        if (carrotDisplay != null)
        {
            carrotDisplay.text = carrots.ToString();
        }
    }
    public void ChangCarrots(int amount)
    {
        carrots += amount;
    }
    public void SetCarrotDisplay(TMP_Text newDisplay)
    {
        carrotDisplay = newDisplay;
    }
    public void Reset()
    {
        carrots = 5;
        if (carrotDisplay != null)
        {
            carrotDisplay.text = carrots.ToString();
        }
    }
}
