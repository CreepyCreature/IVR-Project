using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour {

    public delegate void CoinDestroyed(string gameObjectName);
    public event CoinDestroyed OnCoinDestroyed;

	// Use this for initialization
	void Start ()
    {
        //int ID = gameObject.GetInstanceID();
        string name = gameObject.name;
        Vector3 position = transform.position;
        bool active = gameObject.activeInHierarchy;        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerResources.CollectCoin();
            AudioManager.Instance.PlaySound("CollectCoin");
            //gameObject.SetActive(false);

            if (OnCoinDestroyed != null)
            {
                Debug.LogWarning("PickUpCoin::Calling OnCoinDestroyed!");
                OnCoinDestroyed(gameObject.name);
            } else Debug.LogWarning("PickUpCoin::OnCoinDestroyed is NULL!");

            Destroy(gameObject);
        }
    }
}
