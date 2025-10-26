using UnityEngine;

public class Builder : MonoBehaviour
{
    //Para contruis los objetos de la escena 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildObject"))
        {
            BuildObject buildObject = other.GetComponent<BuildObject>();
            if (buildObject.Price <= GameEconomy.Coins)
            {
                buildObject.Build();
                GameEconomy.Coins -= buildObject.Price;
            }



        }
    }
}
