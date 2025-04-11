using UnityEngine;
using UnityEngine.Video;

public class VideoChanger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videos;
    private int currentVideoIndex = 0;

    void Start()
    {
        // Aseta ensimmäinen video toistettavaksi
        videoPlayer.clip = videos[currentVideoIndex];
        videoPlayer.Play();
    }

    // Metodi vaihtaa seuraavaa videota
    public void ChangeToNextVideo()
    {
        // Tarkista, onko seuraavaa videota olemassa
        if (currentVideoIndex + 1 < videos.Length)
        {
            currentVideoIndex++;
            videoPlayer.clip = videos[currentVideoIndex];
            videoPlayer.Play();
        }
    }

    // Metodi vaihtaa edelliseen videota
    public void ChangeToPreviousVideo()
    {
        // Tarkista, onko edellistä videota olemassa
        if (currentVideoIndex - 1 >= 0)
        {
            currentVideoIndex--;
            videoPlayer.clip = videos[currentVideoIndex];
            videoPlayer.Play();
        }
    }
}
