using UnityEngine;

[RequireComponent(typeof(PlayerDetector))]
[RequireComponent(typeof(Terrains))]
public class SurfaceController : MonoBehaviour
{
    PlayerDetector detector;
    Terrains terrains;

    private void Awake()
    {
        detector = gameObject.GetComponent<PlayerDetector>();
        terrains = gameObject.GetComponent<Terrains>();
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
        if (terrains._surfaceType == Terrains.ESurfaceType.Icy)
        {
            player.GetComponent<PlayerMovement>().SetTerrainSurface(PlayerMovement.ESurfaceType.Icy);
            Debug.Log("ice");
        }

        if (terrains._surfaceType == Terrains.ESurfaceType.Sticky)
        {
            player.GetComponent<PlayerMovement>().SetTerrainSurface(PlayerMovement.ESurfaceType.Sticky);
            Debug.Log("glue");
        }
    }

    void OnPlayerExit(GameObject player)
    {
        player.GetComponent<PlayerMovement>().SetTerrainSurface(PlayerMovement.ESurfaceType.Defaulty);
        Debug.Log("default");
    }
}