using UnityEngine;

namespace HOM.Modules
{
    [CreateAssetMenu(fileName = "New VFXModule Settings", menuName = "Hell Of Management/Data/VFX Module Settings")]
    public class VFXModuleData : ScriptableObject
    {
        [Header("VFX SMOKE CLOUD"), Space(10)]
        [SerializeField] GameObject smokeCloudAsset;
        [SerializeField] uint smokeCloudStack;
        
        [Header("VFX DROPLETS"), Space(10)]
        [SerializeField] GameObject dropletsAsset;
        [SerializeField] uint dropletsStack;

        [Header("VFX SOUL LIGHT"), Space(10)]
        [SerializeField] GameObject soulLightAsset;
        [SerializeField] uint soulLightStack;

        [Header("VFX FIRE"), Space(10)]
        [SerializeField] GameObject fireAsset;
        [SerializeField] uint fireStack;

        [Header("VFX GLOW"), Space(10)]
        [SerializeField] GameObject glowAsset;
        [SerializeField] uint glowStack;

        [Header("VFX WET AREA"), Space(10)]
        [SerializeField] GameObject wetAreaAsset;
        [SerializeField] uint wetAreaStack;

        [Header("VFX FLOATING BUBBLES"), Space(10)]
        [SerializeField] GameObject floatingBubblesAsset;
        [SerializeField] uint floatingBubblesStack;

        /* Assets getters */
        public GameObject m_smokeyCloudPref => smokeCloudAsset;
        public GameObject m_dropletsPref => dropletsAsset;
        public GameObject m_soulLightPref => soulLightAsset;
        public GameObject m_firePref => fireAsset;
        public GameObject m_glowPref => glowAsset;
        public GameObject m_wetAreaPref => wetAreaAsset;
        public GameObject m_floatingBubblesPref => floatingBubblesAsset;

        /* Stacks getters */
        public uint m_smokeyCloudStack => smokeCloudStack;
        public uint m_dropletsStack => dropletsStack;
        public uint m_soulLightStack => soulLightStack;
        public uint m_fireStack => fireStack;
        public uint m_glowStack => glowStack;
        public uint m_wetAreaStack => wetAreaStack;
        public uint m_floatingBubblesStack => floatingBubblesStack;
        

    }
}