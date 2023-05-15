using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Recipes
{

    public class Recipes_Data : MonoBehaviour
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
            string path = Path.Combine(Application.streamingAssetsPath, "Receipt_Data.json");
            File.WriteAllText(path, detailsJson);
        }

        private void LoadFromJson()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Receipt_Data.json");
            string detailsJson = File.ReadAllText(path);
            topic = JsonConvert.DeserializeObject<Topic[]>(detailsJson);
        }
    }

    //тема:
    [System.Serializable]
    public class Topic
    {
        public string nameOfTopic;
        public Question[] question;
    }

    //вопрос:
    [System.Serializable]
    public class Question
    {
        public string question;
        public string [] nameOfQuestionImage;
        public string descriptionOfAnswer;
        public string nameOfAnswerImage;
    }

    
}
