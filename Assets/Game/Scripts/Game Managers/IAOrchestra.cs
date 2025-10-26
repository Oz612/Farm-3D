using System;
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

    //Queue de personajes IA
    public Transform[] TargetTsQueue;
    public List<CharacterIA> CharacterIAsQueue;

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

        //Tranyectoria de la IA
        for (int i = 0; i < CharacterIAs.Count; i++)
        {
            if (CharacterIAs[i].Shopper.IsAlReadyBuy) 
            {
                //Codigo ir a casa
                RemoveElementQueue(CharacterIAs[i]);
            }
            else if (CharacterIAs[i].Shopper.IsReadyToPay)
            {
                // Ir a caja
                AddElementQueue(CharacterIAs[i]);
            }
            else if (CharacterIAs[i].Shopper.CanGoHome)
            {
                //Codigo ir a casa
                GoHome(CharacterIAs[i]);
            }
        }
    }

    private void AddElementQueue(CharacterIA characterIA)
    {
        if (CharacterIAsQueue.Contains(characterIA) == true) return;

        CharacterIAsQueue.Add(characterIA);
        OrganizeQueue();
    }
    private void RemoveElementQueue(CharacterIA characterIA)
    {
        if (CharacterIAsQueue.Contains(characterIA) == false) return;
        CharacterIAsQueue.Remove(characterIA);
        GoHome(characterIA);
        OrganizeQueue();
    }

    private void OrganizeQueue()
    {
        for (int i = 0; i < CharacterIAsQueue.Count; i++)
        {
            CharacterIAsQueue[i].MoveToTransform(TargetTsQueue[i]);
        }
    }

    //Destruircion de IA
    private void GoHome(CharacterIA characterIA)
    {
        if(CharacterIAs.Contains(characterIA) == false) return;
        CharacterIAs.Remove(characterIA);
        characterIA.MoveToTransform(SpawnT);
        Destroy(characterIA.transform.parent.gameObject, 15);
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

    //Posicion aleateoria de los IA de ir a recoger los productos
    private void ConfigureIA(CharacterIA characterIA)
    {
        //int rdnPos = UnityEngine.Random.Range(0, ProductBaskets.Length);
        //ProductBaskets baskets = ProductBaskets[rdnPos];
        //characterIA.MoveToTransform(baskets.GoalT);
        characterIA.MoveToTransform(_productBasketsAvailable.GoalT);
        characterIA.Shopper.AllowedId = _productBasketsAvailable.AllowedId;
    }
}
