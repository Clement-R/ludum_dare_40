using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class MotorBehavior : MonoBehaviour {

# region PUBLIC_VARIABLES
    public Sprite[] motorLevelSprites;
    #endregion PUBLIC_VARIABLES

    #region PRIVATE_VARIABLES
    [Header("Speed")]
    [SerializeField]
    private int _maxSpeed = 600;
    [SerializeField]
    private int _initialSpeed = 600;
    [SerializeField]
    private int _decreasePerSecond = 5;
    [SerializeField]
    private int _bunnyPower = 25;
    
    private float _speed;
    private int _cappedSpeed;

    [Header("Distance")]
    [SerializeField]
    private float _initialDistance = 1000f;
    [SerializeField]
    private float _initialCopDistance = 0f;
    [SerializeField]
    private float _copSpeed = 300f;

    private float _distance;
    private float _copDistance;
    private float _decreaseRate;

    private SpriteRenderer _motorLevelSr;
    private Animator _motorTube;
#endregion PRIVATE_VARIABLES

    private void Start ()
    {
        EventManager.StartListening("UseBunnyInMotor", AddPower);
        _speed = _initialSpeed;
        _decreaseRate = _decreasePerSecond / 60f;

        _distance = _initialDistance;

        _motorLevelSr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _motorTube = transform.GetChild(1).GetComponent<Animator>();
    }
	
    private void AddPower()
    {
        _speed += _bunnyPower;
        _speed = Mathf.Clamp(_speed, 0, _maxSpeed);

        // TODO : play particle system
        GetComponentInChildren<ParticleSystem>().Play();
        // TODO : play sound
        // TODO : play tube animation
        _motorTube.SetTrigger("put_RABBIT");
    }

    private void Update ()
    {
        // Update speed and clamp it
        _speed -= _decreaseRate;
        _speed = Mathf.Clamp(_speed, 0, _maxSpeed);

        // Get capped speed
        _cappedSpeed = Mathf.CeilToInt(_speed / 100f);
        
        // Update motor level sprite
        if(_cappedSpeed - 1 >= 0)
        {
            _motorLevelSr.sprite = motorLevelSprites[_cappedSpeed - 1];
        } else
        {
            _motorLevelSr.sprite = null;
        }

        // Update distances
        _distance += _speed;
        _copDistance += _copSpeed;

        // Get the delta between the player speed and the cop speed
        float distanceDelta = _distance - _copDistance;
        
        // If distance == 0 then the player lose
        if (distanceDelta <= 0f)
        {
            EventManager.TriggerEvent("LoseCop");
        }
    }
}
