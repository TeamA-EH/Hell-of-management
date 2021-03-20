using UnityEngine;

namespace HOM
{
    public interface IRecipe
    {
        uint Type {get;}
        GameObject Customer{get;}

        uint RedSouls {get;}
        uint GreenSouls {get;}
        uint BlueSouls {get;}

        ///<summary> Overrides recipe infos </summary>
        ///<param name="type"> Defines recipe type (1= dish, 2=drink) </param>
        ///<param name="customer"> The customer who own this order </param>
        ///<param name="redSouls"> The amount of red souls required by this order </param>
        ///<param name="greenSouls"> The amount of green souls required by this order </param>
        ///<param name="blueSouls"> The amount of blue souls required by this order </param>
        void OverrideRecipeInfos(uint type, GameObject customer, uint redSouls, uint greenSouls, uint blueSouls);
    }
}
