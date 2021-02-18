using UnityEngine;
using System;

public class WasteElaborator : MonoBehaviour
{
    [SerializeField] GameObject trashBagAsset;

    /// <summary>
    /// Called when the elaborator computes the output
    /// </summary>
    public event Action<GameObject> OnTrashBagCreated;

    public void CreateTrashbag(Vector3 direction, float ditance = 3)
    {
        var bag = Instantiate(trashBagAsset);
        bag.transform.position = gameObject.transform.position + (direction * 3) + (Vector3.up * 3);

        OnTrashBagCreated?.Invoke(bag);
    }
}
