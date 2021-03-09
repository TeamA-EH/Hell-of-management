using System.Collections.Generic;
using UnityEngine;

namespace HOM.Modules
{
    ///<summary> This class contains a list of methods for optimizing the injenction process for creating and/or destroy a linked effect during run-time </summary>
    internal sealed class VFXModule : MonoBehaviour
    {
        public static VFXModule self {private set; get;}                              //Reference to this class
        Dictionary<uint, List<GameObject>> effects = null;      //Linked effects of this module
        [SerializeField] VFXModuleData moduleData;              //Settings for this moudule

        /* EFFECTS UNIQUE IDs*/
        internal uint VFX_SMOKECLOUD = 0x000000;          //Rappresenta l'effetto simile a una nuova di fumo quando il personaggio cammina
        internal uint VFX_DROPLETS = 0x000001;            //Sono gocce che cadono sul terreno
        internal uint VFX_SOUL_LIGHT = 0x000002;          //L'effetto luminescenza di tutte le anime del gioco
        internal uint VFX_FIRE = 0x000003;                //Effetto del fuoco, usato principalmente sulle corna di lucifero
        internal uint VFX_GLOW = 0x000004;                //Un semplice GLOW effect
        internal uint VFX_WETAREA = 0x000005;             //Effetto di una zona bagnata
        internal uint VFX_FLOATING_BUBBLES = 0x000006;    //Delle bolle che ascendono, principalmente usato per i demoni ubriachi

        //Currents stacks of the all effects 
        //the stack represents instances presents into the module's collection
        //
        uint cloudAmounts = 0;
        uint dropletsAmount = 0;
        uint soulLightAmount = 0;
        uint fireAmount = 0;
        uint glowAmount = 0;
        uint wetAreaAmount = 0;
        uint floatingBubblesAmount = 0;
        
        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        #endregion

        ///<summary> Initializes VFX MODULE, links effect resources, references and collections </summary>
        void Initialize()
        {
            DontDestroyOnLoad(this);       // Avoid to destroy this object during scene transitions

            /* Setup singleton reference*/
            if(!self) self = this;

            /* Initialize effects collection */
            effects = new Dictionary<uint, List<GameObject>>();
            InitEffectResource(VFX_DROPLETS, moduleData.m_dropletsStack, moduleData.m_dropletsPref);
            InitEffectResource(VFX_FIRE, moduleData.m_fireStack, moduleData.m_firePref);
            InitEffectResource(VFX_FLOATING_BUBBLES, moduleData.m_floatingBubblesStack, moduleData.m_floatingBubblesPref);
            InitEffectResource(VFX_GLOW, moduleData.m_glowStack, moduleData.m_glowPref);
            InitEffectResource(VFX_SMOKECLOUD, moduleData.m_smokeyCloudStack, moduleData.m_smokeyCloudPref);
            InitEffectResource(VFX_SOUL_LIGHT, moduleData.m_soulLightStack, moduleData.m_soulLightPref);
            InitEffectResource(VFX_WETAREA, moduleData.m_wetAreaStack, moduleData.m_wetAreaPref);

            /* Updates stacks count for each effect managed(collected) by the VFX MODULE */
            dropletsAmount = GetEffectAmount(VFX_DROPLETS);
            fireAmount = GetEffectAmount(VFX_FIRE);
            floatingBubblesAmount = GetEffectAmount(VFX_FLOATING_BUBBLES);
            glowAmount = GetEffectAmount(VFX_GLOW);
            cloudAmounts = GetEffectAmount(VFX_SMOKECLOUD);
            soulLightAmount = GetEffectAmount(VFX_SOUL_LIGHT);
            wetAreaAmount = GetEffectAmount(VFX_WETAREA);
        }
        ///<summary> Initializes and links a new stack of effect resources to the VFX MODULE </summary>
        ///<param name="id"> represents the unique id for this effect </param>
        ///<param name="stack"> indicates the max amount of effects which can be actives at the same time </param>
        ///<param name="asset"> the particle prefab </param>
        void InitEffectResource(uint id, uint stack, GameObject asset)
        {
            if(!asset)      // Error handling
            {
                Debug.LogError($"{gameObject.name} failed to initialize {asset.name} with id {id}");
                return;
            }

            effects[id] = new List<GameObject>(); // Allocates new memory buffers on heap

            /* Creates the effects stack */
            for(int i = 0; i < stack; i++)
            {
                var effect = Instantiate(asset, gameObject.transform);
                effect.SetActive(false);
                SetEffectResource(id, ref effect);
            }

            Debug.Log(effects[id].Count);
        }

