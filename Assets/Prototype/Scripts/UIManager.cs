using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject storageMenu;
    [SerializeField] GameObject OrderMenu;

    public void SetStorageMenuVisibility(bool visibility) => storageMenu.SetActive(visibility);
}
