using UnityEngine;

public class StorageRoomSpawner : MonoBehaviour
{
    [SerializeField] private bool isInside;
    private bool activateOnce;
    [SerializeField] private GameObject[] possibleIngredients;
    [SerializeField] private GameObject spawnZone;
    [SerializeField] private int spawnLimit;
    [SerializeField] private float xSpawnRange;
    [SerializeField] private float zSpawnRange;

    private void Start()
    {
        activateOnce = false;
    }

    private void Update()
    {
        if (activateOnce == true)
        {
            ManageSouls();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activateOnce = true;
        }
    }

    void ManageSouls()
    {
        if (isInside == true) Spawn();
        else Despawn();
        activateOnce = false;
    }

    void Spawn()
    {
        if (spawnZone.transform.childCount == 0)
        {
            for (int i = 0; i < spawnLimit; i++)
            {
                int randomIngredient = Random.Range(0, possibleIngredients.Length);
                float randomXPos = Random.Range(-xSpawnRange, xSpawnRange);
                float randomZPos = Random.Range(-zSpawnRange, zSpawnRange);
                Vector3 randomPos = new Vector3(spawnZone.transform.position.x + randomXPos, spawnZone.transform.position.y, spawnZone.transform.position.z + randomZPos);
                Instantiate(possibleIngredients[randomIngredient], randomPos, spawnZone.transform.rotation, spawnZone.transform);
            }
        }
    }

    void Despawn()
    {
        if (spawnZone.transform.childCount > 0)
        {
            foreach (Transform child in spawnZone.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

}
