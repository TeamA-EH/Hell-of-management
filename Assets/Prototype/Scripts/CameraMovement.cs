using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    [Header("Offsets"), Space(20)]
    [SerializeField]Transform isometricCamera;
    [SerializeField]Transform StorageCamera;

    [Header("Transition To Storage"), Space(20)]
    float storageTransitionDuration = 0.5f;

    [Header("Transition To Isometric view"), Space(20)]
    float IsometricTransitionDuration = 0.5f;

    public void TranslateToStorage()
    {
        gameObject.transform.DOMove(StorageCamera.position, 0.5f);
        gameObject.transform.DORotateQuaternion(StorageCamera.rotation, 0.5f);
    }
    public void TranslateToIsometric()
    {
        gameObject.transform.DOMove(isometricCamera.position, 0.5f);
        gameObject.transform.DORotateQuaternion(isometricCamera.rotation, 0.5f);
    }
}