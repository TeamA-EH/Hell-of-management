using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulsStorage : MonoBehaviour
    {
        public static SoulsStorage self = null;
        public List<GameObject> storedSouls {private set; get;} = new List<GameObject>();
        [SerializeField] Vector2 roomSize;
        public Vector2 RoomSize => roomSize;

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                Vector3 point = GetPointInsideRoom();
                Debug.DrawLine(point + Vector3.up, point, Color.red, 30f);
                Debug.Log($"Point: {point} - IsRoom: {PointInRange(point)}");
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(roomSize.x, 1, roomSize.y));
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

            uint count = 10;
            var orders = OrdersManager.GetActiveOrders();
            if(OrdersManager.self.GetActiveOrdersCount() > 0)
            {

                foreach(var soul in orders)
                {
                    var item = soul.GetComponent<Order>();

                    for(int i = 0; i < item.RedSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_RED, self.GetPointInsideRoom());
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().SetEnvironment(true);
                        obj.GetComponent<Soul>().ExecuteBehaviourTree();
                        self.storedSouls.Add(obj);
                        count--;
                    }

                    for(int i = 0; i < item.GreenSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_GREEN, self.GetPointInsideRoom());
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().SetEnvironment(true);
                        obj.GetComponent<Soul>().ExecuteBehaviourTree();
                        self.storedSouls.Add(obj);
                        count--;
                    }

                    for(int i = 0; i < item.BlueSouls; i++)
                    {
                        var obj = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_BLUE, self.GetPointInsideRoom());
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().SetEnvironment(true);
                        obj.GetComponent<Soul>().ExecuteBehaviourTree();
                        self.storedSouls.Add(obj);
                        count--;
                    }

                    /* CREATES LAST SOULS */
                    for(int i = 0; i < count; i++)
                    {
                        var obj = SoulsManager.CreatesSoul((uint)UnityEngine.Random.Range(1,4), self.GetPointInsideRoom());
                        obj.GetComponent<Soul>().Init();
                        obj.GetComponent<Soul>().SetEnvironment(true);
                        obj.GetComponent<Soul>().ExecuteBehaviourTree();
                        self.storedSouls.Add(obj);
                    }
                }
            }

            for(int i = 0; i < count; i++)
            {
                var obj = SoulsManager.CreatesSoul((uint)UnityEngine.Random.Range(1,4), self.GetPointInsideRoom());
                obj.GetComponent<Soul>().Init();
                obj.GetComponent<Soul>().SetEnvironment(true);
                obj.GetComponent<Soul>().ExecuteBehaviourTree();
                self.storedSouls.Add(obj);
            }
        }

        /// <summary> Returns TRUE if the point is inside the storage room otherwise FALSE </summary>
        ///<param name="point"> The point to evaluate </param>
        public bool PointInRange(Vector3 point)
        {
            if(
                (point.x >= gameObject.transform.position.x - roomSize.x && point.x/2 <= gameObject.transform.position.x + roomSize.x/2) &&
                (point.z >= gameObject.transform.position.z - roomSize.y && point.z/2 <= gameObject.transform.position.z + roomSize.y/2)) return true;
                else return false;
        }

        public Vector3 GetPointInsideRoom()
        {
            float x = UnityEngine.Random.Range(gameObject.transform.position.x - roomSize.x/2, gameObject.transform.position.x + (roomSize.x)/2);
            float y = UnityEngine.Random.Range(gameObject.transform.position.z - roomSize.y/2, gameObject.transform.position.z + (roomSize.y)/2);
            return new Vector3(x, 0, y);
        }
    }
}