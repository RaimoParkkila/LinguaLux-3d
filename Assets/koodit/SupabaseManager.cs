using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;

public class SupabaseManager : MonoBehaviour
{
    // Singleton instance
    public static SupabaseManager Instance;

    void Awake()
    {
        // Singleton initialization
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the object between scenes
        }
        else
        {
           //  Destroy(gameObject); // If an instance already exists, destroy this object
        }
    }

    private string baseUrl = "https://vzttssgpvieufruebbxh.supabase.co/rest/v1";
    public string tableName = "chinese";
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

    void Start()
    {
        UnityEngine.Debug.Log("Liitetty objektiin: " + gameObject.name);
        //StartCoroutine(GetData());
        StartCoroutine(GetDataFromTable("chinese"));
    }


    public IEnumerator GetDataFromTable(string table)

    {


        UnityEngine.Debug.Log("✅ GetDataFromTable kutsuttiin: ");
        isLoading = true;
        string url = $"{baseUrl}/{table}?select=*";
        UnityEngine.Debug.Log("✅ koitetaan hakea urlia " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("apikey", apiKey);
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                questions = JsonConvert.DeserializeObject<List<QuestionData>>(request.downloadHandler.text);
                UnityEngine.Debug.Log("✅ Data haettu onnistuneesti: " + request.downloadHandler.text);

                if (questions != null && questions.Count > 0)
                {
                    currentQuestionIndex = 0;
                    isLoading = false;
                    LoadQuestion();
                }
                else
                {
                    questionText.text = "¡No hay preguntas disponibles!";
                    UnityEngine.Debug.LogError($"❌ Virhe: {request.responseCode} - {request.error}");
                    isLoading = false;
                }
            }
            else
            {
                UnityEngine.Debug.LogError($"❌ Virhe: {request.responseCode} - {request.error}");
                questionText.text = "¡Error al cargar datos!";
                isLoading = false;
            }
        }
    }


    IEnumerator GetData()
    {
        isLoading = true;
        string url = $"{baseUrl}/{tableName}?select=*";

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
                UnityEngine.Debug.LogError($"❌ Virhe: {request.responseCode} - {request.error}");
                questionText.text = "¡Error al cargar datos!";
                isLoading = false;
            }
        }
    }

    void LoadQuestion()
    {
        if (isLoading) return;

        if (questions == null || questions.Count == 0)
        {
            questionText.text = "¡No se han cargado preguntas!";
            return;
        }

        QuestionData questionData = questions[currentQuestionIndex];

        questionText.text = questionData.question;
        correctAnswer = questionData.answer;

        List<string> options = new List<string> { questionData.a, questionData.b, questionData.c, questionData.d };
        options.RemoveAll(string.IsNullOrEmpty);

        if (options.Count != 4) return;

        Button[] buttons = { buttonA, buttonB, buttonC, buttonD };

        for (int i = 0; i < buttons.Length; i++)
        {
            TMP_Text textComponent = buttons[i].GetComponentInChildren<TMP_Text>();
            if (textComponent == null) return;

            textComponent.text = options[i];
            string answer = options[i];
            Button button = buttons[i];
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => CheckAnswer(answer, button));
            button.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    void CheckAnswer(string selectedAnswer, Button selectedButton)
    {
        if (selectedAnswer == correctAnswer)
        {
            selectedButton.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        else
        {
            selectedButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

        questionText.text = $"La respuesta correcta es: {correctAnswer}";
        StartCoroutine(NextQuestionAfterDelay(5f));
    }

    IEnumerator NextQuestionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (questions == null || questions.Count == 0)
        {
            UnityEngine.Debug.LogError("Ei kysymyksiä ladattuna!");
            yield break;
        }

        currentQuestionIndex = (currentQuestionIndex + 1) % questions.Count;
        LoadQuestion();
    }

    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string a;
        public string b;
        public string c;
        public string d;
        public string answer;
    }
}
