using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwaper : MonoBehaviour {

    public GameObject[] backgrounds;
    public float[] backgroundSpeedByMotorLevel;

    static private float[] backgroundSpeedByMotorLevelStatic;
    private static GameObject[] backgroundStatic;
    
	private void Start ()
    {
        backgroundSpeedByMotorLevelStatic = backgroundSpeedByMotorLevel;
        backgroundStatic = backgrounds;
    }

    public static void ChangeBackgroundSpeed(int speed)
    {
        foreach (var background in backgroundStatic)
        {
            background.GetComponent<ScrollingObject>().scrollSpeed = backgroundSpeedByMotorLevelStatic[speed];
        }
    }
}
