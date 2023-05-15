using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameSettings gameSettings;


    private void Awake()
    {
        LoadFromJson();
    }

    private void OnDisable()
    {
        SaveToJson();
    }

    private void SaveToJson()
    {
        string detailsJson = JsonConvert.SerializeObject(gameSettings);
        string path = Path.Combine(Application.streamingAssetsPath, "Settings.json");
        File.WriteAllText(path, detailsJson);
    }

    private void LoadFromJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Settings.json");
        string detailsJson = File.ReadAllText(path);
        gameSettings = JsonConvert.DeserializeObject<GameSettings>(detailsJson);
    }
}

[System.Serializable]
public class GameSettings
{
   
    public float AFK_Time;
}
