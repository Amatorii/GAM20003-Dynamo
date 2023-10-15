using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void SceneLoad(string load)
    {
        SceneManager.LoadScene(load);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
