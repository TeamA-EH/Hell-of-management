using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulsManager : MonoBehaviour
    {
        
        static SoulsManager self = null;
        List<GameObject> gameSouls = null;
        [SerializeField] GameObject soulAsset = null;

        /* SOUL STATES */
        public const uint SOUL_STATE_UNDEFINED = 0;
        public const uint SOUL_STATE_GROUNDED = 1;
        public const uint SOUL_STATE_FLOATING = 2;
        public const uint SOUL_STATE_HANDED = 3;
        public const uint SOUL_STATE_STORED = 4;        // Sono le anime nel magazzino; in questo stato non provano a scappare

        /* SOUL TAGS */
        public const uint SOUL_TAG_NULL = 0;
        public const uint SOUL_TAG_GREEN = 1;
        public const uint SOUL_TAG_BLUE = 2;
        public const uint SOUL_TAG_RED = 3;

        #region Unity Callbacks
        void Start()
        {
            Init();
        }
        #endregion

        void Init()
        {
            if(!self) self = this;

            gameSouls = new List<GameObject>();     // init the souls list
            IncreasesSoulsStack(20);                // Reallocates the list size with a new stack of 20 souls
            
        }

        public static void SetSoulState(ref GameObject soul, uint state)
        {
            soul.GetComponent<Animator>().SetInteger("State", (int)state);
        }

        ///<summary> Sets the soul tag for a soul changing his mesh </summary>
        public static void SetSoulTag(Soul target, uint tag)
        {
            target.Tag = tag;
        }

        ///<summary> Send a request for activate a new soul from the souls pull </summary>
        public static GameObject CreatesSoul(uint tag, Vector3 position)
        {
            foreach(var item in self.gameSouls)
            {
                if(!item.activeSelf)
                {
                    item.SetActive(true);
                    item.transform.position = position;
                    SetSoulTag(item.GetComponent<Soul>(), tag);
                    return item;
                }
            }

            self.IncreasesSoulsStack(10);   // generate a new stack

            foreach(var item in self.gameSouls)
            {
                if(!item.activeSelf)
                {
                    item.SetActive(true);
                    item.transform.position = position;
                    SetSoulTag(item.GetComponent<Soul>(), tag);
                    return item;
                }
            }

            return null;
        }

        ///<summary> Removes a soul from the current scene </summary>
        ///<param name="soul"> Soul to destroy </param>
        public static bool DestroySoul(GameObject soul)
        {
            if(!soul)
            {
                Debug.LogAssertion("Attention! The binded souls is NULL!");
                return false;
            }
            else
            {
                soul.SetActive(false);
                return true;
            }
        }

        ///<summary> Increases game souls by [amount] </summary>
        ///<param name="count"> Number of souls to create </param>
        void IncreasesSoulsStack(uint count)
        {
            for(int i = 0; i < count; i++)
            {
                var soul = Instantiate(soulAsset, gameObject.transform);
                soul.SetActive(false);
                gameSouls.Add(soul);
            }
        }

        
    }
}
