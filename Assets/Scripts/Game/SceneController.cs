using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string starting_scene = "StartingScene";
    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;       

    private void Awake()
    {
       //DontDestroyOnLoad(transform.gameObject);
    }

    private IEnumerator Start()
    {
        yield return StartCoroutine(LoadSceneAndSetActive(starting_scene));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(SwitchScenes("AuxScene01"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            StartCoroutine(SwitchScenes(starting_scene));
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameState.Reset();
        }
    }       
        
    private IEnumerator SwitchScenes(string scene_name)
    {
        if (BeforeSceneUnload != null) BeforeSceneUnload();

        yield return SceneManager.UnloadSceneAsync(
            SceneManager.GetActiveScene().buildIndex
            );
        yield return StartCoroutine(LoadSceneAndSetActive(scene_name));

        if (AfterSceneLoad != null) AfterSceneLoad();
    }

    private IEnumerator LoadSceneAndSetActive(string scene_name)
    {
        yield return SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
        Scene new_scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(new_scene);
    }
}
