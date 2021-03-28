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
        bool CanCall = false;

        #region Events
        ///<summary> Event called when the customer is clicked by the player </summary>
        public Action OnCustomerClicked;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) && CanCall)
            {
                OnCustomerClicked?.Invoke();
            }
        }
        void OnMouseEnter()
        {
            CanCall = true;
        }

        void OnMouseExit()
        {
            CanCall = false;
        }
        #endregion

        void Initialize()
        {
            if(!animator) animator = gameObject.GetComponent<Animator>();

            //originalMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material; !-->Implementare con i materiali dei clienti
        }

        
    }
}
