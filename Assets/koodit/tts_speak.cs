using System;
using System.Speech.Synthesis;


class Program
{
    static void Main(string[] args)
    {
        using (SpeechSynthesizer synth = new SpeechSynthesizer())
        {
            Console.WriteLine("Asennetut ‰‰net:");
            foreach (var voice in synth.GetInstalledVoices())
            {
                var info = voice.VoiceInfo;
                Console.WriteLine($"- {info.Name} ({info.Culture})");
            }

            Console.WriteLine("\nSyˆt‰ puhetekstisi:");
            string text = Console.ReadLine();

            try
            {
                // Valitse kiinan ‰‰ni
                synth.SelectVoice("Microsoft Huihui Desktop"); // Kiinan ‰‰ni (mandariini)

                // Tarkistetaan, onko teksti tyhj‰ tai null
                if (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("Virhe: Puheteksti ei voi olla tyhj‰.");
                }
                else
                {
                    // Yrit‰ puhua tekstist‰
                    synth.Speak(text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Virhe ‰‰nen valinnassa: {ex.Message}");
            }
        }
    }
}
