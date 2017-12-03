﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pkm.EventManager;

public class MamazonManager : MonoBehaviour {

    public Text bunnyCounterUI;
    public Text remainingTimeUI;

    [SerializeField]
    private int _maxBunniesInDelivery = 10;
    [SerializeField]
    private float _deliveryDuration = 10f;
    [SerializeField]
    private float _deliveryCooldown = 20f;
    private float _nextDeliveryTime = 0f;

    static private int _bunniesInCurrentDelivery = 0;
    static private bool _isShipAvailable = false;
    private float _remainingDeliveryTime = 0f;

    public static bool IsShipAvailable()
    {
        return _isShipAvailable;
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
            _bunniesInCurrentDelivery++;
            // Increase HUD bunny counter
            bunnyCounterUI.text = _bunniesInCurrentDelivery.ToString();
            WearManager.GainToolbox();

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
        yield return new WaitForSeconds(_deliveryCooldown);
        StartCoroutine(Appear());

        _remainingDeliveryTime = _deliveryDuration;

        // Wait for delivery duration
        while (_remainingDeliveryTime >= 0)
        {
            _remainingDeliveryTime -= Time.deltaTime;
            remainingTimeUI.text = Mathf.CeilToInt(_remainingDeliveryTime).ToString();
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
