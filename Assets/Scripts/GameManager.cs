using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private Vector3 savedPosition;
    private Dictionary<int, Vector3> checkpointDictionary = new Dictionary<int, Vector3>();
    private List<string> destroyedCoins = new List<string>();
    
    void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {  Destroy(gameObject); return;  }

        SceneManager.sceneLoaded += OnSceneLoaded;
        Initialize();
    }

    private void Initialize ()
    {
        GameData.Load();
        checkpointDictionary = GameData.GetCheckpoints();

        int sceneIndex = ActiveSceneIndex();
        PlayerResources.Coins = GameData.GetCoinCount(sceneIndex);
        destroyedCoins = GameData.GetDestroyedCoins(sceneIndex);
    }

	void Start ()
    {
        DestroyCollectedCoins();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PlayerResources.Reset();
            LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            PlayerResources.Reset();
            LoadScene(2);
        }        

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameData.Load();
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GameData.Clear();
        }
	}

    public void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene " + scene.name + " loaded!");
        DestroyCollectedCoins();
    }

    public void LoadScene (int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);

        destroyedCoins = GameData.GetDestroyedCoins(sceneIndex);
        PlayerResources.Coins = GameData.GetCoinCount(sceneIndex);
    }


    private void DestroyCollectedCoins ()
    {
        foreach (var coin in GameObject.FindGameObjectsWithTag("PickUpCoin"))
        {
            // Debug.Log("Analyzing " + coin.name);

            if (destroyedCoins.Exists(x => x == coin.name))
            {                
                Debug.LogWarning("Destroying " + coin.name);
                Destroy(coin.gameObject);
            }
            else
            {
                Debug.Log("Adding callback to " + coin.name);
                coin.GetComponent<PickUpCoin>().OnCoinDestroyed += OnCoinDestroyed;
            }
        }
    }

    // Coin info is saved when a Checkpoint is activated, see SaveCheckpoint(..)
    public void SaveCoinData ()
    {
        int sceneIndex = ActiveSceneIndex();
        GameData.UpdateCoinCount(sceneIndex, PlayerResources.Coins);

        foreach (var coinName in destroyedCoins)
        {
            GameData.UpdateDestroyedCoins(sceneIndex, coinName);
        }
    }

    public void OnCoinDestroyed (string gameObjectName)
    {
        destroyedCoins.Add(gameObjectName);
        Debug.Log("GameManager::Adding coin \"" + gameObjectName + "\" to destroyed list!");
    }

    public void SaveCheckpoint (Vector3 newPosition)
    {
        int sceneIndex = ActiveSceneIndex();
        checkpointDictionary[sceneIndex] = newPosition;
        savedPosition = newPosition;

        // Coin info is saved when a Checkpoint is activated
        SaveCoinData();

        GameData.SetCheckpoint(sceneIndex, newPosition);
        GameData.Save();

        // Debug.Log("GameManager::Saving Player position " + checkpointDictionary[sceneIndex] + " at Scene Index "+ sceneIndex);
    }

    public Vector3 GetPlayerPosition (int sceneIndex = -1)
    {
        if (sceneIndex < 0)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        if (HasPlayerPosition(sceneIndex))
        {
            return checkpointDictionary[sceneIndex];
        }

        return new Vector3(0, 0, 0);
    }

    public bool HasPlayerPosition (int sceneIndex)
    {
        if (checkpointDictionary.ContainsKey(sceneIndex))
            return true;
        else
            return false;
    }

    public void ResetGameState ()
    {
        checkpointDictionary.Clear();
        destroyedCoins.Clear();
        GameData.Clear();
    }

    // Gets the active scene's build index; saving the world
    // one keystroke at a time.
    private int ActiveSceneIndex ()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
