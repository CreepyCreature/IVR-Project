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

    public void LoadNewScene(int buildIndex)
    {
        StartCoroutine(SwitchScenes(buildIndex));
    }

    public void LoadNewScene(string sceneName)
    {
        StartCoroutine(SwitchScenes(sceneName));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(SwitchScenes("AuxScene01"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            StartCoroutine(SwitchScenes("StartingScene"));
        }
        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            StartCoroutine(SwitchScenes("MainMenu"));
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameState.Reset();
        }
    }

    // We have the same function(s) two times to support the addressing
    // of scenes both by the Build Index and the Scene Name

    private IEnumerator SwitchScenes(string sceneName)
    {
        if (BeforeSceneUnload != null) BeforeSceneUnload();

        yield return SceneManager.UnloadSceneAsync(
            SceneManager.GetActiveScene().buildIndex
            );
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        if (AfterSceneLoad != null) AfterSceneLoad();
    }

    private IEnumerator SwitchScenes(int buildIndex)
    {
        if (BeforeSceneUnload != null) BeforeSceneUnload();

        yield return SceneManager.UnloadSceneAsync(
            SceneManager.GetActiveScene().buildIndex
            );
        yield return StartCoroutine(LoadSceneAndSetActive(buildIndex));

        if (AfterSceneLoad != null) AfterSceneLoad();
    }

    private IEnumerator LoadSceneAndSetActive(string scene_name)
    {
        yield return SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
        Scene new_scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(new_scene);
    }

    private IEnumerator LoadSceneAndSetActive(int buildIndex)
    {
        yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        Scene new_scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(new_scene);
    }

}
