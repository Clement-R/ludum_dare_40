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
    public List<SpriteRenderer> wallsObjects = new List<SpriteRenderer>();

    static private List<SpriteRenderer> _wallsObjects = new List<SpriteRenderer>();
    public Sprite[] wallSprite = new Sprite[6];

    static private Wall[] _walls = new Wall[4];
    private float _timeBeforeDecrease;

    private void Start()
    {
        foreach (var wall in wallsObjects)
        {
            _wallsObjects.Add(wall.GetComponent<SpriteRenderer>());
        }

        // Create logical walls
        for (int i = 0; i < 4; i++)
        {
            _walls[i] = new Wall();
            _walls[i].health = 5;
            _wallsObjects[i].sprite = wallSprite[5];
        }

        _timeBeforeDecrease = (ResourceManager.maxBunnies / (float) ResourceManager.GetNumberOfBunnies()) * scaleFactor;
        
        StartCoroutine(LoseWear());
    }

    public static void RepairWall(GameObject wall)
    {
        int index = 0;
        foreach (var wallObject in _wallsObjects)
        {
            // Find the wall in our walls
            if (wallObject.GetInstanceID() == wall.GetInstanceID())
            {
                _walls[index].health++;
                _walls[index].health = Mathf.Clamp(_walls[index].health, 0, 6);

                // TODO : Lose toolbox
                break;
            }
            index++;
        }
    }

    IEnumerator LoseWear()
    {
        print(_timeBeforeDecrease);
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
        _walls[wallIndex].health--;

        if(_walls[wallIndex].health == 0)
        {
            EventManager.TriggerEvent("LoseWear");
        }

        // Change its sprite according to its health
        _wallsObjects[wallIndex].sprite = wallSprite[_walls[wallIndex].health];

        _timeBeforeDecrease = (ResourceManager.maxBunnies / (float)ResourceManager.GetNumberOfBunnies()) * scaleFactor;
        StartCoroutine(LoseWear());
    }
}
