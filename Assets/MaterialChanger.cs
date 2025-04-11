using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialChanger : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Huomaa, ett‰ k‰yt‰mme TextMeshProDropdownin tyyppi‰
    public Material materialA;
    public Material materialB;
    public Material materialC;
    public Renderer objectRenderer; // Renderer-komponentti objektille

    void Start()
    {


        Debug.Log("DropDown step1");
        // Lis‰‰ kuuntelija, joka reagoi Dropdownin arvonmuutoksiin
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        // Tarkista valitun vaihtoehdon indeksi ja vaihda materiaali sen mukaisesti
        Debug.Log("DropDown step2");
        switch (change.value)
        {
            case 0:

                Debug.Log("DropDown A");
                objectRenderer.material = materialA;
                break;
            case 1:

                Debug.Log("DropDown B");
                objectRenderer.material = materialB;
                break;
            case 2:

                Debug.Log("DropDown C");
                objectRenderer.material = materialC;
                break;
        }
    }
}
