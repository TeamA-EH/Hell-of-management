using System;
using UnityEngine;

namespace HOM
{
    [RequireComponent(typeof(Animator))]
    public class SittedCustomer : MonoBehaviour
    {
        [SerializeField] ItemIteractionData customerIteractionData;
        public ItemIteractionData CustomerIteractionData => customerIteractionData;
        Animator animator = null;
        Material originalMaterial;

        public string currentTrigger {private set; get;} = "";

        public bool CanIteract => OrdersManager.self.activeOrders < OrdersManager.maxRecipesCount;

        #region Events
        ///<summary> Event called when the customer is clicked by the player </summary>
        public Action OnCustomerClicked;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        void OnMouseDown()
        {
            OnCustomerClicked?.Invoke();
        }
        #endregion

        void Initialize()
        {
            if(!animator) animator = gameObject.GetComponent<Animator>();

            //originalMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material; !-->Implementare con i materiali dei clienti
        }

        
    }
}
