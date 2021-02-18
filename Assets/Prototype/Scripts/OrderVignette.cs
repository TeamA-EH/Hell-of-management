using UnityEngine;
using UnityEngine.UI;

public class OrderVignette : MonoBehaviour
{
    [Space(5)]
    [SerializeField] GameObject m_firstSoulsContainer;
    [SerializeField] GameObject m_secondSoulsContainer;
    [SerializeField] GameObject m_thirdSoulsContainer;
    [Space(5)]
    [SerializeField] GameObject m_recipeContainer;

    /*
     * Queste variabili contengono una lista delle immagini listate per ogni elemento
     * L'indice corrisponde all'ordine gerarchico degli elementi all'interno dell'interfaccia
     * Souls: Red = 0, Green = 1, Orange = 2
     * Recipe: Drink = 0, Dish = 1
     */
    Image[] primaryElements;
    Image[] secondaryElements;
    Image[] lastElements;
    Image[] recipeElements;


    OrderRequest order;

    #region UnityCallbacks
    private void Awake()
    {
        primaryElements = m_firstSoulsContainer.GetComponentsInChildren<Image>();
        secondaryElements = m_secondSoulsContainer.GetComponentsInChildren<Image>();
        lastElements = m_thirdSoulsContainer.GetComponentsInChildren<Image>();

        recipeElements = m_recipeContainer.GetComponentsInChildren<Image>();

        ResetUI();

        Debug.Log($"elements: {primaryElements.Length}");
    }
    private void OnEnable()
    {
        //OrderManager.self.OnOrderCreated += OnOrderBringed;
        UpdateUI();
    }
    private void OnDisable()
    {
        //OrderManager.self.OnOrderCreated -= OnOrderBringed;
    }
    #endregion

    public void UpdateUI()
    {
        var orders = OrderManager.self.m_activeOrders;
        var currentOrder = orders[orders.Count - 1];

        var redSouls = currentOrder.redSouls;
        var greenSouls = currentOrder.greenSouls;
        var orangeSouls = currentOrder.orangeSouls;

        /* ACTIVATES INGREDIENTS ICONS */
        for (int i = 0; i < 3; i++)
        {
            if(i == 0)
            {
                if(redSouls > 0)
                {
                    primaryElements[0].gameObject.SetActive(true);

                    redSouls--;
                }
                else if(greenSouls > 0)
                {
                    primaryElements[1].gameObject.SetActive(true);

                    greenSouls--;
                }
                else if(orangeSouls > 0)
                {
                    primaryElements[2].gameObject.SetActive(true);

                    orangeSouls--;
                }
            }
            else if (i == 1)
            {
                if (redSouls > 0)
                {
                    secondaryElements[0].gameObject.SetActive(true);

                    redSouls--;
                }
                else if (greenSouls > 0)
                {
                    secondaryElements[1].gameObject.SetActive(true);

                    greenSouls--;
                }
                else if (orangeSouls > 0)
                {
                    secondaryElements[2].gameObject.SetActive(true);

                    orangeSouls--;
                }
            }
            else if (i == 2)
            {
                if (redSouls > 0)
                {
                    lastElements[0].gameObject.SetActive(true);

                    redSouls--;
                }
                else if (greenSouls > 0)
                {
                    lastElements[1].gameObject.SetActive(true);

                    greenSouls--;
                }
                else if (orangeSouls > 0)
                {
                    lastElements[2].gameObject.SetActive(true);

                    orangeSouls--;
                }
            }

        }

        /* ACTIVATES RECIPE ICON*/
        if(currentOrder.orderType == OrderRequest.OrderType.Cocktail)
        {
            recipeElements[0].gameObject.SetActive(true);
        }
        else
        {
            recipeElements[1].gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// Hide all vignette elements
    /// </summary>
    public void ResetUI()
    {
        foreach(var item in primaryElements)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in secondaryElements)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in lastElements)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in recipeElements)
        {
            item.gameObject.SetActive(false);
        }

    }
}