﻿using UnityEngine;

/// <summary>
/// Permette di creare un ordine se il GameObject a cui e' attaccato viene cliccato con il mouse
/// </summary>
public class OrderIteraction : MonoBehaviour
{
    #region UnityCallbacks
    private void OnMouseDown()
    {
        if(OrderManager.GetIstance.CanBringOrder)
        {
            OrderManager.CreateCocktailRequest();

            // Attiva la scheda informazioni dell'ordine di un personaggio
            var vignetteController = GetComponentInChildren<OrderUIController>();
            vignetteController.ActivateVignette();
        }
    }
    #endregion
}
