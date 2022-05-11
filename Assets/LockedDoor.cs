using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private Collider2D _collider;
    private Animator _animator;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private AudioSource _audioSource;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>(); 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();

        if (player.HasKey)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        _animator.SetBool(IsOpen, true);
        _collider.enabled = false;
        _audioSource.Play();
    }
}
