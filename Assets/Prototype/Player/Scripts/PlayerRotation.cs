using UnityEngine;
using DG.Tweening;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation Settings"), Space(20)]
    [Tooltip("Indica in quanto tempo l'oggetto compie la rotazione")]
    [SerializeField] float rotationTime = 0.5f;

    public void UpdateRotation()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(r, out hit))
        {
            var direction = (hit.point - gameObject.transform.position).normalized;

            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

            gameObject.transform.DORotateQuaternion(Quaternion.Euler(0, angle - 90, 0), rotationTime);
            Debug.Log(angle);
        }
    }
}