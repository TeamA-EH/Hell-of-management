using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public sealed class PlatesManager : MonoBehaviour
    {
        static PlatesManager self = null;
        [SerializeField] GameObject plateAsset;
        List<GameObject> platesList = null;

        /* PLATES UNIQUE TAGS */
        public const uint INVALID_PLATE = 0;
        public const uint DISH_PLATE = 1;
        public const uint DRINK_PLATE = 2;

    #region Unity Callbacks
    void Start()
    {
        Init();
    }
    #endregion

        void Init()
        {
            self = this;
            
            /*INITIALIZES PLATES POOL*/
            platesList = new List<GameObject>();
            IncreasePlatesStack(10);
        }

        ///<summary> Return a plate object from the plates pool </summary>
        ///<param name="tag"> The plate uniques identifier </param>
        ///<param name="position"> The plate spawn-point </param>
        ///<param name="orientation"> The plate rotation </param>
        public static GameObject CreatePlate(uint tag, Vector3 position, Quaternion orientation)
        {
            var plate = self.GetFirstAvailablePlate();
            if(!plate)
            {
                self.IncreasePlatesStack(10);
                plate = self.GetFirstAvailablePlate();
            }
            
            plate.SetActive(true);
            plate.GetComponent<Plate>().Type = tag;
            plate.GetComponent<Plate>().UpdateGFX();
            plate.transform.position = position;
            plate.transform.rotation = orientation;

            return plate;
        }

        public static void DestroyPlate(ref GameObject plate) => plate.SetActive(false);

        GameObject GetFirstAvailablePlate()
        {
            foreach(var item in platesList)
            {
                if(!item.activeSelf)
                {
                    return item;
                }
            }

            return null;
        }

        void IncreasePlatesStack(uint amount)
        {
            for(int i = 0; i < amount; i++)
            {
                var plate =  Instantiate(plateAsset, gameObject.transform);
                plate.SetActive(false);
                platesList.Add(plate);
            }
        }
    }
}
