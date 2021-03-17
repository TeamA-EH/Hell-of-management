using UnityEngine;

namespace HOM
{
    ///<summary> This class represents a character hand </summary>
    [System.Serializable]
    public class CharacterHand : MonoBehaviour
    {
        public bool m_canBind => !holdedItem;
        
        /* ASSETS VARIABLE FOR DISPLAYING WHEN ACTIVES */
        GameObject[] soulAssets = null;
        GameObject[] drunkenDemonAssets = null;
        GameObject wasteAsset;
        GameObject trashbagAsset;

        GameObject  holdedItem = null;  // the item who this character is holding

        ///<summary> Base hand constructor </summary>
        ///<param name="soulAssets"> A array of soul prefab (one for each type) to show when this hand binds a soul of that type </param>
        ///<param name="wasteAsset"> The waste prefab to show when this hand binds a waste item </param>
        ///<param name="trashbagAsset"> The trashbag prefab to show when this hand binds a trashabag item </param>
        ///<param name="drunkenDemonAssets"> An arrat of drunken demone prefabs to show when this hand binds a demon of that type </param>
        public void Initialize(GameObject[] soulAssets, GameObject wasteAsset, GameObject trashbagAsset, GameObject[] drunkenDemonAssets)
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

            this.wasteAsset = Instantiate(wasteAsset, gameObject.transform);    //Generates waste istance
            this.wasteAsset.SetActive(false);

            this.trashbagAsset = Instantiate(trashbagAsset, gameObject.transform); //Generates trahsbag istance
            this.trashbagAsset.SetActive(false);
        }

        ///<summary> Shows a soul type on this hand </summary>
        ///<param name="soulTag"> The unique tag for the soul to visalize (red, green, blue, etc.) </param>
        public void BindSoul(uint soulTag)
        {
            UnbindBindedItem();
            soulAssets[soulTag - 1].SetActive(true);
            holdedItem = soulAssets[soulTag - 1];
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

            holdedItem = null;
        }
    }
}