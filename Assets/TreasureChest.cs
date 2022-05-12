using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");
    private ParticleSystem _particleSystem;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();

        if (!player) return;

        _animator.SetTrigger(Open);
        _particleSystem.Play();
        _collider.enabled = false;
    }
}
