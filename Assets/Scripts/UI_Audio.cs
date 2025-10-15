using UnityEngine;

public class UI_Audio : MonoBehaviour
{
    public AudioSource hoverSound;
    public AudioSource clickSound;

    void Start()
    {
        
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlayClick()
    {
        clickSound.Play();
    }
}
