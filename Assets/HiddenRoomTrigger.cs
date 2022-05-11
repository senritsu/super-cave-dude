using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenRoomTrigger : MonoBehaviour
{
    private GameObject[] _siblings;
    private Tilemap _levelTilemap;
    private Tilemap _hiddenRoomTilemap;
    private AudioSource _audioSource;
    private BoxCollider2D _collider;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _levelTilemap = GameObject.Find("Level").GetComponent<Tilemap>();
        _hiddenRoomTilemap = FindObjectsOfType<Tilemap>(true).Single(x => x.gameObject.name == "Hidden Room Tiles");
        
        _hiddenRoomTilemap.gameObject.SetActive(false);

        _siblings = Enumerable.Range(0, transform.parent.childCount)
            .Select(i => transform.parent.GetChild(i).gameObject)
            .Where(x => x != gameObject)
            .ToArray();
        
        foreach (var sibling in _siblings)
        {
            sibling.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();

        if (player == null) return;
        
        foreach (var area in GetComponentsInChildren<BoxCollider2D>().Where(x => x.gameObject != gameObject))
        {
            var center = (Vector2)area.gameObject.transform.position + area.offset;
            var size = area.size;
            var topLeft = center - 0.5f * size;
            var bottomRight = center + 0.5f * size;

            var topLeftIndex = _hiddenRoomTilemap.layoutGrid.WorldToCell(topLeft);
            var bottomRightIndex = _hiddenRoomTilemap.layoutGrid.WorldToCell(bottomRight);

            for (var y = topLeftIndex.y; y <= bottomRightIndex.y; y++)
            {
                for (var x = topLeftIndex.x; x <= bottomRightIndex.x; x++)
                {
                    var pos = new Vector3Int(x, y);
                    var tile = _hiddenRoomTilemap.GetTile(pos);
                    if (tile)
                    {
                        _levelTilemap.SetTile(pos, tile);
                    }
                }
            }
            
            area.gameObject.SetActive(false);
        }
        
        foreach (var sibling in _siblings)
        {
            sibling.SetActive(true);
        }

        _audioSource.Play();

        _sprite.enabled = false;
        _collider.enabled = false;
    }
}
