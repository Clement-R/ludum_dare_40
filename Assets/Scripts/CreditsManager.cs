using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour {

	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            TransitionManager.toggleTransiton("main_menu");
        }
	}
}
