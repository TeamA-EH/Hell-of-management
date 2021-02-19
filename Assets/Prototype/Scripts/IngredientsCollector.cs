using UnityEngine;
using System;

public class IngredientsCollector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] uint ingredientsCapacity = 3;
    public uint maxCapacity => ingredientsCapacity;

    uint redSouls = 0;
    uint greenSouls = 0;
    uint orangeSouls = 0;

    public uint RedSouls => redSouls;
    public uint GreenSouls => greenSouls;
    public uint OrangeSouls => orangeSouls;

    public uint containerWeight => redSouls + greenSouls + orangeSouls;
    public bool CanAddIngredients => containerWeight < ingredientsCapacity;

    /// <summary>
    /// Called when the collector achieves the max capacity
    /// </summary>
    public event Action OnMaxCapacityAchieved;

    /// <summary>
    /// Called when an ingredient is added to the collector
    /// </summary>
    public event Action<uint> OnIngredientAdded;

    public void AddIngredient(GameObject ingredient)
    {
        var ingredientDescription = ingredient.GetComponent<IngredientDescription>();

        if(ingredientDescription.Type == IngredientDescription.IngredientType.red_soul)
        {
            redSouls++;

            if(containerWeight == ingredientsCapacity)
            {
                OnMaxCapacityAchieved?.Invoke();
            }
        }
        else if (ingredientDescription.Type == IngredientDescription.IngredientType.green_soul)
        {
            greenSouls++;

            if (containerWeight == ingredientsCapacity)
            {
                OnMaxCapacityAchieved?.Invoke();
            }
        }
        else if (ingredientDescription.Type == IngredientDescription.IngredientType.blue_soul)
        {
            orangeSouls++;

            if (containerWeight == ingredientsCapacity)
            {
                OnMaxCapacityAchieved?.Invoke();
            }
        }
        OnIngredientAdded?.Invoke(containerWeight);
    }
    /// <summary>
    /// Reset all ingredients' collector to zero
    /// </summary>
    public void ResetCollector()
    {
        redSouls = 0;
        greenSouls = 0;
        orangeSouls = 0;
    }
}
