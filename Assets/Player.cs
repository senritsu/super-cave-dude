using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    public Transform Spawn;
    [field: SerializeField]
    public int Health { get; set; }

    public AudioClip DeathSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Respawn();
    }

    private void Respawn() {
        Health = 0;

        foreach (Transform child in GameObject.Find("Hearts").transform)
        {
            child.gameObject.SetActive(true);
        }

        transform.position = Spawn.position;
    }

    public void Damage(int damage)
    {
        if (damage <= 0) return;
        
        _audioSource.PlayOneShot(DeathSound);
        Respawn();
    }
}
