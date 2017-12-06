using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisibilitySaver : MonoBehaviour
{
    private string name_;
    private bool visible_ = true;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Destroy>().OnDeactivation += Save;
        name_ = gameObject.name + "::Scene" + SceneManager.GetActiveScene().buildIndex;
        if (GameState.Load(name_, ref visible_))
        {
            gameObject.SetActive(visible_);
        }
	}

    private void Save()
    {
        visible_ = false;
        GameState.Save(name_, visible_);
        Debug.Log(name_ + ": " + visible_);
    }
}
