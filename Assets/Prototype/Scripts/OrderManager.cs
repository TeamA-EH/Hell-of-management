using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class OrderManager : MonoBehaviour
{
    [Header("Orders Settings"), Space(20)]
    [Tooltip("Indica il tempo minimo che si deve aspettare prima che venga creato un nuovo ordine")]
    [SerializeField] float minOrderSpawnRate = 30;
    [Tooltip("Indica il tempo massimo che si deve aspettare prima che venga creato un nuovo ordine")]
    [SerializeField] float maxOrderSpawnRate = 120;
    [Tooltip("Indica quanti ordini saranno disposti in questo livello")]
    [SerializeField] uint totalLevelOrders = 5;
    [Tooltip("Indica quanti secondi attendere prima che la sequenza di ordini venga spawnata")]
    [SerializeField] float timeBeforeStart = 5;
    bool canSpawn;
    public List<OrderRequest> activeOrders { private set; get; } = new List<OrderRequest>();

    [System.Serializable]
    struct OrderTemplate
    {
        public string name;
        public uint redSouls;
        public uint greenSouls;
        public uint orangeSouls;
    }
    [Tooltip("Una lista di ordini che possono capitare al giocatore nel gioco")]
    [SerializeField] [Space(20)] OrderTemplate[] orderTemplates = new OrderTemplate[1];

    [Space(20)]
    [Tooltip("Template della scheda dell'ordine")]
    [SerializeField] GameObject orderThumbnailPrefab;

    public void CreateOrder()
    {
        var orderChoice = orderTemplates[Random.Range(0, orderTemplates.Length - 1)];
        activeOrders.Add(new OrderRequest(orderChoice.redSouls, orderChoice.greenSouls, orderChoice.orangeSouls));

        OrdersBlackboard.GetInstance.AddOrderThumbnail(
            orderThumbnailPrefab,
            orderChoice.name,
            orderChoice.redSouls,
            orderChoice.greenSouls,
            orderChoice.orangeSouls);
    }

    IEnumerator SpawnOrderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CreateOrder();
    }
    IEnumerator SpawnOrders()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        CreateOrder();

        for(int i = 0; i<totalLevelOrders; i++)
        {
            var time = Random.Range(minOrderSpawnRate, maxOrderSpawnRate);
            yield return new WaitForSeconds(time);
            CreateOrder();
        }

        Debug.Log("Day Completed");
    }
    #region Events
    public delegate void OrderCreationEventHandler(OrderManager sender, int redSouls, int greenSouls, int orangeSouls);
    public delegate void OrderCompletationEventHandler(OrderManager sender, int rating);

    public static event OrderCreationEventHandler OnOrderCreated;
    public static event OrderCompletationEventHandler OnOrderCompleted;
    #endregion

    #region UnityCallbacks
    private void Start()
    {
        /*
         * All'inizio del gioco aspetta 5 secondi prima di creare il primo ordine
         */
        StartCoroutine(SpawnOrders());
    }
    #endregion
}