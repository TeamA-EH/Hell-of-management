/*
** A class representing the game souls which can be used like ingredients 
** Copyright (C) 2021, Alessio Manna <alessiomanna.job@gmail.com>
** Copyright (C) 2021, Team A - Event Horizon srl
** All rights reserved.
*/
using System;
using UnityEngine;
using DG.Tweening;

namespace HOM
{
    [RequireComponent(typeof(Animator))]
    public class Soul : MonoBehaviour
    {
        [Header("Settings"), Space(10)]
        [SerializeField] GameObject green_soul_mesh;
        [SerializeField] GameObject blue_soul_mesh;
        [SerializeField] GameObject red_soul_mesh;

        Animator controller = null;

        uint m_tag = 0;
        public uint Tag 
        {
            set
            {
                OnTagChanged(value);
                m_tag = value;
            }
            get
            {
                return m_tag;
            }
        }


        #region  Unity Callbacks
        void Start()
        {
            Initialize();
        }
        #endregion

        void Initialize()
        {
            //SoulsManager.SetSoulTag(this, SoulsManager.SOUL_TAG_BLUE);
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
            }
        }

    }
}