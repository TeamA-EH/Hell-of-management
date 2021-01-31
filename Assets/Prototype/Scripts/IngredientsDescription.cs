using UnityEngine;

public class IngredientsDescription : MonoBehaviour
{
    public OrderRequest.OrderType type;
    public uint redSouls { private set; get; } = 0;
    public uint greenSouls { private set; get; } = 0;
    public uint orangeSouls { private set; get; } = 0;

    public void SetIngredientsQuantity(OrderRequest.OrderType type, uint redSouls, uint greenSouls, uint orangeSouls)
    {
        this.type = type;

        this.redSouls = redSouls;
        this.greenSouls = greenSouls;
        this.orangeSouls = orangeSouls;
    }
}