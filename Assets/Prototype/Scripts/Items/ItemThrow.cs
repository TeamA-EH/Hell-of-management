using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    public void ThrowItem(GameObject item, Vector3 direction, float speed, float distance)
    {
        float totalTime = distance / speed;
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.velocity = new Vector3(direction.x * speed, (totalTime * Physics.gravity.magnitude) / 2, direction.z * speed);
    }
}
