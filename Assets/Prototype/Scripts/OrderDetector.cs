using UnityEngine;
using System;

/// <summary>
/// Detecta un GAMEOBJECT che possiede la descrizione di tutti gli ingredienti che sono stati utilizzati per crearlo
/// </summary>
[RequireComponent(typeof(Collider))]
public class OrderDetector : MonoBehaviour
{
    public event Action<GameObject> OnOrderDetected;

    #region UnityCallabacks
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IngredientsDescription>() && other.gameObject.GetComponent<ThrowableItemStates>().state == ThrowableItemStates.States.Floating)
        {
            OnOrderDetected?.Invoke(other.gameObject);
        }
    }
    #endregion
}
