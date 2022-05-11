using UnityEngine;

public class BetterJump : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public bool Disabled;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Disable()
    {
        Disabled = true;
        _rigidBody.gravityScale = 2;
    }

    public void Enable()
    {
        Disabled = false;
    }

    private void FixedUpdate()
    {
        if (Disabled) return;
        
        if (_rigidBody.velocity.y < 0)
        {
            _rigidBody.gravityScale = 5f;
        }
        else if (!Input.GetButton("Jump"))
        {
            _rigidBody.gravityScale = 4;
        } else
        {
            _rigidBody.gravityScale = 2;
        }
    }
}
