
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class CustomersManager : MonoBehaviour
    {
        static CustomersManager self = null;

        [Header("Settings"), Space(10)]
        [SerializeField] GameObject maleDemonAsset;
        [SerializeField] GameObject femaleDemonAsset;
        [Space(10)]
        [SerializeField] GameObject pubDoor;

        [Space(10)]
        [Tooltip("Definisce ogni quanti secondi bisogna controllare se ci sono sedie libere per spawnare un personaggio")]
        [SerializeField] float customerSpawnInterval = 5f;
        float currentTime = 0.0f;

        Chair[] levelChairs = null;
        List<GameObject> maleCustomers = null;
        List<GameObject> femaleCustomers = null;
        
        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        void FixedUpdate()
        {
            currentTime += Time.deltaTime;
            if(currentTime >= customerSpawnInterval)
            {
                if(AnyChairAvailable()) SpawnCustomer();

                currentTime = 0.0f;
            }
        }
        #endregion

        void Initialize()
        {
            if(!self) self = this;

            levelChairs = FindObjectsOfType<Chair>();
            
            /* Initialize Customers Stack */
            maleCustomers = new List<GameObject>();
            femaleCustomers = new List<GameObject>();

            IncreaseCustomerStack(maleDemonAsset, ref maleCustomers, 10);
            IncreaseCustomerStack(femaleDemonAsset, ref femaleCustomers, 10);
            
        }

        bool AnyChairAvailable()
        {
            foreach(Chair item in levelChairs)
            {
                if(!item.IsLocked) return true;
            }

            return false;   
        }

        Chair GetFirstAvailableChair()
        {
             foreach(Chair item in levelChairs)
            {
                if(!item.IsLocked) return item;
            }

            return null;   
        }

        ///<summary> Spawn randomically a female or male customer </summary>
        void SpawnCustomer()
        {
            uint dna = (uint)Random.Range(0, 2);
            if(dna == 0) SpawnMaleCustomer();
            else SpawnFemaleCustomer();
        }

        ///<summary> Increases the customers stack </summary>
        ///<param name="asset"> Prefab to instantiate </param>
        ///<param name="list"> The customer list </param>
        ///<param name="count"> The amount of customer to stack </param>
        void IncreaseCustomerStack(GameObject asset, ref List<GameObject> list, uint count)
        {
            for(int i = 0; i < count; i++)
            {
                GameObject customer = Instantiate(asset, gameObject.transform);
                customer.SetActive(false);
                list.Add(customer);
            }
        }

        ///<summary> Spawn a male customer from the male customer list </summary>
        GameObject SpawnMaleCustomer()
        {
           GameObject customer = GetFirstMaleCustomerAvailable();
           if(!customer) 
           {
               IncreaseCustomerStack(self.maleDemonAsset, ref maleCustomers, 10);
               customer = GetFirstMaleCustomerAvailable();
               return customer;
           }
           else return customer;
        }

        ///<summary> Spawn a female customer from the female customer list </summary>
        GameObject SpawnFemaleCustomer()
        {
            GameObject customer = GetFirstFemaleCustomerAvailable();
            if(!customer)
            {
                IncreaseCustomerStack(femaleDemonAsset, ref self.femaleCustomers, 10);
                customer = self.GetFirstFemaleCustomerAvailable();
                return customer;
            }
            else return customer;
        }

        ///<summary> Get the first available male character from the male customers list </summary>
        GameObject GetFirstMaleCustomerAvailable()
        {
            foreach(GameObject character in self.maleCustomers)
            {
                if(!character.activeSelf)
                {
                    character.SetActive(true);
                    character.transform.position = pubDoor.transform.position - pubDoor.transform.forward;

                    var chair = GetFirstAvailableChair();
                    chair.SetCustomerType(Chair.CustomerType.MALE);
                    chair.Lock();
                    character.GetComponent<Customer>().SetChair(chair);

                    character.GetComponent<Customer>().SetDoor(pubDoor);
                    character.GetComponent<Animator>().SetTrigger("Go To Chair");
                    return character;
                }
            }

            return null;
        }

        ///<summary> Get the first available female character from the female customers list </summary>
        GameObject GetFirstFemaleCustomerAvailable()
        {
            foreach(GameObject character in self.femaleCustomers)
            {
                if(!character.activeSelf)
                {
                    character.SetActive(true);
                    character.transform.position = self.pubDoor.transform.position - self.pubDoor.transform.forward;

                    var chair = GetFirstAvailableChair();
                    chair.SetCustomerType(Chair.CustomerType.FEMALE);
                    chair.Lock();     
                    character.GetComponent<Customer>().SetChair(chair);

                    character.GetComponent<Customer>().SetDoor(pubDoor);
                    character.GetComponent<Animator>().SetTrigger("Go To Chair");
                    return character;
                }
            }

            return null;
        }

        
    }
}
