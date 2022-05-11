using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationMarker : MonoBehaviour
{
    private void Update()
    {
        var x = transform.position.x;
        transform.position = new Vector3(x, EventSystem.current.currentSelectedGameObject.transform.position.y);
    }
}
