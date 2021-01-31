using UnityEngine;
using System;

/// <summary>
/// Rileva l'ingrediente che entra nel collider
/// </summary>
[RequireComponent(typeof(Collider))]
public class IngredientDetector : MonoBehaviour
{
    /// <summary>
    /// Called when an ingredient is detected
    /// </summary>
    public event Action<GameObject> OnIngredientDetected;

    #region UnityCallbacks
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IngredientDescription>())
        {
            OnIngredientDetected?.Invoke(other.gameObject);
        }
    }
    #endregion
}
