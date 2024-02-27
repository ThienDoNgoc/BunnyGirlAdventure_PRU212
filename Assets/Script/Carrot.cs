using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;
    private CarrotManager carrotManager;
    private void Start()
    {
        if (CarrotManager.instance != null)
        {
            carrotManager = CarrotManager.instance;
        }
        else
        {
            Debug.LogError("CarrotManager instance is null.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            carrotManager.ChangCarrots(value);
            Destroy(gameObject);
        }
    }
}
