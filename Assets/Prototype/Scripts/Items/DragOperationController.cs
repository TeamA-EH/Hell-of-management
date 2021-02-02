using UnityEngine;
using DG.Tweening;

/// <summary>
/// Il GAMEOBJECT a cui e' attaccato quest'oggetto puo essere raccolto(picked-up) dal giocatore
/// </summary>
public class DragOperationController : MonoBehaviour
{
    [Header("Settings"), Space(20)]
    [Tooltip("Tempo in secondi per completare l'operazione di dragging di quest'oggetto")]
    [SerializeField] float dragTime = 0.2f;
    [Tooltip("Massima distanza rispetto al giocatore entro la quale l'oggetto puo essere raccolto")]
    [SerializeField] float maxDistanceFromPlayer = 3;
    float currentDistance
    {
        get
        {
            var playerPos = new Vector3(
                MCController.GetIstance.gameObject.transform.position.x,
                0,
                MCController.GetIstance.gameObject.transform.position.z);
            var itemPos = new Vector3(
                gameObject.transform.position.x,
                0,
                gameObject.transform.position.z);

            return (playerPos - itemPos).magnitude;
        }
    }

    MCController pc;

    private void Start()
    {
        if (!pc) pc = MCController.GetIstance;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(r, out hit))
            {
                if(hit.collider.gameObject.GetComponent<DragOperationController>() && pc.canDrag && pc.hands[0].available && currentDistance <= maxDistanceFromPlayer)
                {
                    DragToLeftHand(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit))
            {
                if (hit.collider.gameObject.GetComponent<DragOperationController>() && pc.canDrag  && pc.hands[1].available && currentDistance <= maxDistanceFromPlayer)
                {
                    DragToRightHand(hit.collider.gameObject);
                }
            }
        }
    }

    void DragToLeftHand(GameObject item)
    {
        item.transform.DOMove(pc.hands[0].transform.position, dragTime)
            .OnComplete(() =>
            {
                item.transform.SetParent(pc.hands[0].transform);

                pc.hands[0].holdedItem = item;
                pc.hands[0].available = false;

                //item.GetComponent<ThrowableItemStates>().ChangeState(ThrowableItemStates.States.Grounded);
            });
    }
    void DragToRightHand(GameObject item)
    {
        item.transform.DOMove(pc.hands[1].transform.position, dragTime)
            .OnComplete(() =>
            {
                item.transform.SetParent(pc.hands[1].transform);

                pc.hands[1].holdedItem = item;
                pc.hands[1].available = false;

                //item.GetComponent<ThrowableItemStates>().ChangeState(ThrowableItemStates.States.Grounded);
            });
    }
}