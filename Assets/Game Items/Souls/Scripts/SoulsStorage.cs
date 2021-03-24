using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulsStorage : MonoBehaviour
    {
        public static SoulsStorage self = null;
        public List<GameObject> storedSouls {private set; get;} = new List<GameObject>();

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }

        void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.GetComponent<C_Garth>())
            {
                SpawnSoulsInStorage(4,4);
            }
        }
        #endregion

        void Initialize()
        {
            if(!self) self = this;
        }

        public static void SpawnSoulsInStorage(float _min, float _max)
        {
            if(self.storedSouls.Count > 0)
            {
                for(int i = 0; i < self.storedSouls.Count; i++)
                {
                    SoulsManager.DestroySoul(self.storedSouls[i]);
                }

                self.storedSouls.Clear();
            }

            uint count = 10;
            var orders = OrdersManager.GetActiveOrders();
            if(OrdersManager.self.GetActiveOrdersCount() > 0)
            {

                foreach(var soul in orders)
                {
                    var item = soul.GetComponent<Order>();

                    for(int i = 0; i < item.RedSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_RED, self.GetRandomVectorInRange(_min, _max));
                        obj.GetComponent<Soul>().Init();
                        self.storedSouls.Add(obj);
                        count--;
                    }

                    for(int i = 0; i < item.GreenSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_GREEN, self.GetRandomVectorInRange(_min, _max));
                        obj.GetComponent<Soul>().Init();
                        self.storedSouls.Add(obj);
                        count--;
                    }

                    for(int i = 0; i < item.BlueSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_BLUE, self.GetRandomVectorInRange(_min, _max));
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().Init();
                        self.storedSouls.Add(obj);
                        count--;
                    }

                    /* CREATES LAST SOULS */
                    for(int i = 0; i < count; i++)
                    {
                        var obj = SoulsManager.CreatesSoul((uint)UnityEngine.Random.Range(1,4), self.GetRandomVectorInRange(_min, _max));
                        obj.GetComponent<Soul>().Init();
                        self.storedSouls.Add(obj);
                    }
                }
            }

            for(int i = 0; i < count; i++)
            {
                var obj = SoulsManager.CreatesSoul((uint)UnityEngine.Random.Range(1,4), self.GetRandomVectorInRange(_min,_max));
                obj.GetComponent<Soul>().Init();
                self.storedSouls.Add(obj);
            }
        }

        Vector3 GetRandomVectorInRange(float _min, float _max)
        {
            float x = UnityEngine.Random.Range(self.gameObject.transform.position.x - _min, self.gameObject.transform.position.x + _max + 1);
            float z = UnityEngine.Random.Range(self.gameObject.transform.position.z - _min, self.gameObject.transform.position.z + _max + 1);
            return new Vector3(x, 0, z);
        }
    }
}