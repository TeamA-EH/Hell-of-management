using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        this.transform.position = target.transform.position + offset;
    }
}
