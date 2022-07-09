using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour
{
    public void GameStart() 
    {
        SceneManager.LoadScene(1);
    }

    public void GoTitle() 
    {
        SceneManager.LoadScene(0);
    }
    public void Credit()
    {
        SceneManager.LoadScene(2);
    }
    public void GameQuit() 
    {
        Application.Quit();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
