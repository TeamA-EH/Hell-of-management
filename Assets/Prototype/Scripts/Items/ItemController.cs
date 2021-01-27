using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Settings"), Space(20)]
    [Tooltip("il peso di un oggetto affligge il movimento del giocatore")]
    [SerializeField] float weight;

    private void OnMouseDown()
    {
        Debug.Log("DRAGME");
    }
}