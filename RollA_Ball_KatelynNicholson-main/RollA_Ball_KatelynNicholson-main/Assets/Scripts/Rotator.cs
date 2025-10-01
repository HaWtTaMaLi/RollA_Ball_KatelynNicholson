using UnityEngine;

public class Rotator : MonoBehaviour
{
    public AudioClip pickupSound;   // Drag sound file here in Inspector

    void Update()
    {
        // Keep spinning
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the sound at the pickup's position
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Destroy the pickup
            Destroy(gameObject);
        }
    }
}
