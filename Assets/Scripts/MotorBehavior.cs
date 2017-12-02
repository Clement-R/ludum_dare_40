using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class MotorBehavior : MonoBehaviour {

    [SerializeField]
    private int _maxSpeed = 600;
    [SerializeField]
    private int _initialSpeed = 600;
    [SerializeField]
    private int _decreasePerSecond = 5;
    [SerializeField]
    private int _bunnyPower = 25;

    private float _speed;
    private float _decreaseRate;

    private void Start ()
    {
        EventManager.StartListening("UseBunnyInMotor", AddPower);
        _speed = _initialSpeed;
        _decreaseRate = _decreasePerSecond / 60f;
    }
	
    private void AddPower()
    {
        _speed += _bunnyPower;
        _speed = Mathf.Clamp(_speed, 0, _maxSpeed);
    }

	private void Update ()
    {
        _speed -= _decreaseRate;
	}
}
