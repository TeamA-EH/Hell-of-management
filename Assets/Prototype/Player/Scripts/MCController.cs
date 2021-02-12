using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotation))]
[RequireComponent(typeof(ItemThrow))]
[RequireComponent(typeof(ItemDrop))]
public class MCController : MonoBehaviour
{
    public static MCController GetIstance;

    PlayerMovement movementComponent;
    PlayerRotation rotationComponent;
    ItemThrow throwSystem;
    ItemDrop dropSystem;


    public bool holdingItem => hands[0].holdedItem != null || hands[1].holdedItem != null;

    /* Drag Operation */
    public bool canDrag => (hands[0].available || hands[1].available);
    
    [System.Serializable]
    public struct Hand
    {
        public Transform transform;
        public GameObject holdedItem;
        public bool available;
    }
    public Hand[] hands = new Hand[2];
    public bool hasFreeHand
    {
        get
        {
            foreach(var hand in hands)
            {
                if (hand.available) return true;
            }

            return false;
        }
    }

    #region UnityCallbacks
    private void Awake()
    {
        /* CONTROLLER INITIALIZATION */
        if (!GetIstance) GetIstance = this;                                             //Singleton pattern

        /* COMPONENTS INITIALIZATION*/
        if (!movementComponent) movementComponent = GetComponent<PlayerMovement>();
        if (!rotationComponent) rotationComponent = GetComponent<PlayerRotation>();
        if (!throwSystem) throwSystem = GetComponent<ItemThrow>();
        if (!dropSystem) dropSystem = GetComponent<ItemDrop>();

        //animator = gameObject.GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        movementComponent.UpdateMovement();
        rotationComponent.UpdateRotation();

        /* INPUT CONTROL */
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(!hands[0].available)
            {

                Vector3 direction = Vector3.zero;

                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(r, out hit))
                {
                    Vector3 point = new Vector3(hit.point.x, 0, hit.point.z);
                    direction = (point - hands[0].transform.position).normalized;
                }

                switch (ItemUsabilityManager.GetInstance.ActiveMode)
                {
                    case ItemUsabilityManager.UsabilityModes.Drop:

                        dropSystem.DropItem(hands[0].holdedItem, direction);

                        hands[0].holdedItem = null;
                        hands[0].available = true;

                        break;
                    case ItemUsabilityManager.UsabilityModes.Throw:

                        throwSystem.ThrowItem(hands[0].holdedItem, direction);

                        hands[0].holdedItem = null;
                        hands[0].available = true;

                        break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!hands[1].available)
            {

                Vector3 direction = Vector3.zero;

                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(r, out hit))
                {
                    Vector3 point = new Vector3(hit.point.x, 0, hit.point.z);
                    direction = (point - hands[1].transform.position).normalized;
                }

                switch (ItemUsabilityManager.GetInstance.ActiveMode)
                {
                    case ItemUsabilityManager.UsabilityModes.Drop:

                        dropSystem.DropItem(hands[1].holdedItem, direction);

                        hands[1].holdedItem = null;
                        hands[1].available = true;

                        break;
                    case ItemUsabilityManager.UsabilityModes.Throw:

                        throwSystem.ThrowItem(hands[1].holdedItem, direction);

                        hands[1].holdedItem = null;
                        hands[1].available = true;

                        break;
                }
            }
        }
    }
    #endregion
}
