using UnityEngine;
using System;

public class ItemController : MonoBehaviour
{
    [Header("Settings"), Space(20)]
    [Tooltip("il peso di un oggetto affligge il movimento del giocatore")]
    public float weight;
    [Tooltip("Tempo in secondi per completare l'operazione di dragging di quest'oggetto")]
    [SerializeField] float dragTime = 0.2f;
    MCController pc;

    private void Start()
    {
        if (!pc) pc = MCController.GetIstance;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && pc.canDrag)
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
                    Debug.Log("can't hold other items");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
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