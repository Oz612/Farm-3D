using System;
using System.Threading;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject CollectablePrefab;
    public Transform[] CollectableTs;
    public GameObject[] GrowthStagesGOs;
    public float MaxGrowthTime = 5f;
    private float _maxTimePerStage;
    private float _timerPerStage;
    private int _currentGrowthStageIndex;
    private bool _isAreadyGrowth;

    private void Start()
    {
        ExtractPlant();
    }

    //Distribucion de tiempo de crecimiento 
    private void Update()
    {
        if (_isAreadyGrowth)
        {
            CheckCollectableAvailable();
            return;
        }


        _timerPerStage += Time.deltaTime; 
        if (_timerPerStage > _maxTimePerStage)
        {
            UpdateStages();
            _timerPerStage = 0;
            _currentGrowthStageIndex++;
        }
    }
    //volver a crecer despues de haber recogido
    private void CheckCollectableAvailable()
    {
        bool hasCollectable = false;
        for (int i = 0;i <CollectableTs.Length; i++)
        {
            hasCollectable = CollectableTs[i].childCount > 0;
            if (hasCollectable)
            {
                return;
            }
        }

        ExtractPlant();
    }
    // reinicio del estado de la planta
    private void ExtractPlant()
    {
        for (int i = 0; i < GrowthStagesGOs.Length; i++)
        {
            GrowthStagesGOs[i].gameObject.SetActive(false);
        }
        _currentGrowthStageIndex = 0;
        _timerPerStage = 0;
        _isAreadyGrowth = false;
        ; _maxTimePerStage = MaxGrowthTime / GrowthStagesGOs.Length;
    }

    //Etapa de crecimiento de planta
    private void UpdateStages()
    {
        if(_currentGrowthStageIndex == GrowthStagesGOs.Length)
        {
            CreateCollectable();
            return;
        }

        if (_currentGrowthStageIndex > 0)
        {
            GrowthStagesGOs[_currentGrowthStageIndex - 1].SetActive(false);
        }
        GrowthStagesGOs[_currentGrowthStageIndex].SetActive(true);
    }

    // creacion de posicion de tomates 
    private void CreateCollectable()
    {
        for(int i = 0; i < CollectableTs.Length; i++)
        {
           GameObject collectableGO = Instantiate(CollectablePrefab);
            Collectable collectable = collectableGO.GetComponent<Collectable>();
            collectable.SetLocalPositionToParent(CollectableTs[i]);
        }

        //verificacion que ya crecio la planta 
        _isAreadyGrowth = true;
    }
}

