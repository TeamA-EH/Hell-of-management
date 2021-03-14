using System;
using UnityEngine;
using DG.Tweening;

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
                ((dir * (maxSpeed - itemWeight)) + 
                gravityForce + environmentForce) * Time.deltaTime
            );
        }
        public void Move(float speed)
        {
            var dir  = MovementHandler.DeheightVector(MovementHandler.GetInputDirectionByCameraPosition());

            cc.Move(
                ((MovementHandler.GetLastInput() * (speed - itemWeight)) + (gravityForce + environmentForce)) * Time.deltaTime
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

        ///<summary> Rotates character toward the mouse position </summary>
        ///<param name="duration"> amount on time in seconds to complete rotation </param>
        ///<param name="clockwise"> If TRUE calculate the angle using a frequency of 360 degrees otherwise calculate a signe angle (between -180, 180 degrees) </param>
        ///<param name="OnRotationCompleted"> Callback called when rotation has been completed </param>
        public void RotateCharacter(float duration, bool clockwise = true, Action OnRotationCompleted = null)
        {
            Vector3 mouseDirection = Vector3.zero;

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(r, out hit, 500F))
            {
                mouseDirection = (hit.point - gameObject.transform.position).normalized;
            }

            float angle = 0.0F;

            /* CALCULATES ANGLE */
            if(clockwise) //360 degs frequency
            {
                float dot = Vector3.Dot(Vector3.forward, mouseDirection);
                float length = Vector3.forward.magnitude * mouseDirection.magnitude;

                if(hit.point.x > gameObject.transform.position.x)
                {
                    angle = Mathf.Acos(dot/length)*Mathf.Rad2Deg;
                }
                else
                {
                    angle = 360 - Mathf.Acos(dot/length) * Mathf.Rad2Deg;
                }
            }
            else // signed angle
            {
                angle = Vector3.SignedAngle(Vector3.forward, mouseDirection.normalized, Vector3.up);
            }

            /* Rotate Character */
            gameObject.transform.DORotate(new Vector3(0, angle, 0), duration)
            .OnComplete(() => {
                OnRotationCompleted?.Invoke();
            });
            
        }

    }
}
