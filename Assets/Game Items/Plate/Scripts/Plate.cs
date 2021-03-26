using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class Plate : MonoBehaviour, IRecipe
    {
        [Header("Settings"), Space(10)]
        [SerializeField] ItemIteractionData iteractionData;
        Rigidbody rb = null;
        Material OriginalMaterial;
        [Space(10)]
        [SerializeField] GameObject drinkGFX;
        [SerializeField] GameObject dishGFX;

       [SerializeField] uint type;
        public uint Type 
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }

        GameObject customer = null;
        public GameObject Customer => customer;

        uint redsouls;
        public uint RedSouls => redsouls;
        uint greensouls;
        public uint GreenSouls => greensouls;
        uint bluesouls;
        public uint BlueSouls => bluesouls;

        #region Unity Callbacks
        void OnMouseEnter()
        {
            float distance = (C_Garth.self.gameObject.transform.position - gameObject.transform.position).magnitude;
            if(distance <= iteractionData.DistanceFromPlayer)
            {
                gameObject.GetComponentInChildren<MeshRenderer>().material = iteractionData.HighlightShader;
            }
        }
        void Update()
        {
            if(Input.GetMouseButtonDown(0) && C_Garth.self.PlayerHands[0].m_canBind)
            {
                float distance = (C_Garth.self.gameObject.transform.position - gameObject.transform.position).magnitude;
                if(distance <= iteractionData.DistanceFromPlayer)
                {
                    //Send pickup request
                    SkillManager.SendPickupSkillRequest(gameObject, type + 6, C_Garth.self.gameObject, 0, null, null);
                }
            }

            if(Input.GetMouseButtonDown(1) && C_Garth.self.PlayerHands[1].m_canBind)
            {
                float distance = (C_Garth.self.gameObject.transform.position - gameObject.transform.position).magnitude;
                if(distance <= iteractionData.DistanceFromPlayer)
                {
                    //Send pickup request
                    SkillManager.SendPickupSkillRequest(gameObject, type + 6, C_Garth.self.gameObject, 1, null, null);
                }
            }
            
        }
        void OnMouseExit()
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = OriginalMaterial;
        }
        #endregion

        public void Init()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            OriginalMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
        }

        public void OverrideRecipeInfos(uint type, GameObject customer, uint redSouls, uint greenSouls, uint blueSouls)
        {
            this.type = type;
            this.customer = customer;
            this.redsouls = redSouls;
            this.greensouls = greenSouls;
            this.bluesouls = blueSouls;
        }

        public void UpdateGFX()
        {
            if(type == 1)
            {
                dishGFX.SetActive(true);
            }
            else if(type == 2)
            {
                drinkGFX.SetActive(true);
            }
        }

        public void ResetGFX()
        {
            dishGFX.SetActive(false);
            drinkGFX.SetActive(false);
        }

        #region Physic Simulation
        public void EnablePhysics()
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        public void DisablePhysics()
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        public void SetForce(Vector3 force)
        {
            rb.AddForce(force, ForceMode.Impulse);
        }
        #endregion

    }
}