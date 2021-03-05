using UnityEngine;
using HOM.Modules;

namespace HOM
{
   public class GUIHandler : MonoBehaviour{

       #region Unity Callbacks
        void Start(){

            DontDestroyOnLoad(this);
        }
       #endregion

        ///<summary> Activates one interface calling GUI module </summary>
        public static bool ActivatesMenu(string _menuCode)
        {
            var menu = GUIModule.GetMenues()[_menuCode];
            if(menu)
            {
                menu.GetComponent<Animator>().SetTrigger("Pop-In");
                menu.SetActive(true);
            }

            return false;
        }
        ///<summary> Deactivates one interface calling GUI module </summary>
        public static bool DeactivatesMenu(string _menuCode)
        {
            var menu = GUIModule.GetMenues()[_menuCode];
            if(menu)
            {
                menu.GetComponent<Animator>().SetTrigger("Pop-Out");
                menu.SetActive(true);

                return true;
            }

            return false;
        }
       
   }
}