using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public enum MovementState { Idle, Movement, Dash}
    CharacterController cc;

    [Header("Movement Settings"), Space(20)]
    [Tooltip("La massima velocita durante il movimento base")]
    [SerializeField] float maxMovementSpeed;

    [Header("Dash Settings"), Space(20)]
    [Tooltip("The key for activating player's dash")]
    [SerializeField] KeyCode dashKey = KeyCode.Space;
    [Tooltip("La massima velocita durante il dash")]
    [SerializeField] float maxDashSpeed;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 3f;
    public bool dashEnabled { private set; get; } = true;

    float maxSpeed;
    public float MaxSpeed
    {
        private set
        {
            if(value == 0)
            {
                ChangeMovementState(MovementState.Idle);
            }
            else if(value == maxMovementSpeed)
            {
                ChangeMovementState(MovementState.Movement);
            }
            else if(value == maxDashSpeed)
            {
                ChangeMovementState(MovementState.Dash);
            }

            maxSpeed = value;
        }
        get
        {
            return maxSpeed;
        }
    }
    float decellaration;

    Vector3 direction;
    Vector3 inputDirection = Vector3.zero;
    public MovementState movementState { private set; get; }
    public Vector3 playerVelocity => new Vector3(Input.GetAxis("Horizontal") * inputDirection.x, 0, Input.GetAxis("Vertical") * inputDirection.z);
    public float playerSpeed
    {
        get
        {
            var speed = (inputDirection.magnitude * maxSpeed) - decellaration;
            return Mathf.Clamp(speed, 0, maxSpeed);
        }
    }

    #region Events
    /// <summary>
    /// Called when the current movement state changes
    /// </summary>
    public event Action<PlayerMovement, MovementState> OnMovementStateChanged;
    /// <summary>
    /// Called when the player is dashing
    /// </summary>
    public event Action OnDash;
    /// <summary>
    /// Called when player is moving
    /// </summary>
    public event Action OnMovement;
    #endregion

    #region UnityCallbacks
    private void Start()
    {
        cc = GetComponent<CharacterController>();

        maxSpeed = maxMovementSpeed;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashEnabled) StartCoroutine(Dash());

        Vector3 vertical = Vector3.zero;
        Vector3 horizontal = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) vertical =           Camera.main.transform.forward;
        if (Input.GetKey(KeyCode.S)) vertical =           -Camera.main.transform.forward;
        
        if (Input.GetKey(KeyCode.D)) horizontal =           Camera.main.transform.right;
        if (Input.GetKey(KeyCode.A)) horizontal =           -Camera.main.transform.right;

        inputDirection = vertical + horizontal;

        Debug.Log(inputDirection.normalized);
    }
    #endregion

    public void UpdateMovement()
    {
        cc.Move((new Vector3(inputDirection.x, 0, inputDirection.z) - new Vector3(0,9.81f,0)) * playerSpeed * Time.deltaTime);

        OnMovement?.Invoke();
    }
    void ChangeMovementState(MovementState _state)
    {
        movementState = _state;

        OnMovementStateChanged?.Invoke(this, movementState);
    }

    IEnumerator Dash()
    {
        maxSpeed = maxDashSpeed;
        dashEnabled = false;

        OnDash?.Invoke();               // Invokes dash event
        yield return new WaitForSeconds(dashDuration);

        maxSpeed = maxMovementSpeed;

        yield return new WaitForSeconds(dashCooldown);
        dashEnabled = true;
    }
}
