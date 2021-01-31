using UnityEngine;

[RequireComponent(typeof(OrderDetector))]
public class DeliveryController : MonoBehaviour
{
    OrderDetector orderDetector;

    #region UnityCallbacks
    private void Awake()
    {
        if (!orderDetector) orderDetector = gameObject.GetComponent<OrderDetector>();

        /* SUBSCRIBE EVENTS */
        orderDetector.OnOrderDetected += OnOrderDetected;
    }
    #endregion

    protected void OnOrderDetected(GameObject order)
    {
        var orderIngredients = order.GetComponent<IngredientsDescription>();
        var activeOrder = OrderManager.GetIstance.ActiveOrder;

        if (activeOrder.redSouls == orderIngredients.redSouls &&
            activeOrder.greenSouls == orderIngredients.greenSouls &&
            activeOrder.orangeSouls == orderIngredients.orangeSouls && 
            activeOrder.orderType == orderIngredients.type)
        {
            OrderManager.CompleteOrder();

            Destroy(order);
        }
        else
        {
            OrderManager.FailOrder();

            Destroy(order);
        }

    }
}