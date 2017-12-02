using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour {

    private bool _inBunnyDropZone = false;
    private bool _isInMotorZone = false;
    private bool _isInRepairZone = false;

    public bool GetIsInBunnyDropZone()
    {
        return _inBunnyDropZone;
    }

    public bool GetIsInMotorZone()
    {
        return _isInMotorZone;
    }

    public bool GetIsInRepairZone()
    {
        return _isInRepairZone;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "BunnyDrop":
                _inBunnyDropZone = true;
                break;

            case "Motor":
                _isInMotorZone = true;
                break;

            case "RepairRoom":
                _isInRepairZone = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "BunnyDrop":
                _inBunnyDropZone = false;
                break;

            case "Motor":
                _isInMotorZone = false;
                break;

            case "RepairRoom":
                _isInRepairZone = false;
                break;
        }
    }
}
