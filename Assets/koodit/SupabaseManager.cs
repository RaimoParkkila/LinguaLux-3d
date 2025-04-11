using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]


public class SupabaseManager : MonoBehaviour


{


    void Update()
    {
        // Tarkistetaan, onko ESC-näppäintä painettu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Suljetaan peli
            Application.Quit();

            // Jos peli on Editorissa, simuloidaan pelin sulkeminen
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
    [DllImport("sapi.dll")]
    private static extern int SpVoice();
    private string baseUrl = "https://vzttssgpvieufruebbxh.supabase.co/rest/v1";
    private string tableName = "chinese";
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

    private string GetDebuggerDisplay()
    {
        return ToString();
    }   



    private void Start()
    {
        // 🔹 Testataan TTS heti Unityn käynnistyessä
        
       
            Debug.Log("Liitetty objektiin: " + gameObject.name);
     

        StartCoroutine(GetData());
        StartCoroutine(GetData());
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
               // Debug.Log($"Saatiin vastaus Supabaselta: {request.downloadHandler.text}");
                questions = JsonConvert.DeserializeObject<List<QuestionData>>(request.downloadHandler.text);

                if (questions != null && questions.Count > 0)
                {
                    //Debug.Log($"✅ Haettiin {questions.Count} kysymystä.");
                    currentQuestionIndex = 0;
                    isLoading = false; // Lataus valmis ennen LoadQuestion-kutsua!
                    LoadQuestion();
                }
                else
                {
                    // Debug.LogError("❌ No hay preguntas disponibles en Supabase.");
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
        if (isLoading)
        {
            //Debug.LogWarning("⏳ Lataus kesken, odotetaan...");
            return;
        }

        if (questions == null || questions.Count == 0)
        {
            //Debug.LogError("❌ Virhe: Ei kysymyksiä ladattu!");
           //questionText.text = "Ei kysymyksiä ladattu!";
            questionText.text = "¡No se han cargado preguntas!";
            return;
        }

        //Debug.Log($"🔄 Ladataan kysymys {currentQuestionIndex}/{questions.Count}");

        QuestionData questionData = questions[currentQuestionIndex];

        questionText.text = questionData.question;
        correctAnswer = questionData.answer;

        List<string> options = new List<string> { questionData.a, questionData.b, questionData.c, questionData.d };
        options.RemoveAll(string.IsNullOrEmpty);

        if (options.Count != 4)
        {
           // Debug.LogError($"❌ Virhe: Vastausvaihtoehtoja ei ole 4 (löytyi {options.Count})");
            return;
        }

        Button[] buttons = { buttonA, buttonB, buttonC, buttonD };

        for (int i = 0; i < buttons.Length; i++)
        {
            TMP_Text textComponent = buttons[i].GetComponentInChildren<TMP_Text>();
            if (textComponent == null)
            {
               // Debug.LogError($"❌ Virhe: {buttons[i].name}-painikkeesta puuttuu TMP_Text!");
                return;
            }

            textComponent.text = options[i];
            string answer = options[i];
            Button button = buttons[i];
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => CheckAnswer(answer, button));
            button.GetComponent<Image>().color = Color.white;
        }

        //Debug.Log($"✅ LoadQuestion suoritettu onnistuneesti: {questionText.text}");
    }


    void CheckAnswer(string selectedAnswer, Button selectedButton)
    {
        if (selectedAnswer == correctAnswer)
        {
            selectedButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            selectedButton.GetComponent<Image>().color = Color.red;
        }

        //questionText.text = $"Oikein oli: {correctAnswer}";
        questionText.text = $"La respuesta correcta es: {correctAnswer}";
        StartCoroutine(NextQuestionAfterDelay(5f));
    }

    IEnumerator NextQuestionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("Ei kysymyksiä ladattuna!");
            yield break;
        }

        currentQuestionIndex = (currentQuestionIndex + 1) % questions.Count;
        LoadQuestion();
    }
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
