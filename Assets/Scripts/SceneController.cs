using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public string starting_scene = "StartingScene";

    private void Awake()
    {
       //DontDestroyOnLoad(transform.gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Switching Scene");
            StartCoroutine(SwitchScenes("AuxScene01"));
            //SceneManager.LoadSceneAsync("AuxScene01");
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("Switching Scene");
            StartCoroutine(SwitchScenes(starting_scene));
            //SceneManager.LoadSceneAsync(starting_scene);
        }
    }       

    private IEnumerator Start()
    {
        yield return StartCoroutine(LoadSceneAndSetActive(starting_scene));
        //yield return null;
    }
        
    private IEnumerator SwitchScenes(string scene_name)
    {
        yield return SceneManager.UnloadSceneAsync(
            SceneManager.GetActiveScene().buildIndex
            );
        Debug.Log("Unloaded Scene");
        yield return StartCoroutine(LoadSceneAndSetActive(scene_name));
    }

    private IEnumerator LoadSceneAndSetActive(string scene_name)
    {
        Debug.Log("Loading Scene");
        yield return SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
        Scene new_scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(new_scene);
    }
}
