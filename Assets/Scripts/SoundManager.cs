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

    public float savedMusic = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
    public float savedSFX = PlayerPrefs.GetFloat(SFX_KEY, 1f);

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

        AudioMixer.SetFloat("Music", savedMusic);
        AudioMixer.SetFloat("SFX", savedSFX);

        if (musicSlider != null) musicSlider.value = savedMusic;
        if (sfxSlider != null) sfxSlider.value = savedSFX;
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);

    }

    public void SetSFXVolume(float volume)
    {
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
