using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

	public void GoToScene(string sceneName)
    {
        TransitionManager.toggleTransiton(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
