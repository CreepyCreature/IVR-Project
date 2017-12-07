using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnabledSaver : MonoBehaviour
{
    private string name_;
    private bool visible_ = true;
    private SceneController scene_controller_;

	// Use this for initialization
	void Start()
    {
        GetComponent<Destroy>().OnDeactivation += SetInactive;

        scene_controller_ = FindObjectOfType<SceneController>();
        if (!scene_controller_)
        {
            throw new UnityException(
                "Scene Controller could not be found, it should exist in the Persistent scene!"
                );
        }

        name_ = gameObject.name + "::Enable::Scene" + SceneManager.GetActiveScene().buildIndex;

        Load();
    }

    private void SetInactive()
    {
        visible_ = false;
        Save();
    }

    private void Save()
    {
        GameState.Save(name_, visible_);
        Debug.Log("Saving " + name_ + ": " + visible_);
    }

    private void Load()
    {
        if (GameState.Load(name_, ref visible_))
            gameObject.SetActive(visible_);
    }
    
}
