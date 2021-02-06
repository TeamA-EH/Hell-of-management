using UnityEngine;

[RequireComponent(typeof(TrashbagDetector))]
[RequireComponent(typeof(TrashbagDestroyer))]
public class DumpsterController : MonoBehaviour
{
    TrashbagDetector detector;
    TrashbagDestroyer destroyer;

    #region UnityCallbacks
    private void Start()
    {
        if (!detector) detector = GetComponent<TrashbagDetector>();
        if (!destroyer) destroyer = GetComponent<TrashbagDestroyer>();

        detector.OnTrashbagDetected += OnTrashbagDetected;
    }
    #endregion

    protected void OnTrashbagDetected(GameObject trashbag)
    {
        destroyer.DestroyTrashbag(trashbag);
    }
}