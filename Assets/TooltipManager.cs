using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class TooltipManager : MonoBehaviour {

    public GameObject soldBunnyFeedback;
    public Transform soldTransform;

    public GameObject soldFullToolboxFeedback;
    public GameObject canRepairWallIcon;

    private void Start ()
    {
        canRepairWallIcon.SetActive(false);

        EventManager.StartListening("BunnySold", PlayBunnySold);
        EventManager.StartListening("MaxTools", PlayMaxTools);
    }

    public void ToggleRepairIcon(bool active)
    {
        canRepairWallIcon.SetActive(active);
    }

    public void PlayBunnySold()
    {
        Instantiate(soldBunnyFeedback, soldTransform.position, Quaternion.identity);
    }

    public void PlayMaxTools()
    {
        Instantiate(soldFullToolboxFeedback, soldTransform.position, Quaternion.identity);
    }
}
