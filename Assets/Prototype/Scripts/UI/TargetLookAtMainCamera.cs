using UnityEngine;

public sealed class TargetLookAtMainCamera : MonoBehaviour
{
    [Header("Settings"), Space(20)]
    [Tooltip("Indica l'oggetto di gioco verso cui l'oggetto a cui e' attaccato questo script deve guardare")]
     GameObject cameraBoomObject;

    #region UnityCallbacks
    private void Start()
    {
        cameraBoomObject = Camera.main.gameObject;
    }
    private void Update()
    {
        gameObject.transform.LookAt(cameraBoomObject.transform);
    }

    #endregion
}