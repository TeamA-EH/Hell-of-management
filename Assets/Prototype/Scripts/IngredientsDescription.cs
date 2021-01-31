using UnityEngine;

public class IngredientsDescription : MonoBehaviour
{
    public uint redSouls { private set; get; } = 0;
    public uint greenSouls { private set; get; } = 0;
    public uint orangeSouls { private set; get; } = 0;

    public void SetIngredientsQuantity(uint redSouls, uint greenSouls, uint orangeSouls)
    {
        this.redSouls = redSouls;
        this.greenSouls = greenSouls;
        this.orangeSouls = orangeSouls;
    }
}