using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.StartListening("LoseBunny", LoseByBunny);
        EventManager.StartListening("LoseWear", LoseByWear);
        EventManager.StartListening("LoseCop", LoseByCop);
        EventManager.StartListening("LoseBunnyNone", LoseByBunnyNone);
    }

    private void Lose()
    {
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
