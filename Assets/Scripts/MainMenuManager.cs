using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    private void Start()
    {
        AkSoundEngine.PostEvent("Play_menu_music", gameObject);
    }

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
