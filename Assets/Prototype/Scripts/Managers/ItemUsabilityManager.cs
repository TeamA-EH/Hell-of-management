using UnityEngine;
using Unity.Jobs;

public class ItemUsabilityManager : MonoBehaviour
{
    public static ItemUsabilityManager GetInstance;
    public enum UsabilityModes { Drop, Throw}

    [Header("Settings")]
    [Tooltip("Definisce quale tasto utilizzare per abilitare la modalita di lancio")]
    [SerializeField] KeyCode ModeSwitchInput = KeyCode.LeftShift;
    [Space(20)]
    [Tooltip("La modalita di utilizzo degli oggetti che si sta utilizzando")]
    [SerializeField] UsabilityModes activeMode = UsabilityModes.Drop;
    public UsabilityModes ActiveMode
    {
        private set
        {
            activeMode = value;
        }
        get
        {
            return activeMode;
        }
    }

    #region UnityCallbacks
    private void Awake()
    {
        if (!GetInstance) GetInstance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(ModeSwitchInput))
        {
            switch (ActiveMode)
            {
                case UsabilityModes.Drop:
                    ActiveMode = UsabilityModes.Throw;
                    break;

                case UsabilityModes.Throw:
                    ActiveMode = UsabilityModes.Drop;
                    break;
            }
        }
    }
    #endregion
}
