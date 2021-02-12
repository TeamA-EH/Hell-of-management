using UnityEngine;
using System;

[RequireComponent(typeof(SurfaceDetector))]
public class SurfaceCleaner : MonoBehaviour
{
    SurfaceDetector detector;

    #region Events
    /// <summary>
    /// Called when this component is elegible for cleaning a surface
    /// </summary>
    public event Action<GameObject> OnSurfaceCleaning;
    #endregion

    #region Custom Callbacks
    public void OnSurfaceDetected(GameObject _surface)
    {
        OnSurfaceCleaning?.Invoke(_surface);
    }
    #endregion

    #region UnityCallbacks
    private void Awake()
    {
        if (!detector) detector = gameObject.GetComponent<SurfaceDetector>();
    }
    private void OnEnable()
    {
        detector.OnSurfaceDetected += OnSurfaceDetected;
    }
    #endregion
}