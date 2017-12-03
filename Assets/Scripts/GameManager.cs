using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class GameManager : MonoBehaviour {
    public TutorialManager tutorialManager;
    public GameObject gameOverMenu;

    private static bool _pause = false;

    public static bool IsGamePaused()
    {
        return _pause;
    }

    private void Awake()
    {
        if(tutorialManager.IsTutorialFinished())
        {
            Time.timeScale = 1f;
        }

        _pause = false;
    }

    private void Start () {
        EventManager.StartListening("Pause", Pause);
        EventManager.StartListening("Resume", Resume);

        EventManager.StartListening("LoseBunny", LoseByBunny);
        EventManager.StartListening("LoseWear", LoseByWear);
        EventManager.StartListening("LoseCop", LoseByCop);
        EventManager.StartListening("LoseBunnyNone", LoseByBunnyNone);

        AkSoundEngine.SetState("pause", "None");
        AkSoundEngine.PostEvent("Play_game_music", gameObject);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        _pause = true;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        _pause = false;
    }

    private void Lose()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void LoseByBunny()
    {
        AkSoundEngine.PostEvent("Play_defaite_lapin", gameObject);
        Debug.Log("You lose by too much bunnies.");
        Lose();
    }

    private void LoseByBunnyNone()
    {
        // AkSoundEngine.PostEvent("Play_defaite_lapin", gameObject);
        Debug.Log("You lose by no more bunnies.");
        Lose();
    }

    private void LoseByWear()
    {
        AkSoundEngine.PostEvent("Play_defaite_lapin", gameObject);
        Debug.Log("You lose by wear.");
        Lose();
    }

    private void LoseByCop()
    {
        AkSoundEngine.PostEvent("Play_defaite_police", gameObject);
        Debug.Log("You lose by cop.");
        Lose();
    }
}
