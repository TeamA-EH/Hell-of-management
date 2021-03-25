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
            if(soul && soul.Tag != SoulsManager.SOUL_TAG_NULL)
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

        IEnumerator CraftPlate()
        {
            yield return new WaitForSeconds(m_data.ProcessingTime);
            ResetLights();
            //Create Plate
            collecteditems = 0;
        }
        
    }
}
