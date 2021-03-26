using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class Plate : MonoBehaviour, IRecipe
    {
        [SerializeField] GameObject drinkGFX;
        [SerializeField] GameObject dishGFX;

       [SerializeField] uint type;
        public uint Type => type;

        GameObject customer = null;
        public GameObject Customer => customer;

        uint redsouls;
        public uint RedSouls => redsouls;
        uint greensouls;
        public uint GreenSouls => greensouls;
        uint bluesouls;
        public uint BlueSouls => bluesouls;

        public void OverrideRecipeInfos(uint type, GameObject customer, uint redSouls, uint greenSouls, uint blueSouls)
        {
            this.type = type;
            this.customer = customer;
            this.redsouls = redSouls;
            this.greensouls = greenSouls;
            this.bluesouls = blueSouls;
        }

        public void UpdateGFX()
        {
            if(type == 1)
            {
                dishGFX.SetActive(true);
            }
            else if(type == 2)
            {
                drinkGFX.SetActive(true);
            }
        }

        public void ResetGFX()
        {
            dishGFX.SetActive(false);
            drinkGFX.SetActive(false);
        }

    }
}