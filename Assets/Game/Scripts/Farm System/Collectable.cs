using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool HasOwner = false;
    public bool IsReadyToBuy = false;
    public int Id = 0; // 0 Tomate : 1 Corn

    public void SetLocalPositionToParent(Transform parentT)
    {
        transform.SetParent(parentT);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
