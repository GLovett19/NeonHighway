using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        ActiveSceneManager.LoadScene("MainMenu", true);
    }

    // Update is called once per frame
    void Update()
    {
        // quit at any time by pressing the escape key.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit App");
            Application.Quit();
        }

    }
}
