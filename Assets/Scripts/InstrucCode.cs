using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstrucCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            SceneManager.LoadScene("SmallIntroScene");
        }
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
