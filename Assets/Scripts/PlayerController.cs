using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float _minSpeed = 120f;
    [SerializeField]
    private float _maxSpeed = 1000f;
    [SerializeField]
    private float _smoothTime = 0.15f;
    
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.IsGamePaused())
        {
            // Move the character
            Vector2.SmoothDamp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), ref velocity, _smoothTime, _maxSpeed, Time.deltaTime);
            _rb.velocity = velocity;

            // Flip character sprite
            if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                _sr.flipY = true;
                transform.GetChild(0).transform.localPosition = new Vector2(transform.GetChild(0).transform.localPosition.x, -59f);
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));
            }
            else
            {
                _sr.flipY = false;
                transform.GetChild(0).transform.localPosition = new Vector2(transform.GetChild(0).transform.localPosition.x, 59f);
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}
