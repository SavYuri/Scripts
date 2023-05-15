using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FindMatch
{

    public class FindMatch_Data : MonoBehaviour
    {
        public Topic[] topic;

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
            string path = Path.Combine(Application.streamingAssetsPath, "FindMatch_Data.json");
            File.WriteAllText(path, detailsJson);
        }

        private void LoadFromJson()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "FindMatch_Data.json");
            string detailsJson = File.ReadAllText(path);
            topic = JsonConvert.DeserializeObject<Topic[]>(detailsJson);
        }
    }
    //����:
    [System.Serializable]
    public class Topic
    {
        public string nameOfTopic;
        public Question[] question;
    }

    //������:
    [System.Serializable]
    public class Question
    {
        //public string question;
        public string rightAnswer;
        public string descriptionOfAnswer;
        public string nameOfImage;
    }
}
