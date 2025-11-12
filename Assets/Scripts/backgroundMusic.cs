using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    [Header("BackGround Music")]
    public AudioSource Sound1;
    public AudioSource Sound2;
    public AudioSource Sound3;
    public AudioSource Sound4;
    public int TrackSelector;
    public int TrackHistory;

    public void Start()
    {
        TrackSelector = Random.Range(0, 4);

        if (TrackSelector == 0 && TrackHistory != 1)
        {
            Sound1.Play();
            TrackHistory = 1;
        }
        else if (TrackSelector == 1 && TrackHistory != 2)
        {
            Sound2.Play();
            TrackHistory = 2;
        }
        else if (TrackSelector == 2 && TrackHistory != 3)
        {
            Sound3.Play();
            TrackHistory = 3;
        }
        else if (TrackSelector == 3 && TrackHistory != 4)
        {
            Sound4.Play();
            TrackHistory = 4;
        }
    }

    
    public void Update()
    {
        if (Sound1.isPlaying == false 
            && Sound2.isPlaying == false 
            && Sound3.isPlaying == false 
            && Sound4.isPlaying == false)
        {
            TrackSelector = Random.Range(0, 4);

            if (TrackSelector == 0)
            {
                Sound1.Play();
                TrackHistory = 1;
            }
            else if (TrackSelector == 1)
            {
                Sound2.Play();
                TrackHistory = 2;
            }
            else if (TrackSelector == 2)
            {
                Sound3.Play();
                TrackHistory = 3;
            }
            else if (TrackSelector == 3)
            {
                Sound4.Play();
                TrackHistory = 4;
            }
        }
    }
}
