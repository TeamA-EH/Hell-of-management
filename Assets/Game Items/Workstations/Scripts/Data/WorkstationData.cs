using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName = "New Workstation Data", menuName = "Hell Of Management/Data/Workstation Data")]
    public class WorkstationData : ScriptableObject
    {
        [Header("Settings"), Space(10)]
        [Tooltip("Represents the amount of data which can be stored by this workstation")]
        [SerializeField] uint containerSize = 3;
        [Tooltip("Representes the amount of time for precessing stored data")]
        [SerializeField] [Range(.1f, 10)] float processingTime = 1;

        public enum InputType {NONE = -1, EVERYTHING, SOUL}
        [Tooltip("Indicates the type of input collectable by this workstation")]
        [SerializeField] InputType input = InputType.NONE;
        public enum OutputType {NONE = -1, DISH, DRINK}
        [Tooltip("Indicates the type of output getted by this workstation")]
        [SerializeField] OutputType output = OutputType.NONE; 
        [Tooltip("The asset which will be spawn when the elaboration process stops")]
        [SerializeField] GameObject outputAsset;

        [Header("Machine Lights Data"), Space(10)]
        [SerializeField] Material defaultLightMaterial;
        [SerializeField] Material redSoulLightMaterial;
        [SerializeField] Material greenSoulLightMaterial;
        [SerializeField] Material blueSoulLightMaterial;
        [SerializeField] Material yellowSoulLightMaterial;
        [SerializeField] Material purpleSoulLightMaterial;

        /* GETTERS */
        public uint ContainerSize => containerSize;
        public float ProcessingTime => processingTime;

        public InputType Input => input;
        public OutputType Output => output;

        public GameObject OutputAsset => outputAsset;

        public Material DefaultLightMaterial => defaultLightMaterial;
        public Material RedSoulLightMaterial => redSoulLightMaterial;
        public Material GreenSoulLightMaterial => greenSoulLightMaterial;
        public Material BlueSoulLightMaterial => blueSoulLightMaterial;
        public Material YellowSoulLightMaterial => yellowSoulLightMaterial;
        public Material PurpleSoulLightMaterial => purpleSoulLightMaterial;
        
    }
}
