using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.IO;

namespace CollectTheWord
{

    public class CollectTheWord_Data : MonoBehaviour
    {
        public Topic [] topic;

        private void Awake()
        {
           // LoadFromJson();
        }

        private void OnDisable()
        {
            SaveToJson();
        }

        private void SaveToJson()
        {
            string detailsJson = JsonConvert.SerializeObject(topic, Formatting.Indented);
            string path = Path.Combine(Application.streamingAssetsPath, "CollectTheWord_Data.json");
            File.WriteAllText(path, detailsJson);
        }

        private void LoadFromJson()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "CollectTheWord_Data.json");
            string detailsJson = File.ReadAllText(path);
            topic = JsonConvert.DeserializeObject<Topic[]>(detailsJson);
        }


    }

    //����:
    [System.Serializable]
    public class Topic
    {
        public string nameOfTopic;
        public Question [] question; 
    }

    //������:
    [System.Serializable]
    public class Question
    {
        public string question;
        public string rightAnswer;
        public string descriptionOfAnswer;
        public string nameOfImage;
    }
}
