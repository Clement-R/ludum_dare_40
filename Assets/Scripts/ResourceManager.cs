using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class ResourceManager : MonoBehaviour {
    static public int maxBunnies = 100;

    public GameObject bunnyPrefab;
    public GameObject bunnyTransform;

    [SerializeField]
    private int _startingNumberOfBunnies = 2;
    static private int _bunnyCounter = 0;
    
    private float _nextReproduction = 0.0f;
    [SerializeField]
    private float _reproductionCooldown = 2f; // Change this value with a formula
    
    private void Start()
    {
        // Start listening to events
        EventManager.StartListening("KillBunny", KillBunny);

        // Create first bunnies
        for (int i = 0; i < _startingNumberOfBunnies; i++)
        {
            BunnyReproduction();
        }
    }

    private void Update ()
    {
        if (_bunnyCounter >= 2)
        {
            if(Time.time >= _nextReproduction)
            {
                BunnyReproduction();
                _nextReproduction = Time.time + _reproductionCooldown;
            }
        }

        if(_bunnyCounter >= maxBunnies)
        {
            EventManager.TriggerEvent("LoseBunny");
        }
	}

    static public int GetNumberOfBunnies()
    {
        return _bunnyCounter;
    }

    private void BunnyReproduction()
    {
        Vector2 pos = new Vector2(bunnyTransform.transform.position.x + Random.Range(-10f, 11f), bunnyTransform.transform.position.y);
        Instantiate(bunnyPrefab, pos, Quaternion.identity, bunnyTransform.transform);
        _bunnyCounter++;
    }

    private void KillBunny()
    {
        _bunnyCounter--;
    }
}
