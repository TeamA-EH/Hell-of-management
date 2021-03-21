using UnityEngine;

namespace HOM
{
    public sealed class Chair : MonoBehaviour
    {
        [Tooltip(" Activatable GFX for male and female customers ")]
        [SerializeField] GameObject maleCustomerGFX, femaleCustomerGFX;
        public bool IsLocked {private set; get;} = false;

        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
        
        public enum CustomerType {NONE = -1, MALE, FEMALE}
        CustomerType customerType;

        ///<summary> Sets the customer type </summary>
        ///<param name="type"> The customer type (Male, Female, etc.) </param>
        public void SetCustomerType(CustomerType type) => customerType = type;

        ///<summary> TODO: Activates sitted customer graphics on this chair </summary>
        public void EnableCustomerGFX()
        {
            switch(customerType)
            {
                case CustomerType.MALE: 

                    maleCustomerGFX.SetActive(true);

                break;
                case CustomerType.FEMALE:

                    femaleCustomerGFX.SetActive(true);

                break;
            }
        }

        ///<summary> TODO: Deactivates sitted customer graphics </summary>
        public void DisableCustomerGFX()
        {

        }

        void OnGraphicsEnabled()
        {

        }

        void OnGraphicsDisabled()
        {

        }
    }
}
