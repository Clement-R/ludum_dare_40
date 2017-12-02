using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderBehavior : MonoBehaviour {

    [SerializeField]
    private float _grabRadius = 150f;

    private GameObject _holdedObject = null;
    
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
            if (_holdedObject != null)
            {
                DropObject();
            }
            else
            {
                if (nearestBunny != null)
                {
                    HoldObject(nearestBunny);
                }
            }
        }
    }

    private void DropObject()
    {
        _holdedObject.GetComponent<Rigidbody2D>().simulated = true;
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
