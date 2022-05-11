using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public int Speed;
    public int JumpForce;
    public float TerminalVelocity;
    public float JumpBufferTime;
    
    public Transform GroundCornerTopLeft;
    public Transform GroundCornerBottomRight;

    private float _direction;
    private double _remainingJumpBuffer;

    public AudioClip JumpSound;
    private AudioSource _audioSource;
    
    private bool _isJumping;

    Rigidbody2D _rigidBody;    // Start is called before the first frame update
    private Animator _animator;
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int HorizontalMovement = Animator.StringToHash("HorizontalMovement");
    private static readonly int VerticalMovement = Animator.StringToHash("VerticalMovement");
    private SpriteRenderer _sprite;
    private Player _player;
    private BetterJump _betterJump;
    private bool _isGrounded;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _player = GetComponent<Player>();
        _betterJump = GetComponent<BetterJump>();
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        var axisDirection = Input.GetAxis("Horizontal");
        _direction = axisDirection < 0 ? -1 : axisDirection > 0 ? 1 : 0;

        if (Input.GetButtonDown("Jump"))
        {
            _remainingJumpBuffer = JumpBufferTime;
        }
    }

    private void Jump()
    {        
        _remainingJumpBuffer = 0;
        _isJumping = true;
        _audioSource.PlayOneShot(JumpSound, 1);
    }

    private void LandOnGround()
    {
        _isJumping = false;
        _betterJump.Enable();
    }

    private void UpdateAnimator()
    {
        _animator.SetBool(IsJumping, _isJumping);
        _animator.SetFloat(HorizontalMovement, Mathf.Abs(_rigidBody.velocity.x));
        if (Mathf.Abs(_rigidBody.velocity.x) > 0)
        {
            _sprite.flipX = _rigidBody.velocity.x < 0;
        }
        _animator.SetFloat(VerticalMovement, _rigidBody.velocity.y);
    }

    private void FixedUpdate()
    {
        var wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapArea(GroundCornerTopLeft.position, GroundCornerBottomRight.position, LayerMask.GetMask("Ground")) != null;

        if (!wasGrounded && _isGrounded)
        {
            LandOnGround();
        }

        if (_player.IsDead)
        {
            UpdateAnimator();
            return;
        }
        
        var x = _rigidBody.velocity.x;
        var y = _rigidBody.velocity.y;

        if (_remainingJumpBuffer > 0) {
            if (_isGrounded)
            {
                Jump();
                y = JumpForce;
            }

            _remainingJumpBuffer -= Time.deltaTime;
        }

        if (_isGrounded) {
            x = _direction * Speed;
        } else {
            x = Mathf.Lerp(x, _direction * Speed, 0.1f);
        }

        _rigidBody.velocity = new Vector2(x, Mathf.Clamp(y, -TerminalVelocity, 2*TerminalVelocity));
        
        UpdateAnimator();
    }

    public void Freeze()
    {
        _rigidBody.simulated = false;
    }

    public void Unfreeze()
    {
        _rigidBody.simulated = true;
    }
}
