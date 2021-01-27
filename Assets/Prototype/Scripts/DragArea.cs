using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Prototype/Areas/Drag Area")]
[RequireComponent(typeof(BoxCollider))]
public class DragArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.gameObject.GetComponent<MCController>();
        if (playerController) playerController.EnableDragOperation();
    }
    private void OnTriggerExit(Collider other)
    {
        var playerController = other.gameObject.GetComponent<MCController>();
        if (playerController) playerController.DisableDragOperation();
    }
}
