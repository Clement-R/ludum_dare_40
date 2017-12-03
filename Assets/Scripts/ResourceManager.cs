using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pkm.EventManager;

public class ResourceManager : MonoBehaviour {
    public int maxBunnies = 100;

    public Text textBunnies;
    public Image bunniesProgressBar;

    public float scaleFactor = 1f;
    public GameObject bunnyPrefab;
    public GameObject bunnyTransform;

    [SerializeField]
    private int _startingNumberOfBunnies = 2;
    private int _bunnyCounter = 0;
    
    private float _nextReproduction = 0.0f;
    private float _reproductionCooldown; // Change this value with a formula
    
    public int GetBunnyCounter()
    {
        return _bunnyCounter;
    }

    private void Start()
    {
        // Start listening to events
        EventManager.StartListening("KillBunny", KillBunny);

        // Create first bunnies
        for (int i = 0; i < _startingNumberOfBunnies; i++)
        {
            BunnyReproduction();
        }

        _reproductionCooldown = (maxBunnies / _bunnyCounter) * scaleFactor;
    }

    private void Update ()
    {
        if (!GameManager.IsGamePaused())
        {
            if (_bunnyCounter >= 2)
            {
                if (Time.time >= _nextReproduction)
                {
                    BunnyReproduction();
                    _reproductionCooldown = (maxBunnies / _bunnyCounter) * scaleFactor;
                    _nextReproduction = Time.time + _reproductionCooldown;
                }
            }

            if (_bunnyCounter >= maxBunnies && Time.timeScale > 0)
            {
                EventManager.TriggerEvent("LoseBunny");
            }

            if (_bunnyCounter == 0 && Time.timeScale > 0)
            {
                EventManager.TriggerEvent("LoseBunnyNone");
            }

            print(_bunnyCounter / maxBunnies);
            bunniesProgressBar.fillAmount = _bunnyCounter / (float)maxBunnies;
            textBunnies.text = _bunnyCounter.ToString() + " / " + maxBunnies.ToString();
        }
    }

    public int GetNumberOfBunnies()
    {
        return _bunnyCounter;
    }

    private void BunnyReproduction()
    {
        AkSoundEngine.PostEvent("Play_reproduction", gameObject);
        Vector2 pos = new Vector2(bunnyTransform.transform.position.x + Random.Range(-10f, 11f), bunnyTransform.transform.position.y);
        Instantiate(bunnyPrefab, pos, Quaternion.identity, bunnyTransform.transform);
        _bunnyCounter++;
    }

    private void KillBunny()
    {
        _bunnyCounter--;
    }
}
