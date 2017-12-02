using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class ResourceManager : MonoBehaviour {

    public GameObject bunnyPrefab;
    public GameObject bunnyTransform;

    [SerializeField]
    private int _startingNumberOfBunnies = 2;
    private List<GameObject> _bunnies = new List<GameObject>();
    
    private float _nextReproduction = 0.0f;
    [SerializeField]
    private float _reproductionCooldown = 2f; // Change this value with a formula
    [SerializeField]
    private int _maxBunnies = 100;

    private void Start()
    {
        for (int i = 0; i < _startingNumberOfBunnies; i++)
        {
            BunnyReproduction();
        }
    }

    void Update ()
    {
        if (_bunnies.Count >= 2)
        {
            if(Time.time >= _nextReproduction)
            {
                BunnyReproduction();
                _nextReproduction = Time.time + _reproductionCooldown;
            }
        }

        if(_bunnies.Count >= _maxBunnies)
        {
            EventManager.TriggerEvent("LoseBunny");
        }
	}

    void BunnyReproduction()
    {
        Vector2 pos = new Vector2(bunnyTransform.transform.position.x + Random.Range(-10f, 11f), bunnyTransform.transform.position.y);
        _bunnies.Add(Instantiate(bunnyPrefab, pos, Quaternion.identity, bunnyTransform.transform));
    }
}
