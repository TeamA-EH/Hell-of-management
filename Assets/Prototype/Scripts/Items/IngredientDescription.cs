using UnityEngine;

/// <summary>
/// Descrive la tipologia di ingrediente
/// </summary>
public class IngredientDescription : MonoBehaviour
{
    public enum IngredientType { red_soul, green_soul, orange_soul}

    [Header("Description Settings"), Space(20)]
    [Tooltip("Definisce la tipologia dell'ingrediente [Red/Green/Orange]")]
    [SerializeField] IngredientType description = IngredientType.red_soul;
    public IngredientType Type => description;

}