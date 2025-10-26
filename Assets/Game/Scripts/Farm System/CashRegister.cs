using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public int HereIsPlayer;     // 0 no , 1 yes 
    public int HereIsShopper;    // 0 no , 1 yes 
    public Shopper Shopper;    // 0 no , 1 yes
    public Transform TargetT;

    //Condicion para cobrar 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HereIsPlayer = 1;
        }
        if (other.CompareTag("IA"))
        {
            HereIsShopper = 1;
            Shopper = other.GetComponent<Shopper>();
        }
    }

    //Verificacion si se puede cobrar 
    private void Update()
    {
        if (HereIsPlayer == 1 && HereIsShopper == 1)
        {
            if (Shopper == null) return;
            if (Shopper.IsAlReadyBuy == true) return;
            int purchaseVale = Shopper.GetAvailableCollectableCount() * 20;
            Shopper.IsAlReadyBuy = true;
            Shopper.CanGoHome = true;
            GameEconomy.Coins += purchaseVale;
        }
    }
    //Sale si ya compro de la fina 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HereIsPlayer = 0;
        }
        if (other.CompareTag("IA"))
        {
            HereIsShopper = 0;
            Shopper = null;
        }
    }
}
