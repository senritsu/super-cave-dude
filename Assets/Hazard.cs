using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        var player = other.gameObject.GetComponent<Player>();

        if (player == null) return;
        
        player.Damage(1);
    }
}
