using UnityEngine;
using HOM.Modules;

namespace HOM
{
   public class GUIHandler : MonoBehaviour{

       #region Unity Callbacks
        void Start(){

            //DontDestroyOnLoad(this);
        }
       #endregion

        ///<summary> Activates one interface calling GUI module </summary>
        public static bool ActivatesMenu(string _menuName)
        {
            var menu = GUIModule.GetMenues()[_menuName];
            if(menu)
            {
                menu.SetActive(true);
                //menu.GetComponent<Animator>().SetTrigger("Pop-In");
                Debug.Log("trigger activated!");
            }

            return false;
        }
        ///<summary> Deactivates one interface calling GUI module </summary>
        public static bool DeactivatesMenu(string _menuName)
        {
            var menu = GUIModule.GetMenues()[_menuName];
            if(menu)
            {
                //menu.GetComponent<Animator>().SetTrigger("Pop-Out"); --> IMPLEMENTARE SUCCESSIVAMENTE (Alessio)
                menu.SetActive(false);

                return true;
            }

            return false;
        }
       
   }
}