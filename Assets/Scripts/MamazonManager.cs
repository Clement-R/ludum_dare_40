using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class MamazonManager : MonoBehaviour {

	void Start () {
        EventManager.StartListening("UseDeliveryZone", SellBunny);
    }

    private void SellBunny()
    {
        WearManager.GainToolbox();
    }


    void Update () {
		
	}
}
