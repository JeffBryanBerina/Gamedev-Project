
using UnityEngine;
using TMPro;


public class playerCollection : MonoBehaviour
{

    public static playerCollection instance;

    private int coint;
    [SerializeField] private TMP_Text cointDisplay;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void OnGUI()
    {
        cointDisplay.text=coint.ToString(); 
    }
    public void ChangeCoins(int amount)
    {
        coint += amount;

    }

}
