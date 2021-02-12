using System.Collections;
using UnityEngine;

public class SpeedModifier : MonoBehaviour, IModifier
{
    [SerializeField] float surfaceMass;
    PlayerMovement movement;

    public float value => surfaceMass;

    public void Modify(GameObject gameObject, float value)
    {
        movement = gameObject.GetComponent <PlayerMovement> ();
        movement.decellaration = value;
    }
}
