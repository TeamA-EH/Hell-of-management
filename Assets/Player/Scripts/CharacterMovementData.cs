using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName = "New Movement Data", menuName="Hell Of Management/Data/Character Movement Data")]
    public class CharacterMovementData : ScriptableObject
    {
        [Header("Settings"), Space(5)]
        [Tooltip("Indica in quanto tempo il personaggio completa una rotazione durante l'esecuzione")]
        [SerializeField] float rotationInterval = .25f;
        [Tooltip("Rappresenta la massima velocita che un personaggio puo raggiungere;")]
        [SerializeField] float maxMovementSpeed;
        [Tooltip("La costante gravitazione che spinge il giocatore sul terreno")]
        [SerializeField] Vector3 gravityForce = new Vector3(0,-2, 0);
        [Tooltip("Una forza aggiuntiva che viene applicata al movimento del personaggio(eg. vento/ghiaccio)")]
        [SerializeField] Vector3 environmentForce = Vector3.zero;

        [Header("Accelleration"), Space(10)]
        [Tooltip("indica l'ammontare di tempo in secondi in cui viene aggiunta l'accellerazione massima")]
        [SerializeField] float accellerationTime = 0;
        [Tooltip("indica l'andamento dell'accellerazione; modificare il valore del tempo con la stesso valore di [accellerationTime]")]
        [SerializeField] AnimationCurve accellerationCurve;
        [Tooltip("La massima accellerazione deve corrispondere ad un valore massimo che sia uguale a [MaxMovementSpeed]")]
        [SerializeField] float maxAcceleration = 1;

        [Header("Decelleration"), Space(10)]
        [Tooltip("Indica l'ammontare di tempo in secondi in viene raggiunta la decellerazione massima")]
        [SerializeField] float decelerationTime;
        [Tooltip("Indica la curva di decellerazione [VALORE MASSIMO: 1, TEMPO MASSIMO = decelerationTime]")]
        [SerializeField] AnimationCurve decelerationCurve;
        [Tooltip("Indica il massimo valore di decellerazione che il personaggio puo raggiungere")]
        [SerializeField] float maxDeceleration;

        /* DATA GETTERS */
        public float m_rotationInterval => rotationInterval;
        public float m_maxMovementSpeed => maxAcceleration;

        public float m_accelerationTime => accellerationTime;
        public float m_decelerationTime => decelerationTime;

        public float m_maxAcceleration => maxAcceleration;
        public float m_maxDeceleration => maxDeceleration;

        public AnimationCurve m_accelerationCurve => accellerationCurve;
        public AnimationCurve m_decelerationCurve => decelerationCurve;

        public Vector3 m_gravityForce => gravityForce;
        public Vector3 m_environmentForce => environmentForce;

    }
}

