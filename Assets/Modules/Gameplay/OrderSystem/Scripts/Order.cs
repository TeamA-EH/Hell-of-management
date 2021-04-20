using UnityEngine;

namespace HOM
{
    public class Order : MonoBehaviour, IRecipe
    {
        uint type = 0;
        public uint Type => type;
        GameObject customer;
        public GameObject Customer => customer;
        uint redSouls = 0;
        public uint RedSouls => redSouls;
        uint greenSouls = 0;
        public uint GreenSouls => greenSouls;
        uint blueSouls = 0;
        public uint BlueSouls => blueSouls;
        uint yellowSouls = 0;
        public uint YellowSouls => yellowSouls;
        uint purpleSouls = 0;
        public uint PurpleSouls => purpleSouls;

        public void OverrideRecipeInfos(uint type, GameObject customer, uint redSouls, uint greenSouls, uint blueSouls, uint yellowSouls, uint purpleSouls)
        {
            this.type = type;
            this.customer = customer;
            this.redSouls = redSouls;
            this.greenSouls = greenSouls;
            this.blueSouls = blueSouls;
            this.yellowSouls = yellowSouls;
            this.purpleSouls = purpleSouls;
        }

        

    }
}
