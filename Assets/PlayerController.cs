using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float _minSpeed = 120f;
    [SerializeField]
    private float _maxSpeed = 1000f;
    [SerializeField]
    private float smoothTime = 0.15f;
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2.SmoothDamp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), ref velocity, smoothTime, _maxSpeed, Time.deltaTime);
        _rb.velocity = velocity;
    }
}
