using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName="New Weight Table", menuName="Hell Of Management/Data/Weight Table")]
    public class ItemWeightTable :  ScriptableObject
    {
        [Header("Souls"), Space(10)]
        [SerializeField] float redSoulWeight = 0;
        [SerializeField] float greenSoulWeight = 0;
        [SerializeField] float blueSoulWeight = 0;

        [Header("Plates"), Space(10)]
        [SerializeField] float dishPlateWeight = 0;
        [SerializeField] float drinkPlateWeight = 0;

        [Header("Garbage"), Space(10)]
        [SerializeField] float trashWeight = 0;
        [SerializeField] float trashbagWeight = 0;

        [Header("Demons"), Space(10)]
        [SerializeField] float femaleDemonWeight;
        [SerializeField] float maleDemonWeight;

        /* GETTERS */
        public float RedSoulWeight => redSoulWeight;
        public float GreenSoulWeight => greenSoulWeight;
        public float BlueSoulWeight => blueSoulWeight;
        public float DishPlateWeight => dishPlateWeight;
        public float DrinkPlateWeight => drinkPlateWeight;
        public float TrashWeight => trashWeight;
        public float TrashbagWeight => trashbagWeight;
        public float FemaleDemonWeight => femaleDemonWeight;
        public float MaleDemonWeight => maleDemonWeight;
    }
}
