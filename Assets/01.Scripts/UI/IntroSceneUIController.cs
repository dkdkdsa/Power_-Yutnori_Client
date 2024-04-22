using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneUIController : MonoBehaviour
{
    
    public void StartGame()
    {

        SceneManager.LoadScene("InGame_M 1");

    }

    public void ExitGame()
    {

        Application.Quit();

    }

}
