using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Newtonsoft.Json;
using System.Diagnostics;
using UnityEngine.Networking;

public class SupabaseButtonSpawner : MonoBehaviour
{
    public static SupabaseButtonSpawner Instance;

    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public TMP_Text statusText; // ← UI-teksti, johon tulostetaan painallus

    private string baseUrl = "https://vzttssgpvieufruebbxh.supabase.co/rest/v1";
    public string tableName = "languagetables";
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ6dHRzc2dwdmlldWZydWViYnhoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDExMTM4NTIsImV4cCI6MjA1NjY4OTg1Mn0.4gK35q7MWVju2NODxxAHb0O966H6YpJWTTMgcVl8TqU";

 

    public List<OptionData> optionsData;
    public void GetDataFromTable()
    {
        // Esimerkiksi suoritetaan HTTP-pyyntö tai käytetään Supabase SDK:ta
        // Tämä on vain esimerkki, joten täydennä koodi tarpeidesi mukaan

        string url = $"{baseUrl}/{tableName}";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", "Bearer " + apiKey);
            www.SendWebRequest();

            // Odota, että pyyntö on valmis ja käsittele tulos
            if (www.result == UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.Log("Data received: " + www.downloadHandler.text);
            }
            else
            {
                UnityEngine.Debug.Log("Error: " + www.error);
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public delegate void ButtonClicked(string selectedOption);
    public event ButtonClicked OnButtonClicked;

    // Muokattu GenerateButtons-funktio ottamaan taulun nimi SupabaseManagerilta
    public void GenerateButtons(string[] vaihtoehdot)
    {
        foreach (Transform lapsi in buttonContainer)
        {
            Destroy(lapsi.gameObject); // Tyhjennetään vanhat napit
        }

        foreach (var vaihtoehto in vaihtoehdot)
        {
            GameObject uusiNappi = Instantiate(buttonPrefab, buttonContainer);
            TMP_Text teksti = uusiNappi.GetComponentInChildren<TMP_Text>();
            if (teksti != null)
            {
                teksti.text = vaihtoehto;
                teksti.fontSize = 28;
                teksti.color = Color.black;
                teksti.alignment = TextAlignmentOptions.Midline;
            }

            Button btn = uusiNappi.GetComponent<Button>();
            if (btn != null)
            {
                ColorBlock cb = btn.colors;
                cb.normalColor = new Color(0.9f, 0.9f, 0.95f);
                cb.highlightedColor = new Color(1f, 0.8f, 0.4f);
                cb.pressedColor = new Color(0.8f, 0.6f, 0.3f);
                cb.selectedColor = cb.highlightedColor;
                cb.disabledColor = new Color(0.5f, 0.5f, 0.5f);
                btn.colors = cb;

                btn.onClick.AddListener(() =>
                {
                    UnityEngine.Debug.Log("Klikattiin: " + vaihtoehto);

                    if (statusText != null)
                    {
                        statusText.text = "Klikattiin: " + vaihtoehto;
                    }

                    // Käynnistetään uuden datan haku valitun vaihtoehdon perusteella
                    SupabaseManager.Instance.StartCoroutine(
                        SupabaseManager.Instance.GetDataFromTable(vaihtoehto));

                    OnButtonClicked?.Invoke(vaihtoehto); // nyt käytetään valittua vaihtoehtoa suoraan
                });

            }

            LayoutElement le = uusiNappi.GetComponent<LayoutElement>();
            if (le == null)
                le = uusiNappi.AddComponent<LayoutElement>();
            le.preferredHeight = 60;
            le.minWidth = 300;
            le.flexibleWidth = 1;
        }
    }

}
