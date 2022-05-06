using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int HealthValue = 1;
    private AudioSource _audioSource;
    public AudioClip AudioClip;

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.gameObject.GetComponent<Player>();

        if (player == null) return;
        
        player.Health += HealthValue;
        AudioSource.PlayClipAtPoint(AudioClip, transform.position);
        
        gameObject.SetActive(false);
    }
}
