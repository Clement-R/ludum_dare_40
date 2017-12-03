using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    public float scrollSpeed = 100f;
    private Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = new Vector2(-scrollSpeed, 0);
    }

    public void UpdateSpeed(float scrollSpeed)
    {
        _rb2d.velocity = new Vector2(-scrollSpeed, 0);
    }
}
