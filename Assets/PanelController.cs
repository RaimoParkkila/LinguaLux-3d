using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;
    private bool isPanelVisible = true;
    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        // Tallenna alkuperäinen koko ja sijainti
        originalScale = panel.transform.localScale;
        originalPosition = panel.transform.position;
    }

    public void TogglePanelVisibility()
    {
        isPanelVisible = !isPanelVisible;
        if (isPanelVisible)
        {
            // Näytä paneeli muuttamalla sen kokoa suuremmaksi

            Debug.Log("SUURENNA");
            EnlargePanel();
        }
        else
        {
            // Piilota paneeli muuttamalla sen kokoa pienemmäksi
            Debug.Log("PIENENNÄ");
            ShrinkPanel();
        }
    }

    public void ShrinkPanel()
    {
        // Aseta paneelin koko pienemmäksi
        panel.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void EnlargePanel()
    {
        // Palauta paneelin alkuperäinen koko
       panel.transform.localScale = originalScale;

       // panel.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
    }

    public void IntensityWall()
    {
        // Tee jotain, kun IntensityWall-nappia painetaan
        Debug.Log("IntensityWall-nappia painettiin!");
    }
}
