using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("Drop Settings"), Space(20)]
    [SerializeField] float dropDuration;
    [SerializeField] float dropDistance;

    public void DropItem(GameObject item, Vector3 direction)
    {
        float speed = dropDistance / dropDuration;

        Rigidbody rb = item.GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;

        item.transform.SetParent(null);

        rb.AddForce(direction * speed, ForceMode.Impulse);

        item.GetComponent<ThrowableItemStates>().ChangeState(ThrowableItemStates.States.Floating);
    }
}