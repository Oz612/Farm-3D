using System;
using UnityEngine;

public class Shopper : MonoBehaviour
{
    // estaran todos los objetos que se recogen
    public Transform[] CollectableTs;
    private int _currentCollectableTIndex;
    public bool IsAlReadyBuy; // ya pago ??
    public bool IsReadyToPay; // Listo para pagar?
    public bool CanGoHome;  //Ir a casa ?
    public int AllowedId;

    //Detecta si esta en la zona de la mesa de frutas para vender
    private void OnTriggerStay(Collider other)
    {
        if (CanGoHome) return;

        if (other.CompareTag("FruitTable"))
        {
            //ubicacion de los objetocos canasta de fruta para ventas
            ProductBaskets productBastkets = other.GetComponent<ProductBaskets>();
            Collectable collectable = productBastkets.GetAvailablCollectable();
            if (collectable == null) 
            {
                CanGoHome = true;
                return;
            }
            if (collectable.Id != AllowedId) return;
            if (collectable.IsReadyToBuy == false) return; 
            if (collectable.HasOwner == true) return; 
            if (_currentCollectableTIndex == CollectableTs.Length) return;
            collectable.SetLocalPositionToParent(CollectableTs[_currentCollectableTIndex]);
            collectable.HasOwner = true;
            collectable.IsReadyToBuy = true;
            IsReadyToPay = true; 
            _currentCollectableTIndex++;
        }
    }

    private Collectable GetAvailableCollectable()
    {
        for (int i = CollectableTs.Length - 1; i >= 0; i--)
        {
            if (CollectableTs[i].childCount > 0)
            {
                return CollectableTs[i].GetComponentInChildren<Collectable>();
            }
        }

        return null;
    }

    public bool HasProductsIA()
    {
        return GetAvailableCollectable() != null;
    }

    public int GetAvailableCollectableCount()
    {
        int count = 0;
        for (int i = 0; i < CollectableTs.Length; i++)
        {
            if (CollectableTs[i].childCount > 0)
            {
                count++;
            }
        }

        return count;
    }

}


