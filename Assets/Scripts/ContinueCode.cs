using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueCode : MonoBehaviour
{
    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitPauseMenu()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
