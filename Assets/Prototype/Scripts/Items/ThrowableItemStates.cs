using UnityEngine;
using System;

public class ThrowableItemStates : MonoBehaviour
{
    public enum States { None, Grounded, Floating}
    public States state { private set; get; } = States.Floating;

    /// <summary>
    /// Called when the object state changes
    /// </summary>
    public event Action<States> OnStateChanged;
    /// <summary>
    /// Called every frame when the object is setted as GROUNDED
    /// </summary>
    public event Action OnGroundedState;
    /// <summary>
    /// Called every frame when the state is setted as FLOATING
    /// </summary>
    public event Action OnFloatingState;

    #region UnityCallbacks
    private void Update()
    {
        switch (state)
        {
            case States.Grounded:

                OnGroundedState?.Invoke();

                break;
            case States.Floating:

                OnFloatingState?.Invoke();

                break;
        }
    }
    #endregion

    /// <summary>
    /// Change the object current state
    /// </summary>
    /// <param name="_state"></param>
    public void ChangeState(States _state)
    {
        state = _state;

        OnStateChanged?.Invoke(state);
    }

}