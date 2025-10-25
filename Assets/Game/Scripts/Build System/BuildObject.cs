using TMPro;
using UnityEngine;

public class BuildObject : MonoBehaviour
{
    public GameObject ObjetcPrefab;
    public Transform ObjectT;
    public TextMeshPro PriceTMP;
    public int Price;

    
    //Mostrar el precio en la pantalla
    private void Start()
    {
        PriceTMP.text = Price.ToString();
    }
    [ContextMenu("Build")]
    
    //contruir en la posicion asigna y desactiva el valor del precio
    public void Build()
    {
        Instantiate(ObjetcPrefab, ObjectT.position,ObjectT.rotation);
        gameObject.SetActive(false);
    }

}
