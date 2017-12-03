using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class HolderBehavior : MonoBehaviour {
    public MamazonManager mamazon;
    public WearManager wearManager;

    public Sprite idleSprite;
    public Sprite holdingSprite;

    [SerializeField]
    private float _grabRadius = 150f;

    private GameObject _holdedObject = null;
    private InteractController interactionController;

    private SpriteRenderer _sr;

    private void Awake()
    {
        interactionController = GetComponent<InteractController>();
    }

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(!GameManager.IsGamePaused())
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

            if (Input.GetMouseButtonDown(0))
            {
                // If we can drop a bunny
                if (CanDropBunny())
                {
                    if (interactionController.GetIsInBunnyDropZone())
                    {
                        DropObject();
                    }
                    else if (interactionController.GetIsInMotorZone() || interactionController.GetIsInDeliveryZone())
                    {
                        UseBunny();
                    }
                }
                else if (nearestBunny != null)
                {
                    if (interactionController.GetIsInBunnyDropZone())
                    {
                        // Grab nearest bunny if there is one
                        HoldObject(nearestBunny);
                    }
                }
                else if (_holdedObject == null)
                {
                    UseZone();
                }
            }
        }
    }

    private bool CanDropBunny()
    {
        if((interactionController.GetIsInBunnyDropZone() || interactionController.GetIsInMotorZone() || interactionController.GetIsInDeliveryZone()) && _holdedObject != null && _holdedObject.tag == "Bunny")
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

        if (interactionController.GetIsInDeliveryZone())
        {
            if(mamazon.IsShipAvailable())
            {
                // Check if the player doesn't already have max toolbox
                if (wearManager.GetRemainingToolbox() != wearManager.GetMaxToolbox())
                {
                    KillObject();
                    // Decrease bunny counter
                    EventManager.TriggerEvent("KillBunny");
                    // Add a toolbox
                    EventManager.TriggerEvent("UseDeliveryZone");
                }
                else
                {
                    print("full toolbox !!");
                    // TODO : Feedback on already have max tools
                }
            }
            else
            {
                print("Delivery zone not available");
            }
            
        }
    }

    private void UseObject()
    {
        if(_holdedObject.tag == "Bunny")
        {
            AkSoundEngine.PostEvent("Play_action_lapin", gameObject);
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
        AkSoundEngine.PostEvent("Play_action_lapin", gameObject);
        Destroy(_holdedObject);

        _sr.sprite = idleSprite;

        _holdedObject = null;
    }

    private void DropObject()
    {
        AkSoundEngine.PostEvent("Play_action_lapin", gameObject);
        _holdedObject.GetComponent<Rigidbody2D>().simulated = true;
        _holdedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _holdedObject.transform.parent = null;

        _sr.sprite = idleSprite;

        _holdedObject = null;
    }

    private void HoldObject(GameObject holdedObject)
    {
        AkSoundEngine.PostEvent("Play_action_lapin", gameObject);
        holdedObject.transform.parent = transform.GetChild(0).transform;
        holdedObject.GetComponent<Rigidbody2D>().simulated = false;
        holdedObject.transform.position = transform.GetChild(0).transform.position;

        _sr.sprite = holdingSprite;

        _holdedObject = holdedObject;
    }
}
