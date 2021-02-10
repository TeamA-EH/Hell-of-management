using UnityEngine;
using System;
using DG.Tweening;


[RequireComponent(typeof(SurfaceCleaner))]
public class MopController : MonoBehaviour
{
    SurfaceCleaner cleaner;

    #region UnityCallbacks
    private void Awake()
    {
        if (!cleaner) cleaner = gameObject.GetComponent<SurfaceCleaner>();
    }

    private void OnEnable()
    {
        cleaner.OnSurfaceCleaning += CleanSurface;
    }
    #endregion

    protected void CleanSurface(GameObject  _surface)
    {
        _surface.transform.DOScale(0, 1.2f)
            .OnComplete(() =>
            {
                Destroy(_surface);
            });
    }
}