using UnityEngine;

/// <summary>
/// Permette di creare un ordine se il GameObject a cui e' attaccato viene cliccato con il mouse
/// </summary>
public class OrderIteraction : MonoBehaviour
{
    private bool mouseOver = false;
    #region UnityCallbacks
    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    private void Update()
    {
        if (mouseOver == true)
        {
            if (OrderManager.self.m_canBringOrder && Input.GetKeyDown(KeyCode.E))
            {
                OrderManager.CreateRandomOrder(gameObject);

                // Attiva la scheda informazioni dell'ordine di un personaggio
                var vignetteController = GetComponentInChildren<OrderUIController>();
                vignetteController.ActivateVignette();
            }
        }
    }
    #endregion
}
