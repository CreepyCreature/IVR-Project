using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        if (!sceneController)
        {
            throw new UnityException(
                "Scene Controller could not be found, it should exist in the Persistent scene!"
                );
        }
        sceneController.LoadNewScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
