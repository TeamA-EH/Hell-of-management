using UnityEngine;
public class OrderRequest
{

    public enum OrderType { Cocktail, Dish}          // Indica se bisogni andare alla cocktail machine o la dishes machine

    public readonly GameObject owner;
    public readonly OrderType orderType;
    public readonly uint redSouls;
    public readonly uint greenSouls;
    public readonly uint orangeSouls;

    public OrderRequest(OrderType orderType, uint redSouls, uint greenSouls, uint orangeSouls, GameObject _owner)
    {
        this.orderType = orderType;
        this.redSouls = redSouls;
        this.greenSouls = greenSouls;
        this.orangeSouls = orangeSouls;

        owner = _owner;
    }
}