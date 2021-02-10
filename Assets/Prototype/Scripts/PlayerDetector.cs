using UnityEngine;
using System;

public class PlayerDetector : MonoBehaviour
{
    public event Action<GameObject> OnPlayerDetected;
    public event Action<GameObject> OnPlayerExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MCController>())
        {
            OnPlayerDetected?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MCController>())
        {
            OnPlayerExit?.Invoke(other.gameObject);
        }
    }
}
