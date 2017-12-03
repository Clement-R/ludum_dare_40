using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pkm.EventManager;

public class MotorBehavior : MonoBehaviour {

    # region PUBLIC_VARIABLES
    public BackgroundSwaper backgroundManager;
    public Sprite[] motorLevelSprites;
    public Text textScore;

    public Image copMeter;
    public Image distanceMeter;
    public Text distanceText;
    #endregion PUBLIC_VARIABLES

    #region PRIVATE_VARIABLES
    [Header("UI")]
    private float copMeterPosition;
    private float distanceMeterPosition;

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
        _copDistance = _initialCopDistance;

        StartCoroutine(PlayLowSpeedSound());
    }

    IEnumerator PlayLowSpeedSound()
    {
        while(true)
        {
            if(_cappedSpeed <= 2 && Time.time > 1f)
            {
                AkSoundEngine.PostEvent("Play_low_speed", gameObject);
                yield return new WaitForSeconds(5f);
            }
            yield return null;
        }
    }

    private void Update ()
    {
        if(!GameManager.IsGamePaused())
        {
            // Update speed and clamp it
            _speed -= _decreaseRate;
            _speed = Mathf.Clamp(_speed, 0, _maxSpeed);

            // Get capped speed
            _cappedSpeed = Mathf.CeilToInt(_speed / 100f);

            // Update motor level sprite
            if (_cappedSpeed - 1 >= 0)
            {
                _motorLevelSr.sprite = motorLevelSprites[_cappedSpeed - 1];
            }
            else
            {
                _motorLevelSr.sprite = null;
            }

            // Update motor level sound
            AkSoundEngine.SetRTPCValue("speed_vaisseau", (_cappedSpeed * 100) / 6f);

            // Update distances
            _distance += _speed;
            _copDistance += _copSpeed;

            // Get the delta between the player speed and the cop speed
            float distanceDelta = _distance - _copDistance;
            if (distanceDelta < 0f)
            {
                distanceDelta = 0f;
            }

            // If distance == 0 then the player lose
            if (distanceDelta <= 0f && Time.timeScale > 0)
            {
                EventManager.TriggerEvent("LoseCop");
            }

            if (distanceDelta <= (300 * 60 * 7) && Time.timeScale > 0)
            {
                AkSoundEngine.PostEvent("Play_police", gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent("Stop_police", gameObject);
            }

            // Update text score
            textScore.text = (_distance - _initialDistance).ToString();

            // Update distance meter
            distanceText.text = Mathf.FloorToInt(distanceDelta).ToString();

            backgroundManager.ChangeBackgroundSpeed(_cappedSpeed);

            // TODO : Toggle an icon when there is 2 level of motor
            // TODO : Show a danger sign when distance is close (cop sound)
        }
    }

    private void AddPower()
    {
        _speed += _bunnyPower;
        _speed = Mathf.Clamp(_speed, 0, _maxSpeed);

        // Feedbacks
        GetComponentInChildren<ParticleSystem>().Play();
        AkSoundEngine.PostEvent("Play_four", gameObject);
        _motorTube.SetTrigger("put_RABBIT");
    }
}
