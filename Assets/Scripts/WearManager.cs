using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pkm.EventManager;

public class WearManager : MonoBehaviour {

    struct Wall
    {
        public int health;
    }

    public float scaleFactor = 1f;
    public SpriteRenderer[] walls = new SpriteRenderer[4];
    public Sprite[] wallSprite = new Sprite[6];

    private Wall[] _walls = new Wall[4];
    private float _timeBeforeDecrease;

    private void Start()
    {
        EventManager.StartListening("UseRepairZone", RepairShip);

        // Create logical walls
        for (int i = 0; i < 4; i++)
        {
            _walls[i] = new Wall();
            _walls[i].health = 6;
        }

        _timeBeforeDecrease = (ResourceManager.maxBunnies / (float) ResourceManager.GetNumberOfBunnies()) * scaleFactor;
        
        StartCoroutine(LoseWear());
    }

    IEnumerator LoseWear()
    {
        yield return new WaitForSeconds(_timeBeforeDecrease);

        // Find wall with the biggest health
        int maxHealth = 0;
        List<int> walls = new List<int>();
        int index = 0;
        foreach (var wall in _walls)
        {
            if(wall.health >= maxHealth)
            {
                maxHealth = wall.health;
                walls.Add(index);
            }
            index++;
        }

        // Get random wall that will get damaged
        int wallIndex = walls[Random.Range(0, walls.Count)];
        // Change its sprite according to its health
        this.walls[wallIndex].sprite = wallSprite[_walls[wallIndex].health];

        _timeBeforeDecrease = (ResourceManager.maxBunnies / (float)ResourceManager.GetNumberOfBunnies()) * scaleFactor;
        StartCoroutine(LoseWear());
    }

    private void RepairShip()
    {
        //_wear -= repairValue;
        //_wear = Mathf.Clamp(_wear, 0f, _maxWear);
    }
}
