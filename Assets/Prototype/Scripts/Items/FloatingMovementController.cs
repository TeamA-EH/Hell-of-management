using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ThrowableItemStates))]
public class FloatingMovementController : MonoBehaviour
{
    ThrowableItemStates states;
    Rigidbody rb;

    [Header("Settings"), Space(20)]
    [Tooltip("Indica a che distanza deve trovarsi dal terreno l'oggetto prima di cambiare stato")]
    [SerializeField] float maxDistanceFromGround;

    #region UnityCallbacks
    private void OnEnable()
    {
        if (!states) states = GetComponent<ThrowableItemStates>();
        if (!rb) rb = GetComponent<Rigidbody>();

        states.OnGroundedState += OnGroundedState;
        states.OnFloatingState += OnFloatingState;
    }
    #endregion

    void OnGroundedState()
    {

    }
    void OnFloatingState()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(r, out hit))
        {
            var distanceFromGround = (hit.point - gameObject.transform.position).magnitude;
            if(distanceFromGround <= .5f)
            {
                rb.useGravity = false;
                rb.isKinematic = true;

                states.ChangeState(ThrowableItemStates.States.Grounded);
            }
        }
    }
}