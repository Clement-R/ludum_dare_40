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
    }

    private void Lose()
    {
        Time.timeScale = 0f;
    }

    private void LoseByBunny()
    {
        Debug.Log("You lose by too much bunnies.");
        Lose();
    }

    private void LoseByWear()
    {
        Debug.Log("You lose by wear.");
        Lose();
    }

    private void LoseByCop()
    {
        Debug.Log("You lose by cop.");
        Lose();
    }
}
