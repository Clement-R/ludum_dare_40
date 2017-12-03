using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwaper : MonoBehaviour {

    public GameObject[] backgrounds;
    public float[] backgroundSpeedByMotorLevel;
    
    private GameObject[] _background;

    public GameObject[] GetBackgrounds()
    {
        return _background;
    }

    public void ChangeBackgroundSpeed(int speed)
    {
        foreach (var background in backgrounds)
        {
            background.GetComponent<ScrollingObject>().UpdateSpeed(backgroundSpeedByMotorLevel[speed]);
        }
    }
}
