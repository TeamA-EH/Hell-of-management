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
        //modifier.Modify(player, modifier.value);
        player.GetComponent<PlayerMovement>().SetTerrainSurface(PlayerMovement.TerrainSurface.Ice);
        Debug.Log("enter");
    }

    void OnPlayerExit(GameObject player)
    {
        player.GetComponent<PlayerMovement>().SetTerrainSurface(PlayerMovement.TerrainSurface.Ground);
        //modifier.Modify(player, 0);
        Debug.Log("exit");
    }
}