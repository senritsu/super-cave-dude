using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelLayout", order = 1)]
public class LevelLayout : ScriptableObject
{
    public float RoomWidth;
    public float RoomHeight;
    public float RoomTransitionDuration;
}
