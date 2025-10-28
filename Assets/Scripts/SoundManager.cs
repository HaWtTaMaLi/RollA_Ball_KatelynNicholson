using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SoundType
{
    HITSOUND,//should match sound order 
    BGMUSIC1,
    BGMUSIC2,
    BGMUSIC3,
    BGMUSIC4,
    COLLECTED,
    CLICK,
    HOVER,

}

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    public AudioSource hoverSound;
    public AudioSource clickSound;

    [SerializeField] private AudioClip[] soundList;
    public static SoundManager instance;
    private AudioSource audioSource;

    [SerializeField] public AudioMixer AudioMixer;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

         if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("Music", volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioMixer.SetFloat("SFX", volume);
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlayClick()
    {
        clickSound.Play();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
