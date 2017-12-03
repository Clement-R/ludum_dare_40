using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class MamazonManager : MonoBehaviour {

    [SerializeField]
    private int _maxBunniesInDelivery = 10;
    [SerializeField]
    private float _deliveryDuration = 10f;
    [SerializeField]
    private float _deliveryCooldown = 20f;
    private float _nextDeliveryTime = 0f;

    static private int _bunniesInCurrentDelivery = 0;
    static private bool _isShipAvailable = false;

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
            // TODO : Increase HUD bunny counter
            WearManager.GainToolbox();
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
        yield return new WaitForSeconds(_deliveryDuration);
        StartCoroutine(Disappear());

        StartCoroutine(ShipCycle());
    }

    IEnumerator Appear()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        _isShipAvailable = true;
        _bunniesInCurrentDelivery = 0;
        yield return null;
    }

    IEnumerator Disappear()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        _isShipAvailable = false;
        yield return null;
    }
}
