using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("PickUp Clip")]
    public AudioClip pickupSound;   // Drag sound file here in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the sound at the pickup's position
            SoundManager.PlaySound(SoundType.COLLECTED);

            // Destroy the pickup
            Destroy(gameObject);
        }
    }
}
