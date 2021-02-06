using UnityEngine;
using System;

public class WasteGenerator : MonoBehaviour
{
    [SerializeField] GameObject WasteAsset;
    [SerializeField] float generationPercentage = 70;
    protected static WasteGenerator GetIstance;

    public event Action<GameObject> OnWasteCreated;

    #region UnityCallbacks
    private void Awake()
    {
        if (!GetIstance) GetIstance = this;
    }
    #endregion

    /// <summary>
    /// Create a waste object by given position
    /// </summary>
    public static void CreateWaste(Vector3 position)
    {
        var waste = Instantiate(GetIstance.WasteAsset);
        waste.transform.position = position;

        GetIstance.OnWasteCreated?.Invoke(waste);
    }
    public static bool CanGenerateWaste()
    {
        var roll = UnityEngine.Random.Range(0, 101);

        return roll <= GetIstance.generationPercentage;
    }
}