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
public class SupabaseFinnish : MonoBehaviour


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

    private string perusUrl = "https://vzttssgpvieufruebbxh.supabase.co/rest/v1";
    private string tauluNimi = "finnish"; // Vaihdetaan taulun nimi "chinese" -> "finnish"
    private string apiAvain = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ6dHRzc2dwdmlldWZydWViYnhoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDExMTM4NTIsImV4cCI6MjA1NjY4OTg1Mn0.4gK35q7MWVju2NODxxAHb0O966H6YpJWTTMgcVl8TqU";

    public TMP_Text kysymysTeksti;
    public Button nappiA;
    public Button nappiB;
    public Button nappiC;
    public Button nappiD;

    private List<KysymysData> kysymykset = new List<KysymysData>();
    private int nykyinenKysymysIndeksi = 0;
    private string oikeaVastaus;
    private bool lataaminenKaynnissa = true;

    private string GetDebuggerDisplay()
    {
        return ToString();
    }

 

    private void Start()
    {

      
        // Testataan TTS heti Unityn käynnistyessä
        Debug.Log("Liitetty objektiin: " + gameObject.name);
        StartCoroutine(HaeData());
    }
    IEnumerator ViivastaTekstinPäivitys()
    {
    
        Debug.Log("Asetettu KYSYMYS, odotetaan ennen Supabasen päivitystä...");
        yield return new WaitForSeconds(2);  // Odotetaan ennen kuin Supabase saa päivitettyä sen
   
    }
    IEnumerator HaeData()
    {
        lataaminenKaynnissa = true;
        string url = $"{perusUrl}/{tauluNimi}?select=*";


        using (UnityWebRequest pyynto = UnityWebRequest.Get(url))
        {
            pyynto.SetRequestHeader("apikey", apiAvain);
            pyynto.SetRequestHeader("Accept", "application/json");

            yield return pyynto.SendWebRequest();

            if (pyynto.result == UnityWebRequest.Result.Success)
            {
                kysymykset = JsonConvert.DeserializeObject<List<KysymysData>>(pyynto.downloadHandler.text);

                if (kysymykset != null && kysymykset.Count > 0)
                {
                    nykyinenKysymysIndeksi = 0;
                    lataaminenKaynnissa = false; // Lataus valmis ennen LoadQuestion-kutsua!
                    LataaKysymys();
                }
                else
                {
                    kysymysTeksti.text = "Ei kysymyksiä ladattu!";
                    lataaminenKaynnissa = false;
                }
            }
            else
            {
                Debug.LogError($"Virhe: {pyynto.responseCode} - {pyynto.error}");
                kysymysTeksti.text = "Virhe datan latauksessa!";
                lataaminenKaynnissa = false;
            }
        }
    }

    void LataaKysymys()
    {
        if (lataaminenKaynnissa)
        {
            return;
        }
        //kysymysTeksti.text = "KYSYMYS";
        if (kysymykset == null || kysymykset.Count == 0)
        {
            kysymysTeksti.text = "Ei kysymyksiä ladattu!";
            return;
        }

        KysymysData nykyinenKysymysData = kysymykset[nykyinenKysymysIndeksi];
     

        kysymysTeksti.text = nykyinenKysymysData.question;
        Debug.Log($"Nykyinen kysymys JSON: {JsonConvert.SerializeObject(nykyinenKysymysData)}");
 
        oikeaVastaus = nykyinenKysymysData.answer;

        List<string> vaihtoehdot = new List<string> { nykyinenKysymysData.a, nykyinenKysymysData.b, nykyinenKysymysData.c, nykyinenKysymysData.d };
        vaihtoehdot.RemoveAll(string.IsNullOrEmpty);

        if (vaihtoehdot.Count != 4)
        {
            return;
        }

        Button[] napit = { nappiA, nappiB, nappiC, nappiD };

        for (int i = 0; i < napit.Length; i++)
        {
            TMP_Text tekstikomponentti = napit[i].GetComponentInChildren<TMP_Text>();
            if (tekstikomponentti == null)
            {
                return;
            }

            tekstikomponentti.text = vaihtoehdot[i];
            string vastaus = vaihtoehdot[i];
            Button nappi = napit[i];
            nappi.onClick.RemoveAllListeners();
            nappi.onClick.AddListener(() => TarkistaVastaus(vastaus, nappi));
            nappi.GetComponent<Image>().color = Color.white;
        }
    }

    void TarkistaVastaus(string valittuVastaus, Button valittuNappi)
    {
        if (valittuVastaus == oikeaVastaus)
        {
            valittuNappi.GetComponent<Image>().color = Color.green;
        }
        else
        {
            valittuNappi.GetComponent<Image>().color = Color.red;
        }

        kysymysTeksti.text = $"Oikea vastaus oli: {oikeaVastaus}";
        StartCoroutine(SiirrySeuraavaanKysymykseenViiveella(5f));
    }

    IEnumerator SiirrySeuraavaanKysymykseenViiveella(float viive)
    {
        yield return new WaitForSeconds(viive);

        if (kysymykset == null || kysymykset.Count == 0)
        {
            Debug.LogError("Ei kysymyksiä ladattuna!");
            yield break;
        }

        nykyinenKysymysIndeksi = (nykyinenKysymysIndeksi + 1) % kysymykset.Count;
        LataaKysymys();
    }
}

[System.Serializable]
public class KysymysData
{
    public string question; // Muutettu "kysymys" -> "question"
    public string a;
    public string b;
    public string c;
    public string d;
    public string answer; // Muutettu "vastaus" -> "answer"
}
