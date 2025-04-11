using UnityEngine;
using System.Diagnostics;

public class WindowsTTS : MonoBehaviour
{
    public void Speak(string message)
    {
        // Korjataan PowerShellin käsittelyä varten
        string safeMessage = message.Replace("'", "''");

        string command = $"powershell -Command \"Add-Type –AssemblyName System.Speech; " +
                         "$speak = New-Object System.Speech.Synthesis.SpeechSynthesizer; " +
                         "$speak.SelectVoice('Microsoft Zira'); " + // Vaihdetaan ääni
                         "$speak.Rate = 0; " + // Puhenopeus, 0 on normaali
                         "$speak.Speak('{safeMessage}');\"";

        ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/C " + command);
        psi.RedirectStandardOutput = false;
        psi.UseShellExecute = true;
        psi.CreateNoWindow = false;

        Process.Start(psi);
    }

    void Start()
    {
        Speak("Hello, welcome to Unity!"); // Testi lause
    }
}
