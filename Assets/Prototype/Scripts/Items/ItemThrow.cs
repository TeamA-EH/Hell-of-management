﻿using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    [Header("Throw Settings"), Space(20)]
    [SerializeField] float throwDuration;
    [SerializeField] float throwDistance;

    public void ThrowItem(GameObject item, Vector3 direction)
    {
        float speed = throwDistance / throwDuration;
        Rigidbody rb = item.GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;

        item.transform.SetParent(null);

        rb.AddForce(direction * speed, ForceMode.Impulse);

        item.GetComponent<ThrowableItemStates>().ChangeState(ThrowableItemStates.States.Floating);
    }
}
