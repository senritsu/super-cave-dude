using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    public int Health { get; private set; }

    public int MaxHealth = 3;
    public float InvulnerabilityTime;

    [field: SerializeField]
    public bool HasKey { get; private set; }

    [field: SerializeField]
    public int Secrets { get; private set; }

    public AudioClip HurtSound;
    public AudioClip DeathSound;
    public AudioClip WinSound;
    private AudioSource _audioSource;
    private Animator _animator;
    public bool IsDead => Health <= 0;
    private static readonly int IsDeadHash = Animator.StringToHash("IsDead");

    private float _remainingInvulnerabilityTime;
    private bool IsInvulnerable => _remainingInvulnerabilityTime > 0;
    private static readonly int IsInvulnerableHash = Animator.StringToHash("IsInvulnerable");

    private void Awake()
    {
        Health = MaxHealth;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsInvulnerable)
        {
            _remainingInvulnerabilityTime -= Time.deltaTime;
            
            if (!IsInvulnerable)
            {
                _animator.SetBool(IsInvulnerableHash, false);
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            BackToMenu();
        }
    }

    private IEnumerator Die()
    {
        _audioSource.PlayOneShot(DeathSound);
        _animator.SetBool(IsDeadHash, true);
        
        yield return new WaitForSeconds(2);

        BackToMenu();
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Damage(int damage)
    {
        if (IsDead) return;
        if (damage <= 0) return;
        if (IsInvulnerable) return;

        Health -= damage;
        _audioSource.PlayOneShot(HurtSound);

        if (IsDead)
        {
            StartCoroutine(Die());
        }
        else
        {
            _animator.SetBool(IsInvulnerableHash, true);

            _remainingInvulnerabilityTime = InvulnerabilityTime;
        }
    }

    private IEnumerator VictoryCelebration()
    {
        _audioSource.PlayOneShot(WinSound);
        
        yield return null;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var collectible = col.GetComponentInChildren<Collectible>();

        if (collectible)
        {
            switch (collectible.Collect())
            {
                case Collectible.CollectibleType.Heart:
                {
                    if (Health < MaxHealth)
                    {
                        Health++;
                    }
                    break;
                }
                case Collectible.CollectibleType.Key:
                {
                    HasKey = true;
                    break;
                }
                case Collectible.CollectibleType.Secret:
                {
                    Secrets++;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        var treasureChest = col.GetComponent<TreasureChest>();

        if (treasureChest)
        {
            StartCoroutine(VictoryCelebration());
        }
    }
}
