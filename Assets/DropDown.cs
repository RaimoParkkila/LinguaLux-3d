using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DropDown : MonoBehaviour

 




{

    public TMP_Dropdown dropdown;
    public Material materialA;
    public Material materialB;
    public Material materialC;
    public Renderer panelRenderer;
    public TextMeshProUGUI output;
    // Start is called before the first frame update


    void HandleInputData(TMP_Dropdown change)
    {
        // Tarkista valitun vaihtoehdon indeksi ja vaihda materiaali sen mukaisesti

        Debug.Log("DropDown step1");
        switch (change.value)
        {
            case 0:
                panelRenderer.material = materialA;
                break;
            case 1:
                panelRenderer.material = materialB;
                break;
            case 2:
                panelRenderer.material = materialC;
                break;
        }
        Debug.Log("DropDown step2");
 
    }
 
    void Start()
    {
        // Lis‰‰ kuuntelija, joka reagoi Dropdownin arvonmuutoksiin
        dropdown.onValueChanged.AddListener(delegate {
            HandleInputData(dropdown);
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
