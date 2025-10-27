using UnityEngine;

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
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
