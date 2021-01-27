using UnityEngine;

public class StorageIteraction : MonoBehaviour, IIteraction
{
    UIManager UI;

    /// <summary>
    /// Open the storage UI
    /// </summary>
    public void Iteract()
    {
        UI.SetStorageMenuVisibility(true);
    }

    #region UnityCallbacks
    private void Start()
    {
        UI = FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Iteract();
    }
    private void OnTriggerExit(Collider other)
    {
        UI.SetStorageMenuVisibility(false);
    }
    #endregion
}