using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour {

    public void Quit()
    {
        TransitionManager.toggleTransiton("main_menu");
    }

    public void Retry()
    {
        TransitionManager.toggleTransiton("game");
    }
}
