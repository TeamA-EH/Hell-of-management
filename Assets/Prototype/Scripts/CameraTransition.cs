using UnityEngine;
using UnityEngine.Events;

public class CameraTransition : MonoBehaviour
{

    [SerializeField] UnityEvent OnAreaEnter;
    [SerializeField] UnityEvent OnAreaExit;

    private void OnTriggerEnter(Collider other)
    {
        OnAreaEnter?.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        OnAreaExit?.Invoke();
    }
}