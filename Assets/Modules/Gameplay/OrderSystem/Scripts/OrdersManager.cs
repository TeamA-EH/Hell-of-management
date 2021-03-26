using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public sealed class OrdersManager : MonoBehaviour
    {
        public static OrdersManager self {private set; get;} = null;
        [SerializeField] GameObject orderAsset;
        public GameObject[] orders {private set; get;}
        public const uint maxRecipesCount = 2;
        public uint activeOrders => GetActiveOrdersCount();
        #region Events
        ///<summary> Handlers for orders creation events </summary>
        ///<param name="sender"> The manger who sended the event </param>
        ///<param name="customer"> the customer who owns this order </param>
        ///<param name="redSouls"> The amount of red souls needed for completing the order </param>
        ///<param name="greenSouls"> The amount of green souls nedeed for completing the order </param>
        ///<param name="blueSouls"> The amount of blue souls needed for completing the order </param>
        public delegate void OrderCreatedEventHander(OrdersManager sender, GameObject customer, uint type, uint redSouls, uint greenSouls, uint blueSouls);
        ///<summary> Handlers for success order delivery events </summary>
        ///<param name="sender"> The system who sended this event </param>
        ///<param name="score"> The score gained by the player</param>
        public delegate void OrderCompletedEventHandler(OrdersManager sender, uint score);
        ///<summary> Handler for failed order delivery events </summary>
        ///<param name="sender"> The systme who sended this event </param>
        public delegate void OrderFailedEventHandler(OrdersManager sender);

        ///<summary> Event called when an customer order is created </summary>
        public static event OrderCreatedEventHander OnOrderCreated;
        ///<summary> Event called when a customer order is completed successfully </summary>
        public static event OrderCompletedEventHandler OnOrderCompleted;
        ///<summary> Event called when the wrong order is given to a customer </summary>
        public static event OrderFailedEventHandler OnOrderFailed;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        #endregion

        void Initialize()
        {
            if(!self) self = this;

            orders = new GameObject[maxRecipesCount];
            for(int i = 0; i < orders.Length; i++)
            {
                orders[i] = Instantiate(orderAsset, gameObject.transform);
                orders[i].SetActive(false);
            }
        }

        ///<summary> Creates an order for a customer </summary>
        ///<param name="customer"> The customer who owns the order </param>
        public static GameObject CreateOrder(GameObject customer)
        {
            foreach(var item in self.orders)
            {
                if(!item.activeSelf)
                {
                    item.SetActive(true);
                    uint r = 0;
                    uint g = 0;
                    uint b = 0;
                    while(r + g + b != 3)
                    {
                        r = (uint)UnityEngine.Random.Range(0,4);
                        g = (uint)UnityEngine.Random.Range(0,4);
                        b = (uint)UnityEngine.Random.Range(0,4);
                    }
                    uint type = (uint)UnityEngine.Random.Range(1,3);
                    item.GetComponent<IRecipe>().OverrideRecipeInfos(type, customer, r, g, b);
                    OnOrderCreated?.Invoke(self, customer, type, r, g, b);
                    return item;
                }
            }

            return null;
        }

        ///<summary> Evalutes if the order is correct and runs all events </summary>
        ///<param name="order"> The customer order </param>
        ///<param name="plate"> The plate to evaluate </param>
        public static void EvaluatePlate(Order order, Plate plate)
        {
            if(order.Type == plate.Type && order.RedSouls == plate.RedSouls && order.GreenSouls == plate.GreenSouls && order.BlueSouls == plate.BlueSouls)
            {
                Debug.LogWarning("Add Score Here");
                OnOrderCompleted?.Invoke(self, 0);
            }
            else 
            {
                OnOrderFailed?.Invoke(self);
            }
        }

        public uint GetActiveOrdersCount()
        {
            uint result = 0;
            foreach(var item in orders)
            {
                if(item.activeSelf) result++;
            }

            return result;
        }

        public static GameObject[] GetActiveOrders()
        {
            List<GameObject> result = new List<GameObject>();

            foreach(var item in self.orders)
            {
                if(item.activeSelf)
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }
       
    }
}