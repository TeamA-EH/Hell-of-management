using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class TrashbagDetector : MonoBehaviour
{
    /// <summary>
    /// Called when a trash bag is detected
    /// </summary>
    public event Action<GameObject> OnTrashbagDetected;

    #region UnityCallbacks
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<TrashbagController>() || other.gameObject.GetComponent<ThrowableItemStates>().state == ThrowableItemStates.States.Floating)
        {
            OnTrashbagDetected?.Invoke(other.gameObject);
        }
    }
    #endregion
}
