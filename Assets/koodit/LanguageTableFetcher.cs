using System.Collections;               // ✅ For IEnumerator (non-generic)
using System.Collections.Generic;       // For List<T>
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Diagnostics;



public class LanguageTableFetcher : MonoBehaviour
{
    private string baseUrl = "https://vzttssgpvieufruebbxh.supabase.co/rest/v1";
    private string tableName = "languagetables"; // Table name
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ6dHRzc2dwdmlldWZydWViYnhoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDExMTM4NTIsImV4cCI6MjA1NjY4OTg1Mn0.4gK35q7MWVju2NODxxAHb0O966H6YpJWTTMgcVl8TqU";


    // Start is called before the first frame update
    void Start()
    {
        // Start fetching data immediately when the game starts
        StartCoroutine(GetLanguageTableData());
    }

    // Fetches data from Supabase
    IEnumerator GetLanguageTableData()
    {
        string url = $"{baseUrl}/{tableName}?select=*"; // URL for fetching data

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("apikey", apiKey);
            request.SetRequestHeader("Accept", "application/json");

            // Wait until the request completes
            yield return request.SendWebRequest();

            // Check for network errors or HTTP errors
            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response
                var data = JsonConvert.DeserializeObject<List<LanguageTableData>>(request.downloadHandler.text);

                UnityEngine.Debug.Log(request.downloadHandler.text);



                if (data != null && data.Count > 0)
                {
                    UnityEngine.Debug.Log($"✅ Fetched {data.Count} rows from 'languagetables'");



                    // Print all rows
                    foreach (var row in data)
                    {
                        UnityEngine.Debug.Log($"Question: {row.question}, A: {row.a}, B: {row.b}, C: {row.c}, D: {row.d}, Correct Answer: {row.answer}");

                    }
                }
                else
                {
                    UnityEngine.Debug.LogWarning("❌ No data found in the 'languagetables' table.");
                }
            }
            else
            {
                // Handle errors during the request
                UnityEngine.Debug.LogError($"❌ Error: {request.responseCode} - {request.error}");
            }
        }
    }
}

// Data structure for "languagetables" table
[System.Serializable]
public class LanguageTableData
{
    public string question;
    public string a;
    public string b;
    public string c;
    public string d;
    public string answer;
}
