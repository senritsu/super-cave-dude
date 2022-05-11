using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        Heart,
        Key,
        Secret
    }

    public CollectibleType Type;

    public CollectibleType Collect()
    {
        var audioSource = GetComponentInParent<AudioSource>();
        audioSource.Play();

        gameObject.SetActive(false);

        return Type;
    }
}