        ///<summary> Link a new effect to the module resources </summary>
        ///<param name="type"> the effect unique identifier </param>
        ///<param name="effect"> effect reference to link to the module </param>
        void SetEffectResource(uint type, ref GameObject effect) => effects[type].Add(effect);

        ///<summary> Returns the amount of effects managed from the VFX MODULE </summary>
        ///<param name="effectId"> the unique id of the effects to find </param>
        ///<return> amount of effects of the given id managed from the VFX MODULE </return>
        uint GetEffectAmount(uint effectId) => (uint)effects[effectId].Count;

        ///<summary> Returns all the effects of the given identifier managed by the VFX MODULE </summary>
        ///<param name="effect"> the effect unique identifier(eg. VFX_FIRE)</param>
        public GameObject[] GetEffectCollection(uint effect) => effects[effect].ToArray();

        ///<summary> Returns the amaount of active module's effects </summary>
        ///<param name="identifier"> the effect unique identifier itself </param>
        public uint GetActiveEffectsAmount(uint identifier)
        {
            uint counter = 0;
            foreach(var item in GetEffectCollection(identifier))
            {
                if(item.activeSelf) counter++;
            }

            return counter;
        }

        ///<summart> Try to get the first available effect from the module stack; Returns NULL if fails. </summary>
        ///<param name="effect"> the effect uniques identifier(eg.VFX_FIRE) </param>
        public GameObject GetFirstEffectAvailable(uint effect)
        {
            foreach(var buffer in GetEffectCollection(effect))
            {
                if(!buffer.activeSelf) return buffer;
            }

            return null;
        }

        ///<summary> Reallocates a new stack of memory for a managed effect </summary>
        ///<param name="type"> the effect unique identifier </param>
        ///<param name="newStack"> surplus of memory to allocate </param>
        internal void RecalculateEffectBufferMemory(uint type, uint newStack)
        {
            GameObject buffer = null;

            if(type == VFX_DROPLETS)
            {
                for (int i = 0; i < newStack; i++)
                {
                    buffer = Instantiate(moduleData.m_dropletsPref, gameObject.transform);
                    buffer.SetActive(false);
                    SetEffectResource(type, ref buffer);
                }

                dropletsAmount += newStack;
            }
            else if(type == VFX_FIRE)
            {
                for (int i = 0; i < newStack; i++)
                {
                    buffer = Instantiate(moduleData.m_firePref, gameObject.transform);
                    buffer.SetActive(false);
                    SetEffectResource(type, ref buffer);
                }

                fireAmount += newStack;
            }
            else if(type == VFX_FLOATING_BUBBLES)
            {
                for (int i = 0; i < newStack; i++)
                {
                    buffer = Instantiate(moduleData.m_floatingBubblesPref, gameObject.transform);
                    buffer.SetActive(false);
                    SetEffectResource(type, ref buffer);
                }

                floatingBubblesAmount += newStack;
            }
            else if(type == VFX_GLOW)
            {
                for (int i = 0; i < newStack; i++)
                {
                    buffer = Instantiate(moduleData.m_glowPref, gameObject.transform);
                    buffer.SetActive(false);
                    SetEffectResource(type, ref buffer);
                }

                glowAmount += newStack;
            }
            else if(type == VFX_SOUL_LIGHT)
            {
                for (int i = 0; i < newStack; i++)
                {
                    buffer = Instantiate(moduleData.m_soulLightPref, gameObject.transform);
                    buffer.SetActive(false);
                    SetEffectResource(type, ref buffer);
                }

                soulLightAmount += newStack;
            }
            else if(type == VFX_WETAREA)
            {
                for (int i = 0; i < newStack; i++)
                {
                    buffer = Instantiate(moduleData.m_wetAreaPref, gameObject.transform);
                    buffer.SetActive(false);
                    SetEffectResource(type, ref buffer);
                }

                wetAreaAmount += newStack;
            }
            else    // Error Handling
            {
                Debug.LogError($"VFX Module can't find this effect({type})");
            }

            buffer = null;
        }

    }
}