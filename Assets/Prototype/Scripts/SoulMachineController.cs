using UnityEngine;

/// <summary>
/// Questa classe gestice il comporamento per craftare gli ordini richiesti dai clieni
/// - IngredientContainer: colleziona gli ingredienti dati in pasto al controller; quanto raggiunge la capienza massima li da in pasto all'elaboratore
/// - IngredientDetector: Manda un broadcast al controller per aggiungere il container
/// - IngredientElaborator: elabora gli ingredienti restituendo un output (cocktail o Dish)
/// </summary>
[RequireComponent(typeof(IngredientsCollector))]
[RequireComponent(typeof(IngredientDetector))]
[RequireComponent(typeof(IngredientsElaborator))]
public class SoulMachineController : MonoBehaviour
{
    IngredientDetector ingredientDetector;
    IngredientsCollector ingredientCollector;
    IngredientsElaborator ingredientElaborator;

    #region UnityCallbacks
    private void Start()
    {
        if (!ingredientDetector) ingredientDetector = GetComponent<IngredientDetector>();
        if (!ingredientCollector) ingredientCollector = GetComponent<IngredientsCollector>();
        if (!ingredientElaborator) ingredientElaborator = GetComponent<IngredientsElaborator>();

        /* EVENTS SUBSCRIPTION */
        ingredientDetector.OnIngredientDetected += OnIngredientDetected;
        ingredientCollector.OnMaxCapacityAchieved += OnCollectorCapacityAchieved;
        ingredientElaborator.OnIngredientsProcessed += OnIngredientsProcessed;
    }
    #endregion

    protected void OnIngredientDetected(GameObject ingredient)
    {
        // Broadcast di aggiunta al collettore
        if(ingredientCollector.CanAddIngredients)
        {
            ingredientCollector.AddIngredient(ingredient);
            Destroy(ingredient);
        }
    }
    protected void OnCollectorCapacityAchieved()
    {
        ingredientElaborator.ProcessIngredient(ingredientCollector.RedSouls, ingredientCollector.GreenSouls, ingredientCollector.OrangeSouls);
    }
    protected void OnIngredientsProcessed(GameObject output)
    {
        ingredientCollector.ResetCollector();
    }
}