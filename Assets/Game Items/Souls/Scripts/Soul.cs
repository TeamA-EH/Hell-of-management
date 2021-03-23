
using System;
using UnityEngine;

namespace HOM
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Soul : Item
    {
        [Header("Settings"), Space(10)]
        [SerializeField] GameObject green_soul_mesh;
        [SerializeField] GameObject blue_soul_mesh;
        [SerializeField] GameObject red_soul_mesh;

        bool canIteract => (gameObject.transform.position - C_Garth.self.gameObject.transform.position).magnitude <= iteractionData.DistanceFromPlayer;
        Material originalMaterial = null;
        Rigidbody rb;

        uint m_tag = 0;
        public uint Tag 
        {
            set
            {
                m_tag = value;
                OnTagChanged(value);
            }
            get
            {
                return m_tag;
            }
        }


        #region  Unity Callbacks
        void Start()
        {
            Init();
        }
        void Update()
        {
            if(canIteract)
            {
               
                uint type = 0;
                if(Tag == SoulsManager.SOUL_TAG_RED) type = 0;
                else if(Tag == SoulsManager.SOUL_TAG_GREEN) type = 1;
                else if(Tag == SoulsManager.SOUL_TAG_BLUE) type = 2;

                if(Input.GetMouseButtonDown(0) && GetSelectedSoul() != null)
                {
                    var selection = GetSelectedSoul();
                    SoulsStorage.self.storedSouls.Remove(selection);
                    SkillManager.SendPickupSkillRequest(GetSelectedSoul(), type, C_Garth.self.gameObject, 0, () => MovementHandler.DisableCharacterRotation(C_Garth.self.gameObject), null);
                }
                else if(Input.GetMouseButtonDown(1) && GetSelectedSoul() != null)
                {
                     var selection = GetSelectedSoul();
                    SoulsStorage.self.storedSouls.Remove(selection);
                    SkillManager.SendPickupSkillRequest(GetSelectedSoul(), type, C_Garth.self.gameObject, 1, () => MovementHandler.DisableCharacterRotation(C_Garth.self.gameObject), null);
                }
            }
        }
        void OnMouseEnter()
        {
            if(canIteract)
            {
                ChangeMaterial(iteractionData.HighlightShader);
            }
        }
        void OnMouseExit()
        {
            if(gameObject.GetComponentInChildren<MeshRenderer>().material != originalMaterial)
            {
                ChangeMaterial(originalMaterial);
            }
        }
        #endregion

        public void Init()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            originalMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
        }

        ///<summary> Changes the mesh material </summary>
        ///<param name="newMat"> The new mesh material </param>
        void ChangeMaterial(Material newMat)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = newMat;
        }

        ///<summary> Get the soul selected from the mouse cursor </summary>
        GameObject GetSelectedSoul()
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit))
            {
                if(hit.collider.gameObject.GetComponent<Soul>())
                {
                    return hit.collider.gameObject;       
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        ///<summary> Bind methods for being called when the soul tag changes </summary>
        ///<param name="tag"> The new tag </param>
        void OnTagChanged(uint tag)
        {
            SwapMeshLayer(tag);
        }

        ///<summary> Swaps displayed mesh using unique tags  </summary>
        ///<param name="tag"> The unique tag for the soul object </param>
        void SwapMeshLayer(uint tag)
        {
            switch(tag)
            {
                case SoulsManager.SOUL_TAG_GREEN:

                green_soul_mesh.SetActive(true);
                blue_soul_mesh.SetActive(false);
                red_soul_mesh.SetActive(false);

                break;
                case SoulsManager.SOUL_TAG_BLUE:

                green_soul_mesh.SetActive(false);
                blue_soul_mesh.SetActive(true);
                red_soul_mesh.SetActive(false);

                break;
                case SoulsManager.SOUL_TAG_RED:

                green_soul_mesh.SetActive(false);
                blue_soul_mesh.SetActive(false);
                red_soul_mesh.SetActive(true);

                break;
                default:
                Debug.LogError("Soul Tag Error!");
                break;
            }
        }

        public void EnablePhysics()
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        public void DisablePhysic()
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        ///<summary> Set the rigidbody velocity attached to this object </summary>
        ///<param name="velocity"> Value to set </param>
        public void SetVelocity(Vector3 velocity)
        {
            rb.velocity = velocity;
        }

        public void SetForce(Vector3 force)
        {
            Debug.Log($"rigidbody: {rb}");
            rb.AddForce(force, ForceMode.Impulse);
        }

    }
}