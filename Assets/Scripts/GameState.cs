using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class GameState
{
    [Serializable]	
    public class KeyValueList<V>
    {
        public List<string> keys = new List<string>();
        public List<V> values = new List<V>();

        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }

        public void TrySetValue(string key, V value)
        {
            int index = keys.FindIndex(k => k == key);
            if (index > -1)
            {
                values[index] = value;
            }
            else
            {
                keys.Add(key);
                values.Add(value);
            }
        }

        public bool TryGetValue(string key, ref V value)
        {
            int index = keys.FindIndex(k => k == key);
            if (index > -1)
            {
                value = values[index];
                return true;
            }
            return false;
        }
    }

    private static KeyValueList<bool> bool_keyval_list = new KeyValueList<bool>();

    public static void Reset()
    {
        bool_keyval_list.Clear();
    }

    public static void Save(string key, bool value)
    {
        Save(bool_keyval_list, key, value);
    }

    public static bool Load(string key, ref bool value)
    {
        return Load(bool_keyval_list, key, ref value);
    }

    private static void Save<V>(KeyValueList<V> list, string key, V value)
    {
        list.TrySetValue(key, value);
    }

    private static bool Load<V>(KeyValueList<V> list, string key, ref V value)
    {
        return list.TryGetValue(key, ref value);
    }

}
