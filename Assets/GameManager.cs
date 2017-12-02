using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.StartListening("LoseBunny", Lose);
        EventManager.StartListening("LoseWear", Lose);
    }

    private void Lose()
    {
        Debug.Log("You lose.");
        Time.timeScale = 0f;
    }
}
