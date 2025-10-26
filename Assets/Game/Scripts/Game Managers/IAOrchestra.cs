using System.Collections.Generic;
using UnityEngine;

public class IAOrchestra : MonoBehaviour
{
    public GameObject IAPrefab;
    public Transform SpawnT;
    public float CreationRepeatRate;
    private float _CreationTimer;
    public ProductBaskets[] ProductBaskets;
    private ProductBaskets _productBasketsAvailable;

    public List<CharacterIA> CharacterIAs;
    public CashRegister CashRegister;   

    private void Start()
    {
        CharacterIAs = new();
    }


    //Cada cuanto tiempo se crea un personaje IA (comprador)
    private void Update()
    {
        _CreationTimer += Time.deltaTime;
        if (_CreationTimer > CreationRepeatRate)
        {
            if (IsAvailableProducts())
            {
                GameObject IAGO = Instantiate(IAPrefab, SpawnT.position, Quaternion.identity);
                CharacterIA characterIA = IAGO.GetComponentInChildren<CharacterIA>();
                CharacterIAs.Add(characterIA);
                ConfigureIA(characterIA);
                _CreationTimer = 0;
            }
        }
        //Recoger cada IA
        for (int i = 0; i < CharacterIAs.Count; i++)
        {
            if (CharacterIAs[i].Shopper.CanGoHome)
            {
                //Codigo ir a casa
                CharacterIAs[i].MoveToTransform(SpawnT);
            }
            else if (CharacterIAs[i].Shopper.IsReadyToPay)
            {
                // Ir a caja
                CharacterIAs[i].MoveToTransform(CashRegister.TargetT);
            }
            else if (CharacterIAs[i].Shopper.IsAlReadyBuy)
            {
                //Codigo ir a casa
            }
        }
    }
    private bool IsAvailableProducts()
    {
       for (int i = 0; i < ProductBaskets.Length; i++)
        {
            if (ProductBaskets[i].HasCollectables())
            {
                _productBasketsAvailable = ProductBaskets[i];
                return true;
            }
        }
        return false;
    }

    //Posicion aleateoria de los IA
    private void ConfigureIA(CharacterIA characterIA)
    {
        characterIA.MoveToTransform(_productBasketsAvailable.GoalT);
        characterIA.Shopper.AllowedId = _productBasketsAvailable.AllowedId;
    }
}
