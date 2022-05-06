using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public int Speed = 5;
    public int JumpForce = 5;

    public Transform GroundCornerTopLeft;
    public Transform GroundCornerBottomRight;

    private float _direction;
    private bool _shouldJump;

    public AudioClip JumpSound;
    private AudioSource _audioSource;

    Rigidbody2D _rigidBody;    // Start is called before the first frame update

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        var axisDirection = Input.GetAxis("Horizontal");
        _direction = axisDirection < 0 ? -1 : axisDirection > 0 ? 1 : 0;

        _shouldJump = Input.GetButton("Jump");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var isGrounded = Physics2D.OverlapArea(GroundCornerTopLeft.position, GroundCornerBottomRight.position, LayerMask.GetMask("Ground")) != null;
        
        var x = _rigidBody.velocity.x;
        var y = _rigidBody.velocity.y;

        if (_shouldJump && isGrounded) {
            _audioSource.PlayOneShot(JumpSound);
            y = JumpForce;

            _shouldJump = false;
        }

        if (isGrounded) {
            x = _direction * Speed;
        } else {
            x = Mathf.Lerp(x, _direction * Speed, 0.1f);
        }

        _rigidBody.velocity = new Vector2(x, y);
    }
}
