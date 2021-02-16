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

    protected void OnOrderDetected(GameObject _order)
    {
        var ingredients = _order.GetComponent<IngredientsDescription>();
        var orders = OrderManager.self.m_activeOrders;
        for(int i = 0; i < OrderManager.self.m_activeOrders.Count; i++)
        {
            if (ingredients.type == orders[i].orderType &&
                ingredients.redSouls == orders[i].redSouls &&
                ingredients.greenSouls == orders[i].greenSouls &&
                ingredients.orangeSouls == orders[i].orangeSouls)
            {
                OrderManager.CompeteOrder(orders[i]);
                Destroy(_order);
            }
        }

    }
}