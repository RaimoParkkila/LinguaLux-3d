using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SupabaseButtonSpawner : MonoBehaviour
{
    public static SupabaseButtonSpawner Instance;

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


    public GameObject buttonPrefab;
    public Transform buttonContainer;

    public void GenerateButtons(string[] vaihtoehdot)
    {
        foreach (Transform lapsi in buttonContainer)
        {
            Destroy(lapsi.gameObject); // Tyhjennet‰‰n vanhat napit
        }

        foreach (var vaihtoehto in vaihtoehdot)
        {
            GameObject uusiNappi = Instantiate(buttonPrefab, buttonContainer);
            uusiNappi.GetComponentInChildren<TMPro.TMP_Text>().text = vaihtoehto;



            uusiNappi.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Klikattiin: " + vaihtoehto);
            });
        }
    }
}
