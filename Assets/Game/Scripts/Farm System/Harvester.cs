using System;
using Unity.VisualScripting;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    // estaran todos los objetos que se recogen
    public Transform[] CollectableTs;
    private int _currentCollectableTIndex;
    private void OnTriggerEnter(Collider other)
    {
        //al recoger en que lugar se ubicara
        if (other.CompareTag("Collectable"))
        {
            //ubicacion de los objetocos recogidos
            if (_currentCollectableTIndex == CollectableTs.Length) return;
            Collectable collectable = other.GetComponent<Collectable>();
            if (collectable.IsReadyToBuy == true) { return; }
            if (collectable.HasOwner == true) { return; }
            collectable.SetLocalPositionToParent(CollectableTs[_currentCollectableTIndex]);
            collectable.HasOwner = true;
            collectable.IsReadyToBuy = false;
            _currentCollectableTIndex++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FruitTable"))
        {
            //ubicacion de los objetocos canasta de fruta para ventas
            ProductBaskets productBastkets = other.GetComponent<ProductBaskets>();
            Collectable collectable = GetAvailableCollectable();
            if (collectable == null) return;

            bool canPlace = productBastkets.PlaceProduct(collectable);
            if (canPlace)
            {
                _currentCollectableTIndex--;
            }
        }
    }

    //Comprobacion de que si tiene collectable
    private Collectable GetAvailableCollectable()
    {
        for (int i = CollectableTs.Length -1; i >= 0; i--)
        {
            if (CollectableTs[i].childCount > 0)
            {
               return CollectableTs[i].GetComponentInChildren<Collectable>();
            }
        }

        return null; 
    }

    public bool HasProducts()
    {
        return GetAvailableCollectable() != null;
    }
}
