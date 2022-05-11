using System;
using System.Collections;
using UnityEngine;

public class ScreenwiseMovement : MonoBehaviour
{
    private Camera _camera;
    public PlayerControls Player;
    public LevelLayout LevelLayout;
    private bool _transitioning;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private Vector3 GetNormalizedRelativePosition() => _camera.WorldToViewportPoint(Player.transform.position);
    
    private bool PlayerIsOnScreen()
    {
        var pos = GetNormalizedRelativePosition();

        return pos.x is > 0 and < 1 && pos.y is > 0 and < 1;
    }

    private Vector2 GetDirection()
    {
        var pos = GetNormalizedRelativePosition();
        
        return pos switch
        {
            { x: < 0 } => Vector2.left,
            { x: > 1 } => Vector2.right,
            { y: < 0 } => Vector2.down,
            { y: > 1 } => Vector2.up,
            _ => throw new ArgumentOutOfRangeException(nameof(pos), "player is actually inside bounds")
        };
    }
    
    private void ChangeRoom()
    {
        var direction = GetDirection();
        var screenSize = _camera.ViewportToWorldPoint(Vector3.one) - _camera.ViewportToWorldPoint(Vector3.zero);
        var change = Vector3.Scale(direction, screenSize);
        
        StartCoroutine(MoveCamera(change));
    }
    
    private IEnumerator MoveCamera(Vector3 change)
    {
        _transitioning = true;
        Player.Freeze();
        
        var start = transform.position;
        
        var target = start + change;
        
        var t = 0f;
        while (t < 1)
        {
            t = Mathf.Clamp01(t);
            transform.position = Vector3.Lerp(start, target, t);
            t += (1 / LevelLayout.RoomTransitionDuration) * Time.deltaTime;
            yield return null;
        }

        Player.Unfreeze();
        _transitioning = false;
    }

    private void Update()
    {
        if (!_transitioning && !PlayerIsOnScreen())
        {
            ChangeRoom();
        }
    }
}
