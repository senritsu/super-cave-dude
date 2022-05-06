using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float Amplitude = 0.1f;
    public float Period = 2;
    public float ScaleX = 1;
    public float ScaleY = 1;

    private float _seed;

    // Start is called before the first frame update
    void Start()
    {
        _seed = Random.value * 5;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Mathf.Sin(_seed + Time.time * 2 * Mathf.PI / Period) * Amplitude * ScaleX;
        var y = Mathf.Cos(_seed + Time.time * 2 * Mathf.PI / Period * 0.4f) * Amplitude * ScaleY;
        transform.localPosition = new Vector2(x, y);
    }
}
