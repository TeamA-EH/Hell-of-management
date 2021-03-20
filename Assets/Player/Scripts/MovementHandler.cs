using System;
using UnityEngine;

namespace HOM
{
    public sealed class MovementHandler : MonoBehaviour
    {
        static MovementHandler self;
        [SerializeField] ItemWeightTable weightTable;
        public ItemWeightTable WeightTable => weightTable;

        Vector3 lastInput = Vector3.zero;

        [Header("Settings"), Space(10)]
        [SerializeField] CharacterMovementData baseMovementData;        // Settings movimento base
        [SerializeField] CharacterMovementData iceMovementData;         // Settings movimento sul ghiaccio
        [SerializeField] CharacterMovementData mudMovementData;         // Settings movimento sul fango

        /* MOVEMENTS SETTINGS UINQUES IDENTIFIERS */
        public static uint MS_BASE = 0x0000001;             // Movimento base
        public static uint MS_ICE = 0x0000002;              // Movimento sul ghiaccio
        public static uint MS_MUD = 0x0000003;              // Movimento sul fango

        /* SURFACES UNIQUES IDETIFIERS */
        public static uint S_Normal = 0x000000;              //La superficie di base dove si muove il personaggio
        public static uint S_Ice = 0x000001;                 //Superficie ghiacciata
        public static uint S_Mud = 0x000002;                 //Superficie rallentante
        const uint surface_size = 3;                         // memory slots limit for surface type

        #region Unity Callbacks
        void Start()
        {
            if(!self) self = this;
        }
        #endregion

        /* TRANSITIONS HANDLERS */

        #region Surface Handlers 
        ///<summary> Activates a transition to a new movement state </summary>
        ///<param name="character"> The subject who will be effected by new movement state </param>
        ///<param name="surface"> The surface uniques identifier(eg. NORMAL_SURFACE) </param>
        ///<param name="OnSurfaceSetted"> callback called when a new movemet transition is setted </param>
        public static void SetMovementSurface(GameObject character, uint surface, Action OnSurfaceSetted = null)
        {
            var controller = character.GetComponent<Animator>();

            if (surface > surface_size) // Error handling
            {
                Debug.LogError("Attention! invalid surface type!");
                return;
            }
            else
            {
                controller.SetInteger("Surface Type", (int)surface);
                OnSurfaceSetted?.Invoke();
            }
        }
        #endregion

        #region Weight Handlers
        ///<summary> TODO: Overrides the max speed of a character by new value </summary>
        ///<param name="character"> The character to change max speed </param>
        ///<param name="maxSpeed"> New value to set </param>
        ///<param name="OnMaxSpeedOverrided"> Callback called when the max speed is overrided </param>
        public static void OverrideCharacterMaxSpeed(GameObject character, float maxSpeed, Action<float> OnMaxSpeedOverrided = null)
        {
            
        }

        ///<summary> Overrides the current weight of a character by a new value</summary>
        ///<param name="character"> The character to change current weight </param>
        ///<param name="newWeight"> New value to set </param>
        ///<param name="OnCharacterWeightOverrided"> Callback called when the new character weight is setted </param>
        public static void OverrideCharacterWeight(GameObject character, float newWeight, Action<float> OnCharacterWeightOverrided = null)
        {
            character.GetComponent<C_Garth>().itemWeight = Mathf.Clamp(newWeight, 0, 10000);
            OnCharacterWeightOverrided?.Invoke(newWeight);
        }

        ///<summary> Increases the amount of current weight for Garth_Character </summary>
        ///<param name="player"> Garth reference </param>
        ///<param name="amount"> Value to add</param>
        ///<param name="OnCompleted">Callback called when the value has beenn added </param>
        public static void IncreaseItemWeight(C_Garth player, float amount, Action OnCompleted = null)
        {   
            player.itemWeight += amount;
            OnCompleted?.Invoke();
        }

        ///<summary> Decreases the amount of current weight for Garth_Character </summary>
        ///<param name="player"> Garth reference </param>
        ///<param name="amount"> Value to add</param>
        ///<param name="OnCompleted">Callback called when the value has been decreased </param>

        public static void DecreaseItemWeight(C_Garth player, float amount, Action OnCompleted = null)
        {
            player.itemWeight = Mathf.Clamp(player.itemWeight - amount, 0, 10000);
        }

