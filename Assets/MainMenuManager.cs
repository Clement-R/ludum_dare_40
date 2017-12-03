using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

	public void GoToScene(string sceneName)
    {
        if(sceneName == "game")
        {
            AkSoundEngine.PostEvent("Stop_music_menu", gameObject);
        }

        TransitionManager.toggleTransiton(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
