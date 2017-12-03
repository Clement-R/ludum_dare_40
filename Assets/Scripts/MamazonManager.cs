using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pkm.EventManager;

public class MamazonManager : MonoBehaviour {

    public Text bunnyCounterUI;
    public Text remainingTimeUI;
    public Animator mamazonSignal;
    public WearManager wearManager;

    [SerializeField]
    private int _maxBunniesInDelivery = 10;
    [SerializeField]
    private float _deliveryDuration = 10f;
    [SerializeField]
    private float _deliveryCooldown = 20f;
    private float _nextDeliveryTime = 0f;

    private int _bunniesInCurrentDelivery = 0;
    private bool _isShipAvailable = false;
    private float _remainingDeliveryTime = 0f;

    public bool IsShipAvailable()
    {
        return _isShipAvailable;
    }

    public int GetBunniesInCurrentDelivery()
    {
        return _bunniesInCurrentDelivery;
    }

    private void Start ()
    {
        EventManager.StartListening("UseDeliveryZone", SellBunny);

        _nextDeliveryTime = Time.time + _deliveryCooldown;

        StartCoroutine(ShipCycle());
    }

    private void SellBunny()
    {
        if(_bunniesInCurrentDelivery <= _maxBunniesInDelivery)
        {
            AkSoundEngine.PostEvent("Play_aspire", gameObject);
            _bunniesInCurrentDelivery++;
            // Increase HUD bunny counter
            bunnyCounterUI.text = _bunniesInCurrentDelivery.ToString();
            wearManager.GainToolbox();

            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetTrigger("putRabbit");
        }
        else
        {
            // TODO : Feedback on max bunnies sold in current delivery
        }
    }

    private void Update ()
    {
		if(Time.time >= _nextDeliveryTime)
        {
            Appear();
        }
        else
        {
            Disappear();
        }
    }

    IEnumerator ShipCycle()
    {
        yield return new WaitForSeconds(_deliveryCooldown - 3f);
        AkSoundEngine.PostEvent("Trigger_mamazon", gameObject);
        mamazonSignal.SetTrigger("mamazonIncoming");
        yield return new WaitForSeconds(3f);
        StartCoroutine(Appear());

        _remainingDeliveryTime = _deliveryDuration;

        // Wait for delivery duration
        while (_remainingDeliveryTime >= 0)
        {
            if (!GameManager.IsGamePaused())
            {
                _remainingDeliveryTime -= Time.deltaTime;
                remainingTimeUI.text = Mathf.CeilToInt(_remainingDeliveryTime).ToString();
            }

            yield return null;
        }
        
        StartCoroutine(Disappear());

        StartCoroutine(ShipCycle());
    }

    IEnumerator Appear()
    {
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("mamazonCome");

        yield return new WaitForSeconds(0.35f);

        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetTrigger("putRabbit");

        _isShipAvailable = true;
        _bunniesInCurrentDelivery = 0;
        bunnyCounterUI.text = _bunniesInCurrentDelivery.ToString();
        yield return null;
    }

    IEnumerator Disappear()
    {
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("mamazonLeave");
        
        _isShipAvailable = false;
        yield return null;
    }
}
