using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulsStorage : MonoBehaviour
    {
        public static SoulsStorage self = null;
        public List<GameObject> storedSouls {private set; get;} = new List<GameObject>();

        
        GameObject[] points;       //Lista dei punti di spawn per le anime
        List<GameObject> availablePoints = new List<GameObject>();

        [Header("Red Souls"), Space(10)]
        [SerializeField] [Range(0, 36)]uint minRedSoulsCount = 2;
        [SerializeField] [Range(0, 36)] uint maxRedSoulsCount = 2;
        [Header("Green Souls"), Space(10)]
        [SerializeField] [Range(0, 36)] uint minGreenSoulsCount = 2;
        [SerializeField] [Range(0, 36)] uint maxGreenSoulsAmount = 2;
        [Header("Blue Souls"), Space(10)]
        [SerializeField] [Range(0, 36)] uint minBlueSoulsCount = 2;
        [SerializeField] [Range(0,36)] uint maxBlueSoulsCount = 2;
        [Header("Yellow Souls"),Space(10)]
        [SerializeField] [Range(0,36)] uint minYellowSoulsCount = 2;
        [SerializeField] [Range(0,36)] uint maxYellowSoulsCount = 2;
        [Header("Purple Souls"), Space(10)]
        [SerializeField] [Range(0,36)] uint minPurpleSoulsCount = 2;
        [SerializeField] [Range(0,36)] uint maxPurpleSoulsCount = 2; 


        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.GetComponent<C_Garth>())
            {
                RestoreAvailablePoints();
                SpawnSoulsInStorage();
            }
            else if(collider.gameObject.GetComponent<Soul>())
            {
                collider.gameObject.GetComponent<Soul>().SetEnvironment(true);
                storedSouls.Add(collider.gameObject);//!@brief: verificare il funzionamento prima di committar il cambiamento
            }
        }
        void OnTriggerExit(Collider collider)
        {
            if(collider.gameObject.GetComponent<C_Garth>())
            {
                if(self.storedSouls.Count > 0)
                {
                    for(int i = 0; i < self.storedSouls.Count; i++)
                    {
                        SoulsManager.DestroySoul(self.storedSouls[i]);
                    }

                    self.storedSouls.Clear();
                }
            }
            else if(collider.gameObject.GetComponent<Soul>())
            {
                collider.gameObject.GetComponent<Soul>().SetEnvironment(false);
            }
        }
        #endregion

        void Initialize()
        {
            if(!self) self = this;

            points = new GameObject[36];    

            /*COLLECT CURRENT SPAWN POINTS*/
            for(int i = 0; i < 36; i++)
            {
                points[i] = GameObject.Find($"Point ({i+1})");
            }

            for(int i = 0; i < points.Length; i++)      //Initializes the list of points available for the spawn
            {
                availablePoints.Add(points[i]);
            }

            /*
            * ###ERROR HANDLING###
            * Se il numero di anime massime supera il limite di punti di spawn disponibili
            * allora logga l'asserzione nella console con le informazioni.
            */
            if(maxRedSoulsCount + maxGreenSoulsAmount + maxBlueSoulsCount + maxYellowSoulsCount + maxPurpleSoulsCount > 36)
            {
                Debug.LogAssertion($"Attention! Object [{gameObject.name}] initialization failed - Error: the souls which could be spawned exceed the max spawn limit [36]!");
            }
        }

        public static void SpawnSouls(uint soulTag, uint count)
        {
            for(int i = 0; i < count; i++)
            {
                var spawn = self.availablePoints[UnityEngine.Random.Range(0, self.availablePoints.Count)];
                var soul = SoulsManager.CreatesSoul(soulTag, spawn.transform.position);
                soul.GetComponent<Soul>().Init();
                soul.GetComponent<Soul>().DeactivatesAgent();
                soul.gameObject.transform.position = spawn.transform.position;
                soul.GetComponent<Soul>().ActivatesAgent();
                soul.GetComponent<Soul>().SetEnvironment(true);
                soul.GetComponent<Soul>().ExecuteBehaviourTree();

                self.storedSouls.Add(soul);
                self.availablePoints.Remove(spawn);
            }
        }

        ///<summary>Resets to default vaule the available spawn points</summary>
        public void RestoreAvailablePoints()
        {
            for(int i = 0; i < points.Length; i++)
            {
                availablePoints.Add(points[i]);
            }
        }

        public static void SpawnSoulsInStorage()
        {
            
            SpawnSouls(SoulsManager.SOUL_TAG_RED, (uint)UnityEngine.Random.Range(self.minRedSoulsCount, self.maxRedSoulsCount));
            SpawnSouls(SoulsManager.SOUL_TAG_GREEN, (uint)UnityEngine.Random.Range(self.minGreenSoulsCount, self.maxGreenSoulsAmount));
            SpawnSouls(SoulsManager.SOUL_TAG_BLUE, (uint)UnityEngine.Random.Range(self.minBlueSoulsCount, self.maxBlueSoulsCount));
            SpawnSouls(SoulsManager.SOUL_TAG_YELLOW, (uint)UnityEngine.Random.Range(self.minYellowSoulsCount, self.maxYellowSoulsCount));
            SpawnSouls(SoulsManager.SOUL_TAG_PURPLE, (uint)UnityEngine.Random.Range(self.minPurpleSoulsCount, self.maxPurpleSoulsCount));
            
        }
    }
}