using System.Collections;
using UnityEngine;

public class SpeedModifier : MonoBehaviour, IModifier
{
    [SerializeField] float modifierSpeed;
    [SerializeField] float duration;
    PlayerMovement movement;

    public float value => modifierSpeed;

    public void Modify(GameObject gameObject, float value)
    {
        movement = gameObject.GetComponent <PlayerMovement> ();
        movement.MaxSpeed = value;
    }

    IEnumerator modifySpeed()
    {
        movement.MaxSpeed = modifierSpeed;
        yield return new WaitForSeconds(duration);

        movement.MaxSpeed = movement.maxMovementSpeed;
    }
}
