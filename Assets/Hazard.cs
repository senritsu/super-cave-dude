using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.gameObject.GetComponent<Player>();

        if (player != null) {
            player.Damage(1);
        }
    }
}
