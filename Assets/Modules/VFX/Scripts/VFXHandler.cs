using System;
using System.Collections;
using UnityEngine;

namespace HOM
{
    public sealed class VFXHandler : MonoBehaviour
    {
        static VFXHandler self = null;

        #region Unity Callbacks
        void Start()
        {
            DontDestroyOnLoad(this);        // Avoid to destroy this object during scene transitions

            if(!self) self = this;          // Defines singleton reference
        }
        #endregion

        /// <summary> Creates or activates an effect from the VFX MODULE</summary>
        /// <param name="effect"> the effect itself - eg.VFX_DROPLETS></param>
        /// <param name="lifeTime"> indicates the amount of time in seconds where the effect lives; lifeTime = 0 means persistant(no life-cycle) effect </param>
        /// <param name="position"> the spawn point for the effect </param>
        /// <param name="orientation"> the effect rotation offset </param>
        /// <param name="color"> the effect visible color at runtime </param>
        public static GameObject CreateEffect(uint effect, uint lifeTime, Vector3 position, Quaternion orientation)
        {
            var module = Modules.VFXModule.self;
            GameObject vfx = null;
            /* Validate effect static call for invokation */
            uint actives = module.GetActiveEffectsAmount(effect);
            if(!(actives + 1 <= module.GetEffectCollection(effect).Length)) // Check if the next operation will overhead the allowed buffer size
            {
                module.RecalculateEffectBufferMemory(effect, 5);
                vfx = module.GetFirstEffectAvailable(effect);
                self.ActivateEffect(ref vfx, position, orientation, (fx) => {
                    if(!(lifeTime == 0)) self.StartCoroutine(self.DeactivateEffectAfterDelay(lifeTime, fx));
                });
            }
            else    // Activates VFX normaly
            {
                vfx = module.GetFirstEffectAvailable(effect);
                self.ActivateEffect(ref vfx, position, orientation, (fx) => {
                    if(!(lifeTime == 0)) self.StartCoroutine(self.DeactivateEffectAfterDelay(lifeTime, fx));
                });
            }

            module = null;
            return vfx;
        }

        ///<Summary> Activates the linked effect </summary>
        ///<param name="OnActivation"> callback called when the effect is just activated </param>
        void ActivateEffect(ref GameObject vfx, Vector3 position, Quaternion orientation, Action<GameObject> OnActivation = null)
        {
            vfx.SetActive(true);
            vfx.transform.position = position;
            vfx.transform.rotation = orientation;

            OnActivation?.Invoke(vfx);
        }
        
        ///<summary> Deactivates the linked effect </summary>
        ///<param name="OnDeactivation"> Callback called when the effect is just deactivated(one time call) </param>
        void DeactivateEffect(ref GameObject vfx, Action OnDeactivation = null)
        {
            vfx.SetActive(false);

            OnDeactivation?.Invoke();
        }

        ///<summary> Deactivates the referred effect after an amount of time </summary>
        ///<param name="delay"> The amount of time in seconds to wait before deactivate the effect </param>
        ///<param name="effect"> The effect to deactivate </param>
        IEnumerator DeactivateEffectAfterDelay(float delay, GameObject effect)
        {
            yield return new WaitForSeconds(delay);
            DeactivateEffect(ref effect);
        }
    }
}