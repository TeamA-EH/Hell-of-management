using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsCollectorProgressUI : MonoBehaviour
{
    [SerializeField] IngredientsCollector ingredientCollector;
    public Text ingredientsAmount;

    private void Start()
    {
        ingredientsAmount.text = $"{ingredientCollector.containerWeight}/{ingredientCollector.maxCapacity}";
    }

    private void OnEnable()
    {
        ingredientCollector.OnIngredientAdded += OnIngredientAdded;
    }

    void OnIngredientAdded(uint storedIngredients)
    {
        ingredientsAmount.text = $"{storedIngredients}/{ingredientCollector.maxCapacity}";
    }


}
