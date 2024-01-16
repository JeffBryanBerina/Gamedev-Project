
using UnityEngine;

public class foodScript : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;

    private playerCollection PlayerCollection;

    private void Start()
    {
        PlayerCollection = playerCollection.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            PlayerCollection.ChangeCoins(value);
            Destroy(gameObject);
        }
    }
}
