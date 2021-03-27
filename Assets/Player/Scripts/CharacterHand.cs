using UnityEngine;

namespace HOM
{
    ///<summary> This class represents a character hand </summary>
    [System.Serializable]
    public class CharacterHand : MonoBehaviour
    {
        public bool m_canBind => !holdedItem;
        
        /* ASSETS VARIABLE FOR DISPLAYING WHEN ACTIVE */
        GameObject[] soulAssets = null;
        GameObject[] drunkenDemonAssets = null;
        GameObject wasteAsset;
        GameObject trashbagAsset;
        GameObject drinkAsset;
        GameObject dishAsset;

        GameObject  holdedItem = null;  // the item who this character is holding
        public GameObject HoldedItem => holdedItem;

        public OrdersManager.OrderInfo plateInfos;

        public uint holdedItemIndex {private set; get;} = 0;

        ///<summary> Base hand constructor </summary>
        ///<param name="soulAssets"> A array of soul prefab (one for each type) to show when this hand binds a soul of that type </param>
        ///<param name="wasteAsset"> The waste prefab to show when this hand binds a waste item </param>
        ///<param name="trashbagAsset"> The trashbag prefab to show when this hand binds a trashabag item </param>
        ///<param name="drunkenDemonAssets"> An array of drunken demon prefabs to show when this hand binds a demon of that type </param>
        ///<param name="dishAsset"> The dish prefab to show whne this hand binds a dish item </param>
        ///<param name="drinkAsset"> The drink prefab to show whne this hand binds a drink item </param>
        public void Initialize(GameObject[] soulAssets, GameObject wasteAsset, GameObject trashbagAsset, GameObject[] drunkenDemonAssets, GameObject dishAsset, GameObject drinkAsset)
        {
            /* INITIALIZES DATA */
            this.soulAssets = new GameObject[soulAssets.Length];
            this.drunkenDemonAssets = new GameObject[drunkenDemonAssets.Length];

            /* ALLOCATES DATA */
            for(int i = 0; i < soulAssets.Length; i++) //generates souls istances
            {
                var soul = Instantiate(soulAssets[i], gameObject.transform);
                soul.SetActive(false);
                this.soulAssets[i] = soul;  // stores new data
                
            }

            for(int i = 0; i < drunkenDemonAssets.Length; i++) //generates demons istances
            {
                var ddemon = Instantiate(drunkenDemonAssets[i], gameObject.transform);
                ddemon.SetActive(false);
                this.drunkenDemonAssets[i] = ddemon;
            }

            this.wasteAsset = Instantiate(wasteAsset, gameObject.transform);        //Generates waste istance
            this.wasteAsset.SetActive(false);

            this.trashbagAsset = Instantiate(trashbagAsset, gameObject.transform);  //Generates trahsbag istance
            this.trashbagAsset.SetActive(false);

            this.dishAsset = Instantiate(dishAsset, gameObject.transform);          //Generates dish plate istance 
            dishAsset.SetActive(false);

            this.drinkAsset = Instantiate(drinkAsset, gameObject.transform);        //Generated drink plate istance
            drinkAsset.SetActive(false);
        }

        ///<summary> Shows a soul type on this hand </summary>
        ///<param name="soulTag"> The unique tag for the soul to visalize (red, green, blue, etc.) </param>
        public void BindSoul(uint soulTag)
        {
            UnbindBindedItem();
            soulAssets[soulTag - 1].SetActive(true);

            switch(soulTag)
            {
                case SoulsManager.SOUL_TAG_RED:
                    soulAssets[2].SetActive(true); 
                    holdedItemIndex = 0;
                    holdedItem = soulAssets[2];
                break;
                case SoulsManager.SOUL_TAG_GREEN:
                    soulAssets[0].SetActive(true);
                    holdedItem = soulAssets[0];
                    holdedItemIndex = 1;
                break;
                case SoulsManager.SOUL_TAG_BLUE:
                    soulAssets[1].SetActive(true);
                    holdedItem = soulAssets[1];
                    holdedItemIndex = 2;
                break;
            }
        }

        ///<summary> Shows a waste on this hand </summary>
        public void BindWaste()
        {
            UnbindBindedItem();
            wasteAsset.SetActive(true);
            holdedItem = wasteAsset;
        }
        ///<summary> Shows a trashbag on this hand </summary>
        public void BindTrashBag()
        {
            UnbindBindedItem();
            trashbagAsset.SetActive(true);
            holdedItem = trashbagAsset;
        }

        ///<summary> Shows a drunken demon on this hand </summary>
        ///<param name="type"> The unique tag for the demon to visalize (red, green, blue, etc.) </param>
        public void BindDrunkenDemon(uint type)
        {
            UnbindBindedItem();
            drunkenDemonAssets[type].SetActive(true);
            holdedItem = drunkenDemonAssets[type];
        }

        ///<summary> Shows a plate on this hand </summary>
        ///<param name="type"> The unique tag for the plate to visualize(1: dish, 2: drink)
        public void BindPlate(uint type)
        {
            UnbindBindedItem();

            if(type == 1)           //VISUALIZE DISH
            {
                dishAsset.SetActive(true);
                holdedItemIndex = 3;
                holdedItem = dishAsset;
            }
            else if(type == 2)      //VISUALIZE DRINK
            {
                drinkAsset.SetActive(true);
                holdedItemIndex = 4;
                holdedItem = drinkAsset;
            }
        }

        ///<summary> Reset item holded by this hand </summary>
        public void UnbindBindedItem()
        {
            foreach(var item in soulAssets)
            {
                item.SetActive(false);
            }

            foreach(var item in drunkenDemonAssets)
            {
                item.SetActive(false);
            }

            wasteAsset.SetActive(false);
            trashbagAsset.SetActive(false);

            dishAsset.SetActive(false);
            drinkAsset.SetActive(false);

            holdedItem = null;
            holdedItemIndex = 0;
        }
    }
}