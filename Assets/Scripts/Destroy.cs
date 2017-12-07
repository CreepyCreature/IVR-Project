using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Destroy : MonoBehaviour
{
    [Range(0, 1)] public float look_treshold = 0.95f;
    [Range(0, 100)] public float distance_treshold = 5f;

    public event Action OnDeactivation;

    private bool active_ = true;
    private bool looked_at_ = false;
    private bool close_enough_ = false;

	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1) && looked_at_ && close_enough_)
        {
            active_ = !active_;

            if (OnDeactivation != null) OnDeactivation();

            //GetComponent<Renderer>().enabled = active_;
            gameObject.SetActive(active_);
            //Destroy(gameObject);
        }

        float dot = Vector3.Dot(
            Vector3.Normalize(Camera.main.transform.forward),
            Vector3.Normalize(transform.position - Camera.main.transform.position)
            );
        if (dot * dot > look_treshold)
        {
            looked_at_ = true;
        } else { looked_at_ = false; }

        if (Vector3.Magnitude(transform.position - Camera.main.transform.position) < distance_treshold)
        {
            close_enough_ = true;
        } else { close_enough_ = false; }
    }
}
