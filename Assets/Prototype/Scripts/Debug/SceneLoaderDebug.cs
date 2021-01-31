using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderDebug : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("DesignTesting");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("DesignTestingIsometric");
        }
    }
}
