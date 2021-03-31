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

                        if(IsRightOrder(order, plate))
                        {
                            Debug.Log("Correct Order");
                            order.Customer.GetComponentInChildren<OrderVignetteUI>().Deactivate();
                            order.Customer.GetComponent<Animator>().SetTrigger("Take Order");
                            order.gameObject.SetActive(false);
                            plate.gameObject.SetActive(false);

                            Score.self.AddScore(100);

                            return;
                        }
                        else    //Wrong Plate
                        {
                            Debug.Log("Wrong Order");
                            order.Customer.GetComponentInChildren<OrderVignetteUI>().Deactivate();
                            order.Customer.GetComponent<Animator>().SetTrigger("Leave Table");
                            order.gameObject.SetActive(false);
                            plate.gameObject.SetActive(false);
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
