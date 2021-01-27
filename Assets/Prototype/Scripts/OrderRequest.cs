public class OrderRequest
{
    public readonly uint redSouls;
    public readonly uint greenSouls;
    public readonly uint orangeSouls;

    public OrderRequest(uint redSouls, uint greenSouls, uint orangeSouls)
    {
        this.redSouls = redSouls;
        this.greenSouls = greenSouls;
        this.orangeSouls = orangeSouls;
    }
}