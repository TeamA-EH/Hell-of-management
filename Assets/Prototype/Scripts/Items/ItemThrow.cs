using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    public void ThrowItem(GameObject item, Vector3 direction, float duration, float distance)
    {
        float speed = distance / duration;
        Rigidbody rb = item.GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;

        item.transform.SetParent(null);

        rb.AddForce(direction * speed, ForceMode.Impulse);
    }
}
