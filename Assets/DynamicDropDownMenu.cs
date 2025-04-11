using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class DynamicDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Renderer panelRenderer;
    public string directoryPath = "Assets/kuvat"; // Hakemiston polku, jossa kuvat sijaitsevat 
    public List<string> imagePaths; // Lista kuvatiedostojen poluista

    void Start()
    {


        // Varmista, ett‰ tarvittavat komponentit on asetettu
        if (dropdown == null || panelRenderer == null)
        {
            Debug.LogError("Dropdown or Panel Renderer is not assigned in the Inspector!");
            return;
        }

        try
        {
            CreateDropdown();
        }
        catch (Exception e)
        {
            Debug.LogError("Virhe tiedostojen k‰sittelyss‰: " + e.Message);
        }

        // Alusta lista kuvatiedostojen poluille
        imagePaths = new List<string>();

        // Hae kaikki .jpg-tiedostot annetusta hakemistosta
        string[] files = Directory.GetFiles(directoryPath, "*.jpg");
        Debug.Log("RIVI 22");

        // Lis‰‰ kaikki .jpg-tiedostojen polut listaan
        foreach (string file in files)
        {
            Debug.Log("RIVI 27");

            Debug.Log(file);
            imagePaths.Add(file);
        }

        // Luo pudotusvalikko ja lis‰‰ vaihtoehdot

        try
        {
            CreateDropdown();
        }



        catch (Exception e)
        {
            Debug.LogError("Virhe tiedostojen k‰sittelyss‰: " + e.Message);
        }
    }

    void CreateDropdown()
    {
        // Tyhjenn‰ pudotusvalikon vaihtoehdot
        dropdown.ClearOptions();

        // Luo lista vaihtoehdoista
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();



     

        // Lis‰‰ vaihtoehdot pudotusvalikkoon
        foreach (string imagePath in imagePaths)
        {
            // Hae tiedostonimi ilman polkua
            string fileName = Path.GetFileName(imagePath);

            // Luo uusi vaihtoehto, jossa tiedostonimi
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(fileName);

            // Lis‰‰ vaihtoehto listaan
            options.Add(option);
        }

        // Lis‰‰ vaihtoehdot pudotusvalikkoon
        dropdown.AddOptions(options);

        // Lis‰‰ kuuntelija, joka reagoi Dropdownin arvonmuutoksiin
        dropdown.onValueChanged.AddListener(delegate {
            HandleDropdownChange(dropdown);
        });
    }

    void HandleDropdownChange(TMP_Dropdown change)
    {
        // Tarkista valitun vaihtoehdon indeksi
        int selectedIndex = change.value;

        // Lataa ja aseta valittu kuva paneeliin
        if (selectedIndex >= 0 && selectedIndex < imagePaths.Count)
        {
            string selectedImagePath = imagePaths[selectedIndex];

            // Lataa tekstuuria paneeliin (olettaen ett‰ paneeli on Renderer-komponentti)
            Texture2D texture = LoadTexture(selectedImagePath);
            if (texture != null)
            {
                panelRenderer.material.mainTexture = texture;
            }
        }
    }

    Texture2D LoadTexture(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // Lataa kuvadata tekstuuriksi
        return texture;
    }
}
