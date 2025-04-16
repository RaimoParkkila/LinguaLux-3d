using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

//using static System.Net.Mime.MediaTypeNames;
//using System.Diagnostics;

public class LanguageTableFetcher : SupabaseManager

{
    [SerializeField] private string selectedTable = "chinese";

    [DllImport("sapi.dll")]
    private static extern int SpVoice(); // (ei ole käytössä tässä versiossa, mutta säilytetty jos haluat lisätä TTS:n)

    private string baseUrl = "https://vzttssgpvieufruebbxh.supabase.co/rest/v1";
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ6dHRzc2dwdmlldWZydWViYnhoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDExMTM4NTIsImV4cCI6MjA1NjY4OTg1Mn0.4gK35q7MWVju2NODxxAHb0O966H6YpJWTTMgcVl8TqU";

    public TMP_Text questionText;
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;

    private List<QuestionData> questions = new List<QuestionData>();
    private int currentQuestionIndex = 0;
    private string correctAnswer;
    private bool isLoading = true;

    private void Start()
    {
        Debug.Log("🔹 SupabaseManager2 käynnistyi, haetaan kysymyksiä taulusta: " + selectedTable);
        StartCoroutine(GetData());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public void SetTable(string tableName)
    {
        selectedTable = tableName;
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        isLoading = true;
        string url = $"{baseUrl}/{selectedTable}?select=*";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("apikey", apiKey);
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                questions = JsonConvert.DeserializeObject<List<QuestionData>>(request.downloadHandler.text);
                if (questions != null && questions.Count > 0)
                {
                    currentQuestionIndex = 0;
                    isLoading = false;
                    LoadQuestion();
                }
                else
                {
                    questionText.text = "¡No hay preguntas disponibles!";
                    isLoading = false;
                }
            }
            else
            {
                Debug.LogError($"❌ Virhe: {request.responseCode} - {request.error}");
                questionText.text = "¡Error al cargar datos!";
                isLoading = false;
            }
        }
    }

    void LoadQuestion()
    {
        if (isLoading || questions == null || questions.Count == 0)
        {
            questionText.text = "¡No se han cargado preguntas!";
            return;
        }

        QuestionData questionData = questions[currentQuestionIndex];

        questionText.text = questionData.question;
        correctAnswer = questionData.answer;

        List<string> options = new List<string> { questionData.a, questionData.b, questionData.c, questionData.d };
        options.RemoveAll(string.IsNullOrEmpty);

        if (options.Count != 4)
        {
            questionText.text = "Error: faltan opciones.";
            return;
        }

        Button[] buttons = { buttonA, buttonB, buttonC, buttonD };

        for (int i = 0; i < buttons.Length; i++)
        {
            TMP_Text textComponent = buttons[i].GetComponentInChildren<TMP_Text>();
            if (textComponent == null)
            {
                Debug.LogError($"❌ {buttons[i].name}-painikkeesta puuttuu TMP_Text!");
                continue;
            }

            textComponent.text = options[i];
            string answer = options[i];
            Button button = buttons[i];
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => CheckAnswer(answer, button));
            button.GetComponent<Image>().color = Color.white;
        }
    }

    void CheckAnswer(string selectedAnswer, Button selectedButton)
    {
        if (selectedAnswer == correctAnswer)
        {
            selectedButton.GetComponent<Image>().color = Color.green;
            Debug.Log("✅ Oikea vastaus!");
        }
        else
        {
            selectedButton.GetComponent<Image>().color = Color.red;
            Debug.Log("❌ Väärä vastaus!");
        }

        // Voit lisätä viiveellä seuraavan kysymyksen latauksen jne.
    }
}
