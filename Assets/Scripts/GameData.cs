using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;
using System.IO;

using System.Linq;

//[System.Serializable]
public struct CheckpointStruct
{
    public int SceneIndex { get; set; }
    public Vector3 Position { get; set; }
}

[XmlRoot("GameData")]
public class GameData
{
    private static /*readonly*/ GameData instance = new GameData();
    static GameData () { }
    private GameData () { }
    public static GameData Instance { get { return instance; } private set { instance = value; } }
        
    private static string XMLFileName = "/GameData.xml";
    public static string gameDataFile = Application.persistentDataPath + XMLFileName;

    [XmlArray("Checkpoints")]
    [XmlArrayItem(ElementName = "Checkpoint")]
    public List<CheckpointStruct> checkpointsProxy = new List<CheckpointStruct>();

    [XmlIgnore]
    private static Dictionary<int, Vector3> checkpoints = new Dictionary<int, Vector3>();
    
    //[XmlArray("Checkpoints")]
    //[XmlArrayItem(ElementName = "Checkpoint")]
    //private static List<CheckpointProxy> CheckpointProxy { get; set; }
    //[XmlIgnore]
    //private static Dictionary<int, Vector3> CheckpointDictionary
    //{
    //    get { return CheckpointProxy.ToDictionary(x => x.Key, x => x.Value); }
    //    set { CheckpointProxy = value.Select(x => new global::CheckpointProxy() { Key = x.Key, Value = x.Value }).ToList(); }
    //}

    public static void Save ()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream fileStream = new FileStream(gameDataFile, FileMode.Create);
        serializer.Serialize(fileStream, Instance);
        fileStream.Close();

        // Debug.Log("GameData::Game Data saved in " + gameDataFile);
    }

    public static void Load ()
    {
        if (!File.Exists(gameDataFile))
        {
            Debug.LogWarning("GameData::Save file " + gameDataFile + " not found!");
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream fileStream = new FileStream(gameDataFile, FileMode.Open);

        try
        {
            Instance = serializer.Deserialize(fileStream) as GameData;
            DeserializeCheckpoints();
        }
        finally
        {
            fileStream.Close();
        }
    }

    public static void Clear ()
    {
        checkpoints.Clear();
        Instance.checkpointsProxy.Clear();
        Save();
    }

    public static void SetCheckpoint (int sceneIndex, Vector3 position)
    {
        checkpoints[sceneIndex] = position;

        if (Instance.checkpointsProxy.Exists(x => x.SceneIndex == sceneIndex))
        {
            Instance.checkpointsProxy.RemoveAll(x => x.SceneIndex == sceneIndex);
        }

        CheckpointStruct newCheckpoint = new CheckpointStruct();
        newCheckpoint.SceneIndex = sceneIndex;
        newCheckpoint.Position = position;
        Instance.checkpointsProxy.Add(newCheckpoint);
    }

    public static Dictionary<int, Vector3> GetCheckpoints ()
    {
        return checkpoints;
    }

    private static void DeserializeCheckpoints ()
    {
        var checkpointList = Instance.checkpointsProxy;
        foreach (var c in checkpointList)
        {
            checkpoints[c.SceneIndex] = c.Position;
        }
    }

    private static void SerializeCheckpointDictionary()
    {
        //foreach (var key in checkpoints.Keys)
        //{
        //    Debug.Log("adding");
        //    Instance.checkpointProxy.Add(new CheckpointProxy(key, checkpoints[key]));
        //}

        //foreach (var e in Instance.checkpointProxy)
        //{
        //    Debug.Log(e.Key + " => " + e.Value);
        //}
    }
}
