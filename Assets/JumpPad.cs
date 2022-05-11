using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float Strength = 15;

    private static readonly int Bounce = Animator.StringToHash("Bounce");
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var rigidBody = col.GetComponent<Rigidbody2D>();
        
        if (rigidBody == null) return;
        
        GetComponent<Animator>().SetTrigger(Bounce);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, Strength);

        var betterJump = col.GetComponent<BetterJump>();
        if (betterJump)
        {
            betterJump.Disable();
        }
        
        _audioSource.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + 6 * Vector3.up);
    }
}
