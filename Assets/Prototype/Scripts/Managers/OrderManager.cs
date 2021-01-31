using UnityEngine;

public sealed class OrderManager : MonoBehaviour
{
    public static OrderManager GetIstance;

    public OrderRequest ActiveOrder { private set; get; }
    /// <summary>
    /// Restituisce TRUE se si prendere un ordine altrimenti resituise FALSE
    /// </summary>
    public bool CanBringOrder => ActiveOrder == null;

    public static event System.Action<OrderRequest> OnOrderReceived = (evtargs) => GetIstance.ActiveOrder = evtargs;

    public static event System.Action<OrderManager, OrderRequest> OnOrderCompleted = (sender, order) =>
    {
        // Add Score
        GetIstance.ActiveOrder = null;
    };
    public static event System.Action<OrderManager, OrderRequest> OnOrderFailed = (sender, order) =>
    {
        GetIstance.ActiveOrder = null;
    };

    #region UnityCallbacks
    private void Awake()
    {
        if (!GetIstance) GetIstance = this;
    }
    #endregion

    /// <summary>
    /// Crea casualmente un ordine tra cocktail e drink con ingredienti random
    /// </summary>
    /// <returns> Ordine Random</returns>
    public static OrderRequest CreateRandomRequest()
    {
        int redSouls = 100;
        int greenSouls = 100;
        int orangeSouls = 100;

        var orderType = (OrderRequest.OrderType)Random.Range(0, 2);

        while ((redSouls + greenSouls + orangeSouls) != 3)
        {
            redSouls = Random.Range(0, 4);
            greenSouls = Random.Range(0, 4);
            orangeSouls = Random.Range(0, 4);
        }

        var Order = new OrderRequest(orderType, (uint)redSouls, (uint)greenSouls, (uint)orangeSouls);

        OnOrderReceived?.Invoke(Order);

        return new OrderRequest(orderType, (uint)redSouls, (uint)greenSouls, (uint)orangeSouls);
    }
    /// <summary>
    /// Crea un ordine casuale tra Coktail e Dish con i parametri di anime specificati
    /// </summary>
    /// <param name="redSouls"></param>
    /// <param name="greenSouls"></param>
    /// <param name="orangeSouls"></param>
    /// <returns></returns>
    public static OrderRequest CreateRandomRequest(uint redSouls, uint greenSouls, uint orangeSouls)
    {
        var orderType = (OrderRequest.OrderType)Random.Range(0, 2);

        var order = new OrderRequest(orderType, redSouls, greenSouls, orangeSouls);

        OnOrderReceived?.Invoke(order);

        return order;
    }
    /// <summary>
    /// Crea un ordine per un cocktail con ingredienti casuali
    /// </summary>
    /// <returns> Ordine Cocktail</returns>
    public static OrderRequest CreateCocktailRequest()
    {
        int redSouls = 100;
        int greenSouls = 100;
        int orangeSouls = 100;

        var orderType = OrderRequest.OrderType.Cocktail;

        while ((redSouls + greenSouls + orangeSouls) != 3)
        {
            redSouls = Random.Range(0, 4);
            greenSouls = Random.Range(0, 4);
            orangeSouls = Random.Range(0, 4);
        }

        var Order = new OrderRequest(orderType, (uint)redSouls, (uint)greenSouls, (uint)orangeSouls);

        OnOrderReceived?.Invoke(Order);

        return new OrderRequest(orderType, (uint)redSouls, (uint)greenSouls, (uint)orangeSouls);
    }
    /// <summary>
    /// Crea un ordine per un cocktail con l'ammontare di ingredienti specificato
    /// </summary>
    /// <param name="redSouls"></param>
    /// <param name="greenSouls"></param>
    /// <param name="orangeSouls"></param>
    /// <returns></returns>
    public static OrderRequest CreateCocktailRequest(uint redSouls, uint greenSouls, uint orangeSouls)
    {
        var orderType = OrderRequest.OrderType.Cocktail;

        var order = new OrderRequest(orderType, redSouls, greenSouls, orangeSouls);

        OnOrderReceived?.Invoke(order);

        return order;
    }
    /// <summary>
    /// Crea un ordine per un piatto con ingredienti casuali
    /// </summary>
    /// <returns></returns>
    public static OrderRequest CreateDishRequest()
    {
        int redSouls = 100;
        int greenSouls = 100;
        int orangeSouls = 100;

        var orderType = OrderRequest.OrderType.Dish;

        while ((redSouls + greenSouls + orangeSouls) != 3)
        {
            redSouls = Random.Range(0, 4);
            greenSouls = Random.Range(0, 4);
            orangeSouls = Random.Range(0, 4);
        }

        var Order = new OrderRequest(orderType, (uint)redSouls, (uint)greenSouls, (uint)orangeSouls);

        OnOrderReceived?.Invoke(Order);

        return new OrderRequest(orderType, (uint)redSouls, (uint)greenSouls, (uint)orangeSouls);
    }
    /// <summary>
    /// Crea un ordine per un piatto con l'ammontare di ingredienti specificato
    /// </summary>
    /// <returns></returns>
    public static OrderRequest CreateDishRequest(uint redSouls, uint greenSouls, uint orangeSouls)
    {
        var orderType = OrderRequest.OrderType.Dish;

        var order = new OrderRequest(orderType, redSouls, greenSouls, orangeSouls);

        OnOrderReceived?.Invoke(order);

        return order;
    }

    /// <summary>
    /// Completa con successo l'ordine attualmente attivo
    /// </summary>
    public static void CompleteOrder() => OnOrderCompleted?.Invoke(GetIstance, GetIstance.ActiveOrder);
    /// <summary>
    /// Fallisce l'ordine attualmente attivo
    /// </summary>
    public static void FailOrder() => OnOrderFailed?.Invoke(GetIstance, GetIstance.ActiveOrder);

    /// <summary>
    /// Stampa nella console le informazioni dell'ordine appena generato
    /// </summary>
    public static void StampOrderInfos()
    {
        Debug.Log($"Type: {GetIstance.ActiveOrder.orderType}, " +
            $"Red Souls: {GetIstance.ActiveOrder.redSouls}, " +
            $"Green Souls: {GetIstance.ActiveOrder.greenSouls}, " +
            $"Orange Souls: {GetIstance.ActiveOrder.orangeSouls}");
    }
}