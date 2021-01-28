using UnityEngine;
using System;

public class ItemController : MonoBehaviour
{
    [Header("Settings"), Space(20)]
    [Tooltip("il peso di un oggetto affligge il movimento del giocatore")]
    public float weight;
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && pc.canDrag && currentDistance <= maxDistanceFromPlayer)
        {
            var r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(r,out hit, 5000, LayerMask.GetMask("Default")))
            {
                try
                {
                    var item = hit.collider.gameObject.GetComponent<ItemController>().gameObject;
                    if (hit.collider.gameObject.GetComponent<ItemController>())
                    {
                        if (pc.hands[0].available) pc.DragToLeftHand(item, dragTime);
                    }
                }
                catch
                {
                    //Debug.Log(hit.collider.gameObject);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1) && pc.canDrag && currentDistance <= maxDistanceFromPlayer)
        {
            var r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, 5000, LayerMask.GetMask("Default")))
            {
                try
                {
                    var item = hit.collider.gameObject.GetComponent<ItemController>().gameObject;
                    if (item)
                    {
                        if (pc.hands[1].available) pc.DragToRightHand(item, dragTime);
                    }
                }
                catch
                {
                    Debug.Log(hit.collider.gameObject);
                }
            }
        }
    }
}