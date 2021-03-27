using System.Collections;
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
            if(collider.gameObject.GetComponent<Plate>())
            {
                var customers = gameObject.GetComponentsInChildren<SittedCustomer>();

                /* Check for customer order */
                foreach(GameObject order in OrdersManager.GetActiveOrders())
                {
                    foreach(var customer in customers)
                    {
                        if(order.GetComponent<Order>().Customer == customer.gameObject)
                        {
                            collider.gameObject.SetActive(false);
                            order.SetActive(false);
                            customer.GetComponentInChildren<OrderVignetteUI>().Deactivate();
                            customer.gameObject.GetComponent<Animator>().SetTrigger("Take Order");
                            return;
                        }
                    }
                }

                //collider.gameObject.SetActive(false);
                OrdersManager.GetActiveOrders()[0].GetComponent<Order>().Customer.GetComponentInChildren<OrderVignetteUI>().Deactivate();
                OrdersManager.GetActiveOrders()[0].GetComponent<Order>().Customer.GetComponent<Animator>().SetTrigger("Leave Table");
                OrdersManager.GetActiveOrders()[0].SetActive(false);
            }
        }
        #endregion

        void Init()
        {
            chairs = GetComponentsInChildren<Chair>();
        }
    }
}
