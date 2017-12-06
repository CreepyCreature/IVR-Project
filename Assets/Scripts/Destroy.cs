using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(gameObject);
        }
	}
}
