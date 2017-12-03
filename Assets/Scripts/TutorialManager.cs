using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class TutorialManager : MonoBehaviour {

    public GameObject[] tutorialObjects;

    private bool _isTutorialFinished = false;
    private int actualIndex = 0;
    private GameObject actualSlide;

    public bool IsTutorialFinished()
    {
        return _isTutorialFinished;
    }

	private void Start ()
    {
        
        EventManager.TriggerEvent("Pause");
        actualSlide = Instantiate(tutorialObjects[actualIndex], new Vector2(145, 86), Quaternion.identity);
    }

    private void Update()
    {
        if(!_isTutorialFinished)
        {
            
            // Wait for the player to click
            if(Input.GetMouseButtonDown(0))
            {
                Destroy(actualSlide);
                actualIndex++;

                if (actualIndex == tutorialObjects.Length)
                {
                    _isTutorialFinished = true;
                }
                else
                {
                    // Show next message
                    actualSlide = Instantiate(tutorialObjects[actualIndex], new Vector2(145, 86), Quaternion.identity);
                }
            }
        }
        else
        {
            EventManager.TriggerEvent("Resume");
            Destroy(gameObject);
        }
    }
}