        ///<summary> Returns the item weight </summary>
        ///<param name="type"> The item type:
        /// 0=INVALID ITEM 
        /// 1=RED SOUL 
        /// 2=GREEN SOUL 
        /// 3=BLUE SOUL 
        /// 4=TRASH 
        /// 5=TRASH BAG 
        /// 6=FEMALE DEMON 
        /// 7=MALE DEMON 
        ///</param>
        public static float GetWeight(uint type)
        {
            switch(type)
            {
                case 1: return self.weightTable.RedSoulWeight;
                case 2: return self.weightTable.GreenSoulWeight;
                case 3: return self.weightTable.BlueSoulWeight;
                case 4: return self.weightTable.TrashWeight;
                case 5: return self.weightTable.TrashbagWeight;
                case 6: return self.weightTable.FemaleDemonWeight;
                case 7: return self.weightTable.MaleDemonWeight;
                default: return 0;
            }
        }
        #endregion

        #region Movement Handlers
        ///<summary> Changes Movement Settings </summary>
        ///<param name="player"> Reference to Garth character </param>
        ///<param name="settings"> movement data uniques identifier(eg. MS_ICE) </param>
        ///<param name="OnCompleted"> Callback called when the settings swap has been completed </param>
        public static void SwapMovementSettings(C_Garth player, uint settings, Action OnCompleted = null)
        {
            if(settings == MS_BASE)
            {
                player.movementSettings = self.baseMovementData;
            } 
            else if(settings == MS_ICE)
            {
                player.movementSettings = self.iceMovementData;
            }
            else if(settings == MS_MUD)
            {
                player.movementSettings = self.mudMovementData;
            }
            else    // Error Handling
            {
                Debug.LogError("Attention! Movement data swapping failed!");
                return;
            }

            OnCompleted?.Invoke();
        }

        ///<summary> Return the direction vector translate by camera direction </summary>
        public static Vector3 GetInputDirectionByCameraPosition()
        {
            Vector3 h = Camera.main.transform.right * Input.GetAxis("Horizontal");
            Vector3 v = Camera.main.transform.forward * Input.GetAxis("Vertical");

           if((h+v).magnitude > 0.4F) self.RegisterLastUserInput(h + v);    //Register last input ONLY if the vector module si grater then error tollerance

            return self.RoundVectorComponents(h+v, 0.4f);
        }

        ///<summary> Removes the Y AXIS from the give vector (X, NULL, Z) </summary>
        public static Vector3 DeheightVector(Vector3 v) => new Vector3(v.x, 0, v.z);

        ///<summary> Register a vector containing last direction from keys which user pressed </summary>
        /// <param name="input"> Value to register </param>
        void RegisterLastUserInput(Vector3 input)
        {
            lastInput = RoundVectorComponents(input, 0.4f);
        }

        /// <summary> Returns the last movement input as a vector3 </summary>
        public static Vector3 GetLastInput() => self.lastInput;

        /// <summary> 
        /// Rounds each decimal value from the given vector for each vector components 
        /// with ONLY 2 possible value 1(x > error) or -1(x < error) 
        /// </summary>
        /// <param name="input"> Vector to round </param>
        /// <param name="error"> This value "saves" vector components from the value swap; </param>
        /// <param name="excludeYcomponent"> if TRUE avoid to round the Y value </param>
        /// <returns> the rounded vector </returns>
        Vector3 RoundVectorComponents(Vector3 input, float error, bool excludeYcomponent = true)
        {
            float x = 0;
            float y = 0;
            float z = 0;

            /* SWAP VALUE FOR X COMPONENT */
            if (input.x > error) x = 1;
            else if (input.x < -error) x = -1;
            else x = input.x;

            /* SWAP VALUE FOR Y COMPONENT */
            if(!excludeYcomponent)
            {
                if (input.y > error) y = 1;
                else if (input.y < -error) y = -1;
                else y = input.y;
            }
            else
            {
                y = 0;
            }

            /* SWAP VALUE FOR Z COMPONENT */
            if (input.z > error) z = 1;
            else if (input.z < -error) z = -1;
            else z = input.z;

            return new Vector3(x, y, z);
        }

        ///<summary> Activates rotation layer for the given GameObject </summary>
        ///<param name="character"> GameObject containing the animator with animation layer </param>
        public static void EnableCharacterRotation(GameObject character)
        {
            character.GetComponent<Animator>().SetBool("Can Rotate", true);
        }
        
        ///<summary> Deactivates rotation layer for the given GameObject </summary>
        ///<param name="character"> GameObject containing the animator with animation layer </param>
        public static void DisableCharacterRotation(GameObject character)
        {
            character.GetComponent<Animator>().SetBool("Can Rotate", false);
        }

        #endregion

        ///<summary> Returns TRUE if the user press any movement key otherwise FALSE </summary>
        public static bool AnyMovementKeyPressed()
        {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);
        }
    }
}