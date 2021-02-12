using UnityEngine;

public interface IModifier 
{
    float value { get; }
    void Modify(GameObject gameobject, float value);
}
