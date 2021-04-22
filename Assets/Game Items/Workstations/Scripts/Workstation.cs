using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    ///<summary> The workstation is a machine who creates plates for customers after receiving enought data </summary>
    [RequireComponent(typeof(SphereCollider))]
    public class Workstation : MonoBehaviour
    {
        [SerializeField] WorkstationData m_data;
        [SerializeField] Vector3 outputOffset;
        [SerializeField] GameObject[] machineLights = new GameObject[3];
        protected Animator animator = null;
        [SerializeField] uint collecteditems = 0;
        uint[] items = new uint[3];
        bool canCollect => collecteditems < m_data.ContainerSize;
        ///<summray> The list of plates containetd by this workstation </summary>
        List<GameObject> Plates;

        #region Unity Callbacks
        protected void Start()
        {
            Init();
        }
        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(gameObject.transform.position + outputOffset, new Vector3(1f,.2f,1.5f));
        }
        protected void OnTriggerEnter(Collider collider)
        {
            var soul = collider.gameObject.GetComponent<Soul>();
            if(soul && soul.Tag != SoulsManager.SOUL_TAG_NULL && soul.SoulState == Soul.MachineState.FLOATING)
            {
                if(canCollect)
                {
                    AddItem(collider.gameObject);
                    return;
                }
            }
        }
        #endregion

        protected virtual void Init()
        {
            animator = gameObject.GetComponent<Animator>();
            collecteditems = 0;

            foreach(var rend in machineLights)
            {
                rend.GetComponent<MeshRenderer>().material = m_data.DefaultLightMaterial;
            }

            /* INITIALIZE PLATE ASSETS */
            Plates = new List<GameObject>();
            for(int i = 0; i < 5; i++)
            {
                var obj = Instantiate(m_data.OutputAsset, gameObject.transform);
                obj.transform.position = gameObject.transform.position + outputOffset;
                obj.SetActive(false);
                Plates.Add(obj);
            }
        }

        public void AddItem(GameObject item)
        {
            collecteditems++;
            items[collecteditems - 1] = item.GetComponent<Soul>().Tag;
            item.SetActive(false);
            OnItemAdded(ref item);
        }

        void OnItemAdded(ref GameObject item)
        {
            switch(items[collecteditems - 1])
            {
                case SoulsManager.SOUL_TAG_RED: 
                    machineLights[collecteditems - 1].GetComponent<MeshRenderer>().material = m_data.RedSoulLightMaterial;
                break;
                case SoulsManager.SOUL_TAG_GREEN:
                    machineLights[collecteditems - 1].GetComponent<MeshRenderer>().material = m_data.GreenSoulLightMaterial;
                break;
                case SoulsManager.SOUL_TAG_BLUE:
                    machineLights[collecteditems - 1].GetComponent<MeshRenderer>().material = m_data.BlueSoulLightMaterial;
                break;
                case SoulsManager.SOUL_TAG_YELLOW:
                        machineLights[collecteditems-1].GetComponent<MeshRenderer>().material = m_data.YellowSoulLightMaterial;
                break;
                case SoulsManager.SOUL_TAG_PURPLE:
                        machineLights[collecteditems-1].GetComponent<MeshRenderer>().material = m_data.PurpleSoulLightMaterial;
                break;
                default:
                    Debug.LogError($"Soul {item.name} hasn't correct tag! {items[collecteditems -1]}");
                break;
            }

            if(collecteditems == m_data.ContainerSize)
            {
                StartCoroutine(CraftPlate());
            }
        }

        void ResetLights()
        {
            foreach(var item in machineLights)
            {
                item.GetComponent<MeshRenderer>().material = m_data.DefaultLightMaterial;
            }
        }

        ///<summary> Transfers the recipe infos registred by this workstation to the plate <summary>
        void TransferCraftingInfos(ref GameObject plate)
        {
            Plate target = plate.gameObject.GetComponent<Plate>();
            
            uint type = 0;
            uint redSouls = 0;
            uint greenSouls = 0;
            uint blueSouls = 0;
            uint yellowSouls = 0;
            uint purpleSouls = 0;

            /* DEFINES OUTPUT TYPE */
            switch(m_data.Output)
            {
                case WorkstationData.OutputType.DISH:
                    type = 1;
                break;
                case WorkstationData.OutputType.DRINK:
                    type = 2;
                break;
            }

            /* CONSTRUCT INSERTED SOULS */
            foreach(var t in items)
            {
                if(t == SoulsManager.SOUL_TAG_RED)
                {
                    redSouls++;
                }
                else if(t == SoulsManager.SOUL_TAG_GREEN)
                {
                    greenSouls++;
                }
                else if(t == SoulsManager.SOUL_TAG_BLUE)
                {
                    blueSouls++;
                }
                else if(t == SoulsManager.SOUL_TAG_YELLOW)
                {
                    yellowSouls++;
                }
                else if(t == SoulsManager.SOUL_TAG_PURPLE)
                {
                    purpleSouls++;
                }
            }

            target.OverrideRecipeInfos(type, null, redSouls, greenSouls, blueSouls, yellowSouls, purpleSouls);
        }

        IEnumerator CraftPlate()
        {
            yield return new WaitForSeconds(m_data.ProcessingTime);
            ResetLights();

            var crafted = GetPlateFromList();
            crafted.SetActive(true);
            TransferCraftingInfos(ref crafted);
            crafted.GetComponent<Plate>().UpdateGFX();
            crafted.GetComponent<Plate>().Init();
            collecteditems = 0;
        }

        #region Plate Pull-Chain
        ///<summary> Increase the number of plates </summary>
        ///<param name="amount"> The new stack to instantiate </param>
        void IncreasePlateStack(uint amount)
        {
            for(int i = 0; i < amount; i++)
            {
                var obj = Instantiate(m_data.OutputAsset, gameObject.transform);
                obj.transform.position = gameObject.transform.position + outputOffset;
                obj.SetActive(false);
                Plates.Add(obj);
            }
        }
        ///<summary> If availbale returns the first available plate from the plate pool otherwise returns NULL </summmary>
        GameObject GetFirstAvailablePlate()
        {
            foreach(var item in Plates)
            {
                if(!item.activeSelf)
                {
                    return item;
                }
            }
            return null;
        }
        ///<summary> Return a plate from if available otherwise instantiate other plates and returns the first available </summary>
        GameObject GetPlateFromList()
        {
            var plate = GetFirstAvailablePlate();
            if(!plate)
            {
                IncreasePlateStack(5);
                plate = GetFirstAvailablePlate();
            }

            return plate;
        }
        #endregion
        
    }
}
