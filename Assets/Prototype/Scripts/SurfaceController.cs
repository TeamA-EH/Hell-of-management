using UnityEngine;

[RequireComponent(typeof(PlayerDetector))]
[RequireComponent(typeof(IModifier))]
public class SurfaceController : MonoBehaviour
{
    PlayerDetector detector;
    IModifier modifier;

    private void Awake()
    {
        detector = gameObject.GetComponent<PlayerDetector>();
        modifier = gameObject.GetComponent<IModifier>();
    }

    private void OnEnable()
    {
        detector.OnPlayerDetected += OnPlayerDetected;
        detector.OnPlayerExit += OnPlayerExit;
    }

    private void OnDisable()
    {
        detector.OnPlayerDetected -= OnPlayerDetected;
        detector.OnPlayerExit -= OnPlayerExit;
    }

    void OnPlayerDetected(GameObject player)
    {
        modifier.Modify(player, modifier.value);
        Debug.Log("enter");
    }

    void OnPlayerExit(GameObject player)
    {
        modifier.Modify(player, player.GetComponent<PlayerMovement>().maxMovementSpeed);
        Debug.Log("exit");
    }
}