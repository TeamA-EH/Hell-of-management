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

        public void OverrideRecipeInfos(uint type, GameObject customer, uint redSouls, uint greenSouls, uint blueSouls)
        {
            this.type = type;
            this.customer = customer;
            this.redSouls = redSouls;
            this.greenSouls = greenSouls;
            this.blueSouls = blueSouls;
        }

        

    }
}
