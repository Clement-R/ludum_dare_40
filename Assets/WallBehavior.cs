using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class WallBehavior : MonoBehaviour
{
    private bool isPlayerInZone = false;

    private void Start()
    {
        EventManager.StartListening("UseRepairZone", Repair);
    }

    private void Repair()
    {
        WearManager.RepairWall(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInZone = false;
        }
    }
}
