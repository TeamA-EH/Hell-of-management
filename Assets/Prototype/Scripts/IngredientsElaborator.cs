using UnityEngine;
using System;

public class IngredientsElaborator : MonoBehaviour
{
    public enum ElaboratorOutput { Cocktail, Dish}

    [Header("Elaborator Settings"), Space(20)]
    [SerializeField] ElaboratorOutput output = ElaboratorOutput.Cocktail;
    [SerializeField] GameObject outputAsset;

    public event Action<GameObject> OnIngredientsProcessed;

    public void ProcessIngredient(uint redSouls, uint greenSouls, uint orangeSouls)
    {
        switch (output)
        {
            case ElaboratorOutput.Cocktail:

                var cocktail = Instantiate(outputAsset);

                var cocktailIngredients = cocktail.GetComponent<IngredientsDescription>();
                cocktailIngredients.SetIngredientsQuantity(OrderRequest.OrderType.Cocktail,  redSouls, greenSouls, orangeSouls);

                cocktail.transform.position = gameObject.transform.position + new Vector3(0, 3, 0);

                OnIngredientsProcessed?.Invoke(null);

                break;
            case ElaboratorOutput.Dish:

                // Spawn Dish & Set Infos
                var dish = Instantiate(outputAsset);

                var dishIngredients = dish.GetComponent<IngredientsDescription>();
                dishIngredients.SetIngredientsQuantity(OrderRequest.OrderType.Dish, redSouls, greenSouls, orangeSouls);

                dish.transform.position = gameObject.transform.position + new Vector3(0, 3, 0);

                OnIngredientsProcessed?.Invoke(null);

                break;
        }

    }
}