using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    [RequireComponent(typeof(Collider))]
    public sealed class CustomerTable : MonoBehaviour
    {
        Chair[] chairs;

        #region Unity Callbacks
        void Start()
        {
            Init();
        }
        void OnTriggerEnter(Collider collider)
        {
            var plate = collider.gameObject.GetComponent<Plate>();
            if(plate)
            {
                if(OrdersManager.GetActiveOrders().Length > 0)
                {

                    if(AnyCustomerOrder(plate))
                    {
                        var order = GetOrder(plate);
                        if(!order)
                        {
                            Debug.LogWarning("Attention! Error evaluating this order");
                            return;
                        }

                        /* CHECK ALL ORDERS */
                        foreach(var item in OrdersManager.GetActiveOrders())
                        {
                            if(IsRightOrder(item.GetComponent<Order>(), plate))
                            {
                                Debug.Log("Correct Order");
                                plate.gameObject.transform.position = order.Customer.GetComponentInParent<Chair>().PlateOffset.position;
                                order.Customer.GetComponentInParent<Chair>().RegisterPlate(plate.gameObject);
                                plate.DisablePhysics();
                                item.GetComponent<Order>().Customer.GetComponentInChildren<OrderVignetteUI>().Deactivate();
                                item.GetComponent<Order>().Customer.GetComponent<Animator>().SetTrigger("Take Order");
                                item.SetActive(false);

                                Score.self.AddScore(100);

                                return;
                            }
                        }

                        Debug.Log($"Wrong Order: \n\n Order Type: {order.Type}\n Order red Souls: {order.RedSouls} \n Order Green Souls: {order.GreenSouls} \n Order Blue Souls: {order.BlueSouls}\n\n "
                            +$"Plate Type: {plate.Type} \n Plate Red Souls: {plate.RedSouls} \n Plate Green Souls: {plate.GreenSouls} \n Plate Blue Souls: {plate.BlueSouls} \n\n");

                        if(AnyDrinkOrder() && plate.Type == 2)
                        {
                            if(OrdersManager.GetDrinkOrdersCount() > 1)
                            {
                                plate.gameObject.transform.position = OrdersManager.GetActiveOrders()[UnityEngine.Random.Range(0, OrdersManager.GetActiveOrders().Length - 1)].GetComponent<Order>().Customer.GetComponentInParent<Chair>().PlateOffset.transform.position;
                                plate.DisablePhysics();
                            }
                            else
                            {
                                foreach(var item in OrdersManager.GetActiveOrders())
                                {
                                    if(item.GetComponent<Order>().Type == 2)
                                    {
                                        plate.gameObject.transform.position = item.GetComponent<Order>().Customer.GetComponentInParent<Chair>().PlateOffset.transform.position;
                                        plate.DisablePhysics();
                                        return;
                                    }
                                }
                            }
                        }
                        else if(AnyDishOrder() && plate.Type == 1)
                        {
                            if(OrdersManager.GetDishOrderCount() > 1)
                            {
                                var radnomIndex = 0;
                                for(int i = 0; i < OrdersManager.GetActiveOrders().Length; i++)
                                {
                                    if(i == radnomIndex)
                                    {
                                        plate.gameObject.transform.position = OrdersManager.GetActiveOrders()[i].GetComponent<Order>().Customer.GetComponentInParent<Chair>().PlateOffset.transform.position;
                                        plate.DisablePhysics();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                foreach(var item in OrdersManager.GetActiveOrders())
                                {
                                    if(item.GetComponent<Order>().Type == 1)
                                    {
                                        plate.gameObject.transform.position = item.GetComponent<Order>().Customer.GetComponentInParent<Chair>().PlateOffset.transform.position;
                                        plate.DisablePhysics();
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Attention! Can't define the plate type");
                            return;
                        }               

                    }
                    else    //The wrong table
                    {
                        plate.gameObject.SetActive(false);
                        Debug.LogWarning("Wrong Table");
                    }
                }
            }
        }
        #endregion

        void Init()
        {
            chairs = GetComponentsInChildren<Chair>();
        }

        bool AnyCustomerOrder(Plate plate)
        {
            foreach(var order in OrdersManager.GetActiveOrders())
            {
                if(order.GetComponent<Order>().Type == plate.Type)
                {
                    return true;
                }
            }

            return false;
        }

        bool AnyDishOrder()
        {
            foreach(var order in OrdersManager.GetActiveOrders())
            {
                if(order.GetComponent<Order>().Type == 1) return true;
            }

            return false;
        }

        bool AnyDrinkOrder()
        {
            foreach (var order in OrdersManager.GetActiveOrders())
            {
                if(order.GetComponent<Order>().Type == 2) return true;
            }

            return false;
        }

        Order GetOrder(Plate plate)
        {
            foreach(var order in OrdersManager.GetActiveOrders())
            {
                if(order.GetComponent<Order>().Type == plate.Type)
                {
                    return order.GetComponent<Order>();
                }
            }

            return null;
        }

        bool IsRightOrder(Order order, Plate plate)
        {
            if  (order.Type == plate.Type && 
                order.RedSouls == plate.RedSouls &&
                order.GreenSouls == plate.GreenSouls && 
                order.BlueSouls == plate.BlueSouls) return true;
                else return false;
        }

        Order[] GetAllOrdersOfType(uint type)
        {
            List<Order> result = new List<Order>();

            foreach(var order in OrdersManager.GetActiveOrders())
            {
                if(order.GetComponent<Order>().Type == type)
                {
                    result.Add(order.GetComponent<Order>());
                }
            }

            return result.ToArray();
        }
    }
}
