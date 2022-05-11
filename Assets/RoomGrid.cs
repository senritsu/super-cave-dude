using System.Linq;
using UnityEngine;

public class RoomGrid : MonoBehaviour
{
    public float RoomWidth = 24;
    public float RoomHeight = 14;

    private void OnDrawGizmos()
    {
        foreach (var x in Enumerable.Range(-5, 11))
        {
            foreach (var y in Enumerable.Range(-5, 11))
            {
                var color = Color.green;
                color.a = 0.1f;
                Gizmos.color = color;
                Gizmos.DrawWireCube(new Vector3(x * RoomWidth, y * RoomHeight), new Vector3(RoomWidth, RoomHeight));
            }
        }
    }
}
