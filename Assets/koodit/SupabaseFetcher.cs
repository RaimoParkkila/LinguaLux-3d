using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;

public class SupabaseFetcher : MonoBehaviour
{
    public SupabaseButtonSpawner buttonSpawner;

    private string perusUrl = "https://vzttssgpvieufruebbxh.supabase.co/rest/v1";
    private string apiAvain = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ6dHRzc2dwdmlldWZydWViYnhoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDExMTM4NTIsImV4cCI6MjA1NjY4OTg1Mn0.4gK35q7MWVju2NODxxAHb0O966H6YpJWTTMgcVl8TqU";
    private string tauluNimi = "languagetables";

    void Start()
    {
        StartCoroutine(HaeDataJsonina());
        UnityEngine.Debug.Log("✅ SupabaseManager Start() käynnistyi");
    }

    IEnumerator HaeDataJsonina()
    {
        string url = $"{perusUrl}/{tauluNimi}?select=*";
 

        using (UnityWebRequest pyynto = UnityWebRequest.Get(url))
        {
            pyynto.SetRequestHeader("apikey", apiAvain);
            pyynto.SetRequestHeader("Accept", "application/json");

            yield return pyynto.SendWebRequest();

            if (pyynto.result == UnityWebRequest.Result.Success)
            {
                string json = pyynto.downloadHandler.text;

                // Deserialize JSON -> lista kielidataa
                List<Kielidata> tulokset = JsonConvert.DeserializeObject<List<Kielidata>>(json);

                // Poimitaan title-kentät listaan
                List<string> vaihtoehdot = new List<string>();
                foreach (var rivi in tulokset)
                {
                    vaihtoehdot.Add(rivi.title);  // nyt käytetään JSONin "title"-kenttää
                }

                // Luodaan napit
                if (buttonSpawner != null)
                {
                    buttonSpawner.GenerateButtons(vaihtoehdot.ToArray());
                }
                else
                {
                    Debug.LogWarning("ButtonSpawner ei ole asetettu!");
                }
            }
            else
            {
                Debug.LogError($"Virhe: {pyynto.responseCode} - {pyynto.error}");
            }
        }
    }

    [System.Serializable]
    public class Kielidata
    {
        public string title;
        public string tablename;
        public string tabledescription;
    }
}
