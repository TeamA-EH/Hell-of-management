using UnityEngine;
using System;

public class TrashbagDestroyer : MonoBehaviour
{
    /// <summary>
    /// Called when a trashbag is destroyed
    /// </summary>
    public event Action OnTrashbagDestroyed;

    public void DestroyTrashbag(GameObject trashbag)
    {
        Destroy(trashbag);

        OnTrashbagDestroyed?.Invoke();
    }
}