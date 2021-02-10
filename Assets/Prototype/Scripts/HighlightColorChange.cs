using UnityEngine;

[AddComponentMenu("Prototype/Utilities/Highlight Color Parser")]
public class HighlightColorChange : MonoBehaviour
{
    [Space(20)]
    [Tooltip("Colore di default dell'oggetto")]
    [SerializeField] Color defaultColor;
    [Tooltip("Color che assume l'oggetto quando il cursore ci passa sopra")]
    [SerializeField] Color highlightColor;
    MeshRenderer mr;

    private void Start()
    {
        mr = gameObject.GetComponentInChildren<MeshRenderer>();
        mr.material.color = defaultColor;
    }

    private void OnMouseEnter()
    {
        mr.material.color = highlightColor;
    }
    private void OnMouseExit()
    {
        mr.material.color = defaultColor;
    }
}
