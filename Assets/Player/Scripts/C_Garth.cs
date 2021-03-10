using System;
using UnityEngine;

namespace HOM
{
    ///<summary> This class represents the main character </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class C_Garth : MonoBehaviour
    {
        public static C_Garth self {private set; get;}                // Singleton Reference
        CharacterController cc;

        /* MOVEMENT SETTIGS */
        internal CharacterMovementData movementSettings;
        internal float itemWeight = 0.0f;
        Vector3 gravityForce => movementSettings.m_gravityForce;
        Vector3 environmentForce => movementSettings.m_environmentForce;
        float maxSpeed => movementSettings.m_maxMovementSpeed;
        float maxAcceleration => movementSettings.m_maxAcceleration;
        float maxDeceleration => movementSettings.m_maxDeceleration;
        AnimationCurve accelerationCurve => movementSettings.m_accelerationCurve;
        AnimationCurve decelerationCurve => movementSettings.m_decelerationCurve;       
        float maxAccelerationTime => movementSettings.m_accelerationTime;
        float maxDecelerationTime => movementSettings.m_decelerationTime; 

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        #endregion

        void Initialize()
        {
            if(!self) self = this;
            if(!cc) cc = gameObject.GetComponent<CharacterController>();
            MovementHandler.SwapMovementSettings(this, MovementHandler.MS_BASE, () =>  MovementHandler.SetMovementSurface(self.gameObject, MovementHandler.S_Normal));
        }

        /* MOVEMENTS METHODS */

        ///<summary> Moves the character using his movement settigs <summary>
        public void Move()
        {
            var dir = MovementHandler.DeheightVector(MovementHandler.GetInputDirectionByCameraPosition());

            cc.Move(
                ((dir * (maxSpeed - itemWeight) * maxSpeed) + 
                gravityForce + environmentForce) * Time.deltaTime
            );
        }

        ///<summary> Accellerates character movement until the max accelleration is achieved</summary>
        ///<param name="elapsedTime"> Indicates the current accelleration in seconds</param>
        ///<param name="OnCompleted"> Callback called when the acceleration process has been completed </param>
        public void AccelerateCharacter(float elapsedTime, Action OnCompleted = null)
        {
            var dir = MovementHandler.DeheightVector(MovementHandler.GetInputDirectionByCameraPosition());
            
            if(elapsedTime < maxAccelerationTime)
            {
                cc.Move(
                    ((dir * accelerationCurve.Evaluate(elapsedTime) * maxAcceleration) + 
                    (gravityForce + environmentForce)) * Time.deltaTime);
            }
            else
            {
                OnCompleted?.Invoke();
            }

        }

        ///<summary> Decelerates character movement until max deceleration is achieved </summary>
        ///<param name="elapsedTime"> The current amount in seconds of time passed from deceleration's start </param>
        ///<param name="OnCompleted"> Callback called when the deceleration process has been completed </param>
        public void DecelerateCharacter(float elapsedTime, Action OnCompleted = null)
        {
             var dir = MovementHandler.DeheightVector(MovementHandler.GetInputDirectionByCameraPosition());

            if(elapsedTime < maxDecelerationTime)
            {
                cc.Move(
                    ((MovementHandler.GetLastInput() * decelerationCurve.Evaluate(elapsedTime) * maxDeceleration) + 
                        gravityForce + environmentForce) * Time.deltaTime);
            }
            else
            {
                OnCompleted?.Invoke();
            }
        }

    }
}
