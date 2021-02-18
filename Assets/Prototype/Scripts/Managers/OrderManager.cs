using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class OrderManager : MonoBehaviour
{
    /// <summary>
    /// Singleton reference
    /// </summary>
    public static OrderManager self;

    /// <summary>
    /// Active orders for the player
    /// </summary>
    public List<OrderRequest> m_activeOrders { private set; get; } = new List<OrderRequest>();
    public bool m_canBringOrder => m_activeOrders.Count < 2;

    #region Order Events
    /// <summary>
    /// Called when the user sent the input to receive an order
    /// </summary>
    public event Action<OrderManager, OrderRequest> OnOrderCreated;
    /// <summary>
    /// Called when the user delivers the right order to the order owner
    /// </summary>
    public event Action<OrderManager, OrderRequest> OnOrderCompleted;
    /// <summary>
    /// Called when the user delivers the wrong order to the order owner
    /// </summary>
    public event Action<OrderManager, OrderRequest> OnOrderFailed;

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (!self) self = this;
    }
    #endregion

    public static void CreateDrinkOrder(uint _redSouls, uint _greenSouls, uint _orangeSouls, GameObject _orderOwner)
    {
        var order = new OrderRequest(OrderRequest.OrderType.Cocktail, _redSouls, _greenSouls, _orangeSouls, _orderOwner);

        self.m_activeOrders.Add(order);
        self.OnOrderCreated?.Invoke(self, order);
    }
    public static void CreateDishOrder(uint _redSouls, uint _greenSouls, uint _orangeSouls, GameObject _orderOwner)
    {
        var order = new OrderRequest(OrderRequest.OrderType.Dish, _redSouls, _greenSouls, _orangeSouls, _orderOwner);

        self.m_activeOrders.Add(order);
        self.OnOrderCreated?.Invoke(self, order);
    }
    public static void CreateRandomOrder(GameObject _orderOwner)
    {
        uint r = 0;
        uint g = 0;
        uint o = 0;

        while(r + g + o != 3)
        {
            r = (uint)UnityEngine.Random.Range(0, 4);
            g = (uint)UnityEngine.Random.Range(0, 4);
            o = (uint)UnityEngine.Random.Range(0, 4);
        }

        OrderRequest.OrderType type = (OrderRequest.OrderType)UnityEngine.Random.Range(0, 2);

        var order = new OrderRequest(type, r, g, o, _orderOwner);
        self.m_activeOrders.Add(order);
        self.OnOrderCreated?.Invoke(self, order);
    }

    public static void CompeteOrder(OrderRequest _order)
    {
        /* DEACTIVATES ORDERS & GUEST GFX */
        _order.owner.GetComponentInChildren<OrderVignette>().ResetUI();
        _order.owner.GetComponentInChildren<OrderUIController>().DeactivateVignette();
        _order.owner.SetActive(false);

        /* GENERATE WASTE */
        if (WasteGenerator.CanGenerateWaste())
        {
            WasteGenerator.CreateWaste(_order.owner.transform.position + (_order.owner.transform.forward * 2) + Vector3.up * 3);
        }

        self.m_activeOrders.Remove(_order);
        self.OnOrderCompleted?.Invoke(self, _order);
    }
    public static void FailOrder(OrderRequest _order)
    {
        self.m_activeOrders.Remove(_order);
        self.OnOrderFailed?.Invoke(self, _order);
    }
}