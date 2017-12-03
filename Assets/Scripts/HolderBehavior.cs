using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class HolderBehavior : MonoBehaviour {

    [SerializeField]
    private float _grabRadius = 150f;

    private GameObject _holdedObject = null;
    private InteractController interactionController;

    private void Awake()
    {
        interactionController = GetComponent<InteractController>();
    }

    private void FixedUpdate()
    {
        // Find nearest bunny
        float nearestDistance = _grabRadius;
        GameObject nearestBunny = null;
        foreach (var bunny in GameObject.FindGameObjectsWithTag("Bunny"))
        {
            float bunnyDistance = Vector2.Distance(bunny.transform.position, transform.position);
            if (bunnyDistance < nearestDistance)
            {
                nearestDistance = bunnyDistance;
                nearestBunny = bunny.gameObject;
            }
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            // If we can drop a bunny
            if (CanDropBunny())
            {
                if(interactionController.GetIsInBunnyDropZone())
                {
                    DropObject();
                }
                else if (interactionController.GetIsInMotorZone())
                {
                    UseBunny();
                }
            }
            else if (nearestBunny != null)
            {
                if(interactionController.GetIsInBunnyDropZone())
                {
                    // Grab nearest bunny if there is one
                    HoldObject(nearestBunny);
                }
            }
            else if(_holdedObject == null)
            {
                UseZone();
            }
        }
    }

    private bool CanDropBunny()
    {
        if((interactionController.GetIsInBunnyDropZone() || interactionController.GetIsInMotorZone()) && _holdedObject != null && _holdedObject.tag == "Bunny")
        {
            return true;
        }

        return false;
    }

    private void UseBunny()
    {
        if(interactionController.GetIsInMotorZone())
        {
            KillObject();
            // Decrease bunny counter
            EventManager.TriggerEvent("KillBunny");
            // Add power to the motor
            EventManager.TriggerEvent("UseBunnyInMotor");
        }
    }

    private void UseObject()
    {
        if(_holdedObject.tag == "Bunny")
        {
            UseBunny();
        }
    }

    private void UseZone()
    {
        if (interactionController.GetIsInWallZone())
        {
            EventManager.TriggerEvent("UseRepairZone");
        }
    }

    private void KillObject()
    {
        Destroy(_holdedObject);
        _holdedObject = null;
    }

    private void DropObject()
    {
        _holdedObject.GetComponent<Rigidbody2D>().simulated = true;
        _holdedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _holdedObject.transform.parent = null;

        _holdedObject = null;
    }

    private void HoldObject(GameObject holdedObject)
    {
        holdedObject.transform.parent = transform.GetChild(0).transform;
        holdedObject.GetComponent<Rigidbody2D>().simulated = false;
        holdedObject.transform.position = transform.GetChild(0).transform.position;

        _holdedObject = holdedObject;
    }
}
