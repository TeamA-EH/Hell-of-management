using UnityEngine;
using System;

[RequireComponent(typeof(WasteDetector))]
[RequireComponent(typeof(WasteCollector))]
[RequireComponent(typeof(WasteElaborator))]
public class TrashcanController : MonoBehaviour
{
    WasteDetector wasteDetector;
    WasteCollector wasteCollector;
    WasteElaborator wasteElaborator;

    #region UnityCallbakcs
    private void OnEnable()
    {
        /* GET COMPONENTS FROM PARENT OBJECT */
        if (!wasteDetector) wasteDetector = GetComponent<WasteDetector>();
        if (!wasteCollector) wasteCollector = GetComponent<WasteCollector>();
        if (!wasteElaborator) wasteElaborator = GetComponent<WasteElaborator>();

        /* SUBSCRIBE EVENTS */
        wasteDetector.OnWasteDetected += OnWasteDetected;

        wasteCollector.OnWasteAdded += OnWasteAdded;
        wasteCollector.OnMaxCapacityAchieved += OnMaxCapacityReached;

        wasteElaborator.OnTrashBagCreated += OnTrashBagCreated;
    }
    #endregion

    protected void OnWasteDetected(GameObject waste)
    {
        Destroy(waste);
        wasteCollector.AddWaste();
    }
    protected void OnWasteAdded()
    {
        
    }
    protected void OnMaxCapacityReached()
    {
        wasteElaborator.CreateTrashbag(gameObject.transform.forward, 3);
    }
    protected void OnTrashBagCreated(GameObject trashbag)
    {

    }
}