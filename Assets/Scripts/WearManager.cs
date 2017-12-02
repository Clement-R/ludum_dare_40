using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class WearManager : MonoBehaviour {

    struct Wall
    {
        public int health;
    }

    public float scaleFactor = 1.5f;
    public float repairValue = 25f;

    private Wall[] walls = new Wall[4];

    private void Start()
    {
        EventManager.StartListening("UseRepairZone", RepairShip);

        // Create logical walls
        for (int i = 0; i < 4; i++)
        {
            walls[i] = new Wall();
            walls[i].health = 6;
        }
    }

    private void Update ()
    {


        // Increase the wear rate when we've more bunnies
        //_increasePerSecond = (ResourceManager.GetNumberOfBunnies() / (float) ResourceManager.maxBunnies) * scaleFactor;
        //_increaseRate = _increasePerSecond / 60f;

        //_wear += _increaseRate;

        //print(_wear);
        
        // If the wear is over the maximum wear value we lose
        //if(_wear >= _maxWear && Time.timeScale > 0)
        //{
        //    EventManager.TriggerEvent("LoseWear");
        //}


	}

    private void RepairShip()
    {
        //_wear -= repairValue;
        //_wear = Mathf.Clamp(_wear, 0f, _maxWear);
    }
}
