using UnityEngine;
using UnityEngine.UI;

namespace HOM
{
    public class OrderVignetteUI : MonoBehaviour
    {
        [SerializeField] GameObject backgroundPanel;
        [Space(10)]
        [SerializeField] GameObject firsSoulContainer;
        [SerializeField] GameObject secondSoulContainer;
        [SerializeField] GameObject thirdSoulContainer;
        [Space(10)]
        [SerializeField] GameObject orderTypeContainer;

        Image[] fIcons = new Image[5]; 
        Image[] sIcons = new Image[5]; 
        Image[] tIcons = new Image[5]; 
        Image[] typeIcons = new Image[2];
 
        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        #endregion

        void Initialize()
        {
            //gameObject.transform.LookAt(Camera.main.transform, Camera.main.transform.up);

            if(firsSoulContainer)           SetIcons(ref firsSoulContainer, ref fIcons);
            if(secondSoulContainer)         SetIcons(ref secondSoulContainer, ref sIcons);
            if(thirdSoulContainer)          SetIcons(ref thirdSoulContainer, ref tIcons);
            if(orderTypeContainer)          SetIcons(ref orderTypeContainer, ref typeIcons);

            ResetIcons();
        }

        ///<summary> Activates order vignette panel and icons </summary>
        public void Activate()
        {
            backgroundPanel.SetActive(true);
        }

        ///<summary> Deactivates order vignette panel and background icons </summary>
        public void Deactivate()
        {
            backgroundPanel.SetActive(false);
        }

        public void UpdateInterface(uint type, uint redSouls, uint greenSouls, uint blueSouls, uint yellowSouls, uint purpleSouls)
        {
            uint layer = 0;

            /* SHOW INGREDIENTS ICONS */
            if(redSouls > 0)
            {
                for (int i = 0; i < redSouls; i++)
                {
                    layer++;
                    
                    var container = GetIconsByLayer(layer);
                    EnableIcon(ref container, 0);
                }
            }

            if(greenSouls > 0)
            {
                for (int i = 0; i < greenSouls; i++)
                {
                    layer++;
                    
                    var container = GetIconsByLayer(layer);
                    EnableIcon(ref container, 1);
                }
            }

            if(blueSouls > 0)
            {
                for (int i = 0; i < blueSouls; i++)
                {
                    layer++;
                    
                    var container = GetIconsByLayer(layer);
                    EnableIcon(ref container, 2);
                }
            }

            if(yellowSouls > 0)
            {
                for(int i = 0; i < yellowSouls; i++)
                {
                    layer++;

                    var container = GetIconsByLayer(layer);
                    EnableIcon(ref container, 3);
                }
            }

            if(purpleSouls > 0)
            {
                for(int i = 0; i < purpleSouls; i++)
                {
                    layer++;

                    var container = GetIconsByLayer(layer);
                    EnableIcon(ref container, 4);
                }
            }

            /* SHOW TYPE ICON */
            EnableIcon(ref typeIcons, type - 1);
        }

        ///<summary> Set containers icons </summary>
        ///<param name="container"> The icons container </param>
        ///<param name="data"> icons to bind </param>
        void SetIcons(ref GameObject container, ref Image[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = container.GetComponentsInChildren<Image>()[i];
            }
        }

        ///<summary> Visualizes a contianed icon </summary>
        ///<param name="container"> the container which stored the icons </param>
        ///<param name="index"> the icon index </param>
        void EnableIcon(ref Image[] container, uint index)
        {
            container[index].color = Color.white;
        }
        
       ///<summary> Hides a contianed icon </summary>
        ///<param name="container"> the container which stored the icons </param>
        ///<param name="index"> the icon index </param>
        void DisableIcon(ref Image[] container, uint index)
        {
            container[index].color = Color.clear;
        }

        ///<summary> Returns icons container by layer </summary>
        ///<param name="layer"> The current layer </param>
        Image[] GetIconsByLayer(uint layer)
        {
            if(layer == 1) return fIcons;
            else if(layer == 2) return sIcons;
            else if(layer == 3) return tIcons;
            else return null;
        }

        ///<summary> Hides all soul icons </summary>
        public void ResetIcons()
        {
            /* RESETS FIRST SOUL ICONS */
            foreach(var item in fIcons)
            {
                item.color = Color.clear;
            }

            /* RESETS SECOND SOUL ICONS*/
            foreach(var item in sIcons)
            {
                item.color = Color.clear;
            }

            /* RESETS THIRD SOUL ICONS */
            foreach(var item in tIcons)
            {
                item.color = Color.clear;
            }

            /* RESETS ORDER TYPE ICONS */
            foreach(var item in typeIcons)
            {
                item.color = Color.clear;
            }
        }
    }
}
