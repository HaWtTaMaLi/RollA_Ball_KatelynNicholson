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
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource hoverSound;
    public AudioSource clickSound;
    private AudioSource audioSource;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] soundList;
    [SerializeField] public AudioMixer AudioMixer;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;

    private const string MUSIC_KEY = "Music";
    private const string SFX_KEY = "SFX";

    private void Awake()
    {

        //singleton setup
         if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //attach slider listeners
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //load saved volume or def to 1
        float savedMusic = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float savedSFX = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        //clamp
        savedMusic = Mathf.Max(savedMusic, 0, 0001f);
        savedSFX = Mathf.Max(savedSFX, 0.0001f);

        //apply to mixer
        AudioMixer.SetFloat("Music", Mathf.Log10(savedMusic) * 20);
        AudioMixer.SetFloat("SFX", Mathf.Log10(savedSFX) * 20);

        //set slider
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;

        //slider listener
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Max(volume, 0.0001f);
        AudioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);

    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Max(volume, 0.0001f);
        AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFX_KEY, volume);
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
        if (instance == null || instance.soundList == null) return;
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
