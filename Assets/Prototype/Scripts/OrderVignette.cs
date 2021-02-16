using UnityEngine;
using UnityEngine.UI;

public class OrderVignette : MonoBehaviour
{
    [Space(5)]
    [SerializeField] Text m_orderTypeText;
    [SerializeField] Text m_redSoulsText;
    [SerializeField] Text m_greenSoulsText;
    [SerializeField] Text m_orangeSoulsText;

    OrderRequest order;

    #region UnityCallbacks

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

        m_orderTypeText.text = orders[orders.Count - 1].orderType.ToString();
        m_redSoulsText.text = orders[orders.Count - 1].redSouls.ToString();
        m_greenSoulsText.text = orders[orders.Count - 1].greenSouls.ToString();
        m_orangeSoulsText.text = orders[orders.Count - 1].orangeSouls.ToString();
    }
}