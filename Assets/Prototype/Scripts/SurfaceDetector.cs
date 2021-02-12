using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class SurfaceDetector : MonoBehaviour
{
    #region Events
    /// <summary>
    /// Called when the detector dectects a valid surface
    /// </summary>
    public event Action<GameObject> OnSurfaceDetected;
    #endregion

    #region UnityCallbacks
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<SurfaceController>())
        {
            OnSurfaceDetected?.Invoke(other.gameObject);
        }
    }
    #endregion
}