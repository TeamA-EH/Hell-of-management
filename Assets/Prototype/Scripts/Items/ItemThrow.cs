using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    public Transform theDest;
    public float throwForce = 10;
    public bool canHold = true;
    private Transform mousePos;
    public Camera RMCamera;

    private void Update()
    {       
        RaycastHit hit;
        Ray ray = RMCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            Vector3 hitPosition = hit.point;
            theDest.transform.LookAt(hitPosition);
            // Do something with the object that was hit by the raycast.
        }
    }

    void OnMouseDown()
    {
            var mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            var objectPos = RMCamera.ScreenToWorldPoint(mousePos);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = theDest.position;
            this.transform.localRotation = theDest.rotation;
            this.transform.parent = GameObject.Find("Destination").transform;
     }

    void OnMouseUp()
    {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = theDest.transform.forward * throwForce;
            GetComponent<BoxCollider>().enabled = true;
    }
}
