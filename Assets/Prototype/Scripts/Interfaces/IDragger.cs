using UnityEngine;
public interface IDragger
{
    /// <summary>
    /// Descrive l'operazione di dragging
    /// </summary>
    /// <param name="item"></param>
    void Drag(GameObject item, float dragTime);
}