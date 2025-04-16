using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public GameObject tu;

    

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Tu()
    {
        tu.SetActive(true);
    }

    public void Back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");

    }
}
