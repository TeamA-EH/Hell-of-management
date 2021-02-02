using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(ItemThrow))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotation))]
public class MCController : MonoBehaviour, IDragger
{
    public static MCController GetIstance;
    //Animator animator;                            DISATTIVATO - CAUSA: RICHIESTA DESIGNER
    PlayerMovement movementComponent;
    PlayerRotation rotationComponent;

    public Vector3 inputDirection { private set; get; } = Vector3.zero;
    public float PlayerSpeed { private set; get; } = 0.0f;
    public float CameraAngularDirection { private set; get; } = 0.0f;
    //public float PlayerAngularDirection => Vector3.SignedAngle(animator.gameObject.transform.forward, inputDirection.normalized, Vector3.up);

    [Header("Item Drop"), Space(20)]
    [Tooltip("Indica quanto lontano viene droppa l'oggetto in metri di unity")]
    [SerializeField] float dropDistance;
    [Tooltip("Indica in quanto tempo l'oggetto raggiunge la propria destinazione finale")]
    [SerializeField] float dropDuration;

    [Header("Item Throw"), Space(20)]
    [SerializeField] float throwDistance = 3;
    [SerializeField] float ThrowDuration = .5f;
    ItemThrow throwSystem;

    [Header("Object Holding Movement"), Space(20)]
    [Tooltip("Definisce la scalabilita del movimento del personaggio rispetto al peso dell'oggetto")]
    [SerializeField] float decelleration = 0;
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

    /* DRAG METHODS */
    #region Drag&Drop Operation
    public void Drag(GameObject item, float dragTime)
    {
        if (hands[0].available)
        {
            item.transform.DOJump(hands[0].transform.position, 1, 1, dragTime)
                .OnComplete(() =>
                {
                    item.transform.SetParent(hands[0].transform);
                    hands[0].holdedItem = item;
                    hands[0].available = false;
                    decelleration += item.GetComponent<ItemDragOperator>().weight;
                });
        }
        else
        {
            item.transform.DOJump(hands[1].transform.position, 1, 1, dragTime)
                .OnComplete(() =>
                {
                    item.transform.SetParent(hands[1].transform);
                    hands[1].holdedItem = item;
                    hands[1].available = false;
                    decelleration += item.GetComponent<ItemDragOperator>().weight;
                });
        }

    }
    public void DragToLeftHand(GameObject item, float dragTime)
    {
        hands[0].available = false;

        // disable physics
        var rb = item.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        item.transform.DOJump(hands[0].transform.position, 1, 1, dragTime)
                .OnComplete(() =>
                {
                    item.transform.SetParent(hands[0].transform);
                    hands[0].holdedItem = item;
                    decelleration += item.GetComponent<ItemDragOperator>().weight;
                    item.transform.DOKill();
                });
        
    }
    public void DragToRightHand(GameObject item, float dragTime)
    {
        hands[1].available = false;

        // disable physics
        var rb = item.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        item.transform.DOJump(hands[1].transform.position, 1, 1, dragTime)
                .OnComplete(() =>
                {
                    item.transform.SetParent(hands[1].transform);
                    hands[1].holdedItem = item;
                    decelleration += item.GetComponent<ItemDragOperator>().weight;
                    item.transform.DOKill();
                });
    }
    void DropItem(GameObject item)
    {
        item.transform.SetParent(null);

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 direction = Vector3.zero;

        if(Physics.Raycast(r, out hit))
        {
            var point = new Vector3(hit.point.x, 1, hit.point.z);

            /*CALCULATE DIRECTION*/
            direction = (point - gameObject.transform.position).normalized;
            float angle = Vector3.SignedAngle(Camera.main.transform.forward, direction, Vector3.up);
            gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
        }


        throwSystem.ThrowItem(item, direction, dropDuration, dropDistance);

        decelleration -= item.GetComponent<ItemDragOperator>().weight;
    }
    #endregion

    #region UnityCallbacks
    private void Awake()
    {
        /* CONTROLLER INITIALIZATION */
        if (!GetIstance) GetIstance = this;         //Singleton pattern
        if (!throwSystem) throwSystem = GetComponent<ItemThrow>();
        if (!movementComponent) movementComponent = GetComponent<PlayerMovement>();
        if (!rotationComponent) rotationComponent = GetComponent<PlayerRotation>();

        //animator = gameObject.GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        movementComponent.UpdateMovement();
        rotationComponent.UpdateRotation();

        /* THROW/DROP SYSTEM */
        if (Input.GetKeyUp(KeyCode.Mouse0) && !hands[0].available)
        {
            switch(ItemUsabilityManager.GetInstance.ActiveMode)
            {
                case ItemUsabilityManager.UsabilityModes.Drop:

                    DropItem(hands[0].holdedItem);
                    hands[0].holdedItem = null;
                    hands[0].available = true;

                    Debug.Log("item dropped!");

                    break;
                case ItemUsabilityManager.UsabilityModes.Throw:

                    // [ITEM THROW]
                    Vector3 direction = Vector3.zero;

                    var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if(Physics.Raycast(Ray, out hit))
                    {
                        var point = new Vector3(hit.point.x, gameObject.transform.position.y, hit.point.z);
                        direction = (point - gameObject.transform.position).normalized;    
                    }
                        
                    Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + direction * throwDistance, Color.red, 5f);

                    throwSystem.ThrowItem(hands[0].holdedItem, direction, ThrowDuration, throwDistance);

                    decelleration -= hands[0].holdedItem.GetComponent<ItemDragOperator>().weight;

                    hands[0].holdedItem = null;
                    hands[0].available = true;

                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && !hands[1].available)
        {
            switch (ItemUsabilityManager.GetInstance.ActiveMode)
            {
                case ItemUsabilityManager.UsabilityModes.Drop:

                    // [RELEASE ITEM ]
                    DropItem(hands[1].holdedItem);
                    hands[1].holdedItem = null;
                    hands[1].available = true;

                    Debug.Log("item dropped!");

                    break;
                case ItemUsabilityManager.UsabilityModes.Throw:

                    // [ITEM THROW]
                    Vector3 direction = Vector3.zero;

                    var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(Ray, out hit))
                    {
                        var point = new Vector3(hit.point.x, gameObject.transform.position.y, hit.point.z);
                        direction = (point - gameObject.transform.position).normalized;
                        float angle = Vector3.SignedAngle(Camera.main.transform.forward, direction, Vector3.up);

                        gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
                    }

                    Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + direction * throwDistance, Color.red, 5f);

                    throwSystem.ThrowItem(hands[1].holdedItem, direction, ThrowDuration, throwDistance);

                    decelleration -= hands[1].holdedItem.GetComponent<ItemDragOperator>().weight;

                    hands[1].holdedItem = null;
                    hands[1].available = true;

                    break;
            }
        }
    }
    #endregion
}
