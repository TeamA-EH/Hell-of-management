using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class Restart : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //LevelManager.instance.ReloadGame();
            }
        }
    }
}
