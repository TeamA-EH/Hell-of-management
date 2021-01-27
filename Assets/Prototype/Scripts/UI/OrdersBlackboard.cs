using UnityEngine;

public sealed class OrdersBlackboard : MonoBehaviour
{
    public static OrdersBlackboard GetInstance;

    private void Awake()
    {
        if (!GetInstance) GetInstance = this;
    }

    public void AddOrderThumbnail(GameObject thumbnail, string name, uint r, uint g, uint o)
    {
        var item = Instantiate<GameObject>(thumbnail);
        item.transform.SetParent(gameObject.transform);
        item.GetComponent<OrderThumbnail>().SetThumbnailInfos(name, r, g, o);
    }
}