using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadComponnent : MonoBehaviour
{
    [SerializeField] private string _fileName;

    private string _totalPath;
    private string TotalPath
    {
        get
        {
            if (string.IsNullOrEmpty(_totalPath)) { _totalPath = Application.persistentDataPath + "/" + (string.IsNullOrEmpty(_fileName) ? "DefaultSaveFile" : _fileName) + ".json"; };
            return _totalPath;
        }
    }

    public void Save<T>(T savingData) where T : class
    {
        if (!File.Exists(TotalPath))
        {
            File.Create(TotalPath);
        }
        
        string saveString = JsonUtility.ToJson(savingData);
        
        File.WriteAllText(TotalPath, saveString);
    }

    public void Load<T>(System.Action<T> dataLoadAction) where T : class
    {
        if (!File.Exists(TotalPath))
        {
            return;
        }

        string jsonStream = File.ReadAllText(TotalPath);

        T loadedData = JsonUtility.FromJson<T>(jsonStream);

        if (loadedData != null && dataLoadAction != null)
        {
            dataLoadAction.Invoke(loadedData);
        }
        else
        { Debug.LogError("Can't load data"); }
    }
}
