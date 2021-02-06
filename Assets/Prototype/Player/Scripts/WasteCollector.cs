using UnityEngine;
using System;

public class WasteCollector : MonoBehaviour
{
    [SerializeField] uint maxCapacity = 10;
    public uint currentCapacity { private set; get; } = 0;

    #region Events
    public event Action OnWasteAdded;
    public event Action OnMaxCapacityAchieved;
    #endregion

    public void AddWaste()
    {
        currentCapacity++;

        OnWasteAdded?.Invoke();

        if(currentCapacity == maxCapacity)
        {
            OnMaxCapacityAchieved?.Invoke();
            ResetCapacity();
        }
    }
    void ResetCapacity() => currentCapacity = 0;
}