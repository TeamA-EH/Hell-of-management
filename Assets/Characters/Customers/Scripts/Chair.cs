using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public sealed class Chair : MonoBehaviour
    {
        public bool IsLocked {private set; get;} = false;

        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;


        ///<summary> TODO: Activates sitted customer graphics on this chair </summary>
        public void EnableCustomerGFX(bool isMale)
        {

        }

        ///<summary> TODO: Deactivates sitted customer graphics </summary>
        public void DisableCustomerGFX()
        {

        }
    }
}
