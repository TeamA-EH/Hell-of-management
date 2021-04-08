using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulsStorage : MonoBehaviour
    {
        public static SoulsStorage self = null;
        public List<GameObject> storedSouls {private set; get;} = new List<GameObject>();
       
       [SerializeField] GameObject[] points = new GameObject[10];

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.GetComponent<C_Garth>())
            {
                SpawnSoulsInStorage();
            }
        }
        #endregion

        void Initialize()
        {
            if(!self) self = this;
        }

        public static void SpawnSoulsInStorage()
        {
            if(self.storedSouls.Count > 0)
            {
                for(int i = 0; i < self.storedSouls.Count; i++)
                {
                    SoulsManager.DestroySoul(self.storedSouls[i]);
                }

                self.storedSouls.Clear();
            }

            uint count = 0;
            var orders = OrdersManager.GetActiveOrders();
            if(OrdersManager.self.GetActiveOrdersCount() > 0)
            {

                foreach(var soul in orders)
                {
                    var item = soul.GetComponent<Order>();

                    for(int i = 0; i < item.RedSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_RED, self.points[count].transform.position);
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().SetEnvironment(true);
                        obj.GetComponent<Soul>().ExecuteBehaviourTree();
                        self.storedSouls.Add(obj);
                        count++;
                    }

                    for(int i = 0; i < item.GreenSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_GREEN, self.points[count].transform.position);
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().SetEnvironment(true);
                        obj.GetComponent<Soul>().ExecuteBehaviourTree();
                        self.storedSouls.Add(obj);
                        count++;
                    }

                    for(int i = 0; i < item.BlueSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_BLUE, self.points[count].transform.position);
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().SetEnvironment(true);
                        obj.GetComponent<Soul>().ExecuteBehaviourTree();
                        self.storedSouls.Add(obj);
                        count++;
                    }

                }
                
                for(int i = 0; i < 4; i++)
                {
                    var obj = SoulsManager.CreatesSoul((uint)UnityEngine.Random.Range(1,4), self.points[count].transform.position);
                    obj.GetComponent<Soul>().Init();
                    obj.GetComponent<Soul>().SetEnvironment(true);
                    obj.GetComponent<Soul>().ExecuteBehaviourTree();
                    self.storedSouls.Add(obj);
                    count++;
                }
            }
            else
            {
                for(int i = 0; i < 10; i++)
                {
                    var obj = SoulsManager.CreatesSoul((uint)UnityEngine.Random.Range(1,4), self.points[i].transform.position);
                    obj.GetComponent<Soul>().Init();
                    obj.GetComponent<Soul>().SetEnvironment(true);
                    obj.GetComponent<Soul>().ExecuteBehaviourTree();
                    self.storedSouls.Add(obj);
                    count++;
                }
            }       
        }
    }
}