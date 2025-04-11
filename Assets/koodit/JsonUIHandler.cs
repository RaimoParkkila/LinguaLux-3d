using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class JsonUIHandler : MonoBehaviour
{
    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string[] answers;
    }

    public Text questionText;
    public Button[] answerButtons;

    private void Start()
    {
        string json = "{\"question\": \"Mikä on pääkaupunki?\", \"answers\": [\"Helsinki\", \"Tukholma\", \"Oslo\", \"Kööpenhamina\"]}";

        QuestionData data = JsonUtility.FromJson<QuestionData>(json);
        DisplayQuestion(data);
    }

    void DisplayQuestion(QuestionData data)
    {
        questionText.text = data.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < data.answers.Length)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = data.answers[i];
                answerButtons[i].gameObject.SetActive(true);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
