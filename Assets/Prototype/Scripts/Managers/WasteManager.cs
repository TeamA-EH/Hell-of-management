using UnityEngine;

public class WasteManager : MonoBehaviour
{
    [SerializeField] WasteGenerator generator;

    #region UnityCallbacks
    private void Start()
    {
        generator.OnWasteCreated += OnWasteCreated;
    }
    #endregion

    void OnWasteCreated(GameObject waste)
    {

    }
}