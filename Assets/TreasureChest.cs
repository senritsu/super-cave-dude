using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();

        if (!player) return;

        _animator.SetTrigger(Open);
    }
}
