using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProductBaskets : MonoBehaviour
{
    public Transform[] ProductTs;
    public int AllowedId;
    public Transform GoalT;

    public bool PlaceProduct(Collectable collectable)
    {
        if (collectable == null) return false;
        if (collectable.Id != AllowedId) return false;

        Transform availableT = GetAvailableT();
        if (availableT == null) return false;

        collectable.SetLocalPositionToParent(availableT);
        collectable.IsReadyToBuy = true;
        collectable.HasOwner = false;
        return true;
    }

    public Collectable GetAvailablCollectable()
    {
        for (int i = 0; i < ProductTs.Length; i++)
        {
            if (ProductTs[i].childCount > 0)
            {
                return ProductTs[i].GetComponentInChildren<Collectable>();
            }
        }
        return null;
    }
    private Transform GetAvailableT()
    {
        for (int i = 0; i < ProductTs.Length; i++)
        {
            if (ProductTs[i].childCount == 0)
            {
                return ProductTs[i];
            }
        }
        return null;
    }
    public bool HasCollectables()
    {
        return GetAvailablCollectable() != null;    
    }
}
