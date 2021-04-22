using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName = "New Iteraction Data", menuName = "Hell Of Management/Data/Iteraction Data")]
    public class ItemIteractionData : ScriptableObject
    {
        [Header("Settings"), Space(10)]
        [Tooltip("Represents the max distance from main character to iteract with a game object")]
        [SerializeField] [Range(.1f, 500)] float distanceFromPlayer;
        [Tooltip("Represents the shader program to compute while this object is hided from another one")]
        [SerializeField] Material farDistanceShader;
        [Tooltip("Represents the shader to compute when the object is highlighted/hovered")]
        [SerializeField] Material highlightShader;

        [Tooltip("Represent the shader to compute when an object is iteractable")]
        [SerializeField] Material iteractableShader;

        /* DATA GETTERS */
        public float DistanceFromPlayer => distanceFromPlayer;
        ///<summary> Represents the shader program to compute while this object is hided from another one </summary>
        public Material FarDistanceShader => farDistanceShader;
        ///<summary> Represents the shader program to compute when mouse cursor is hovering the object </summary>
        public Material HighlightShader => highlightShader;
        ///<summary> Returns the shader program to compute when an object is iteractable </summary>
        public Material IteractableShader => iteractableShader;
    }
}
