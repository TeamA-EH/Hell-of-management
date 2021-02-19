using UnityEngine;

public class KeepInTheSpawnZone : MonoBehaviour
{
    [SerializeField] private GameObject spawnZone;
    private GameObject mainCharacter;

    private void Awake()
    {
        SearchForMainCharacter();
    }

    private void SearchForMainCharacter()
    {
        mainCharacter = FindObjectOfType<MCController>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && other.transform.parent != mainCharacter.transform) other.transform.SetParent(spawnZone.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingredient")) other.transform.SetParent(null);
    }
}
