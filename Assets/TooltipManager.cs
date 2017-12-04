using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour {

    public GameObject soldBunnyFeedback;
    public GameObject soldFullToolboxFeedback;
    public GameObject canRepairWallIcon;

    private void Start ()
    {
        // TODO : When a bunny is sold pop a toolbox floating icon (valide_mamazon)
        // TODO : If the player try to buy a toolbox and he's full pop an icon (full)
        canRepairWallIcon.SetActive(false);
    }

    public void ToggleRepairIcon(bool active)
    {
        canRepairWallIcon.SetActive(active);
    }
}
