using UnityEngine;

public sealed class TargetLookAtGameObject : MonoBehaviour
{
    [Header("Settings"), Space(20)]
    [Tooltip("Indica l'oggetto di gioco verso cui l'oggetto a cui e' attaccato questo script deve guardare")]
    [SerializeField] GameObject cameraBoomObject;

    #region UnityCallbacks

    private void Update()
    {
        gameObject.transform.LookAt(cameraBoomObject.transform);
    }

    #endregion
}