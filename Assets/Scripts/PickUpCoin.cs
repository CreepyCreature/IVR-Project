using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //int ID = gameObject.GetInstanceID();
        Vector3 position = transform.position;
        bool active = gameObject.activeInHierarchy;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerResources.CollectCoin();
            AudioManager.Instance.PlaySound("CollectCoin");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
