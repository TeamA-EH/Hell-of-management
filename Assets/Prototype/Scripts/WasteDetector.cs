using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class WasteDetector : MonoBehaviour
{

    public event Action<GameObject> OnWasteDetected;

    #region UnityCallbacks
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<WasteController>() && other.gameObject.GetComponent<ThrowableItemStates>().state == ThrowableItemStates.States.Floating)
        {
            OnWasteDetected?.Invoke(other.gameObject);
        }
    }
    #endregion
}