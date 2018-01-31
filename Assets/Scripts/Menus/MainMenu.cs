using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame ()
    {
        GameManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame ()
    {
        GameManager.Instance.ResetGameState();
        PlayGame();
    }

    public void QuitGame ()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
