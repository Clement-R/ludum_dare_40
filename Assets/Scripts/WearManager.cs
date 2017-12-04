using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pkm.EventManager;

public class WearManager : MonoBehaviour {

    struct Wall
    {
        public int health;
    }

    public ResourceManager resourceManager;
    public float scaleFactor = 1f;
    public Text textTools;
    public Image toolsProgressBar;

    public List<SpriteRenderer> wallsObjects = new List<SpriteRenderer>();
    public Sprite[] wallSprite = new Sprite[6];
    
    private Wall[] _walls = new Wall[4];
    private float _timeBeforeDecrease;


    [SerializeField]
    private int _maxToolbox = 5;
    private int _remainingToolbox;

    public int GetRemainingToolbox()
    {
        return _remainingToolbox;
    }

    private void Start()
    {
        // Create logical walls
        for (int i = 0; i < 4; i++)
        {
            _walls[i] = new Wall();
            _walls[i].health = 5;
            wallsObjects[i].sprite = wallSprite[5];
        }
        
        _remainingToolbox = 1;

        _timeBeforeDecrease = (resourceManager.maxBunnies / (float)resourceManager.GetNumberOfBunnies()) * scaleFactor;

        StartCoroutine(LoseWear());
    }

    private void Update()
    {
        toolsProgressBar.fillAmount = _remainingToolbox / (float) _maxToolbox;
        textTools.text = _remainingToolbox.ToString() + " / " + _maxToolbox.ToString();
    }

    public int GetMaxToolbox()
    {
        return _maxToolbox;
    }

    public void GainToolbox()
    {
        AkSoundEngine.PostEvent("Play_metal", Camera.main.gameObject);
        _remainingToolbox++;
        _remainingToolbox = Mathf.Clamp(_remainingToolbox, 0, _maxToolbox);
    }

    public void RepairWall(GameObject wall)
    {
        int index = 0;
        foreach (var wallObject in wallsObjects)
        {
            // Find the wall in our walls
            if (wallObject.gameObject.GetInstanceID() == wall.GetInstanceID())
            {
                if(_walls[index].health != 5 && _remainingToolbox > 0)
                {
                    // Update its health
                    _walls[index].health++;
                    _walls[index].health = Mathf.Clamp(_walls[index].health, 0, 5);

                    // Update its sprite accordingly to its health
                    wallsObjects[index].sprite = wallSprite[_walls[index].health];

                    AkSoundEngine.PostEvent("Play_reparation", wallsObjects[index].gameObject);

                    // Lose a toolbox
                    _remainingToolbox--;
                }
                break;
            }
            index++;
        }
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
            print("health : " + wall.health);

            if(wall.health > maxHealth)
            {
                maxHealth = wall.health;
                walls.Add(index);
            }
            index++;
        }

        print("Max : " + maxHealth);
        print("---------------");

        // Get random wall that will get damaged
        int wallIndex = walls[Random.Range(0, walls.Count)];
        _walls[wallIndex].health--;
        AkSoundEngine.PostEvent("Play_usure", gameObject);

        if(_walls[wallIndex].health == 1)
        {
            AkSoundEngine.PostEvent("Play_usurecritique", gameObject);
        }

        if(_walls[wallIndex].health == 0)
        {
            EventManager.TriggerEvent("LoseWear");
        }

        // Change its sprite according to its health
        wallsObjects[wallIndex].sprite = wallSprite[_walls[wallIndex].health];

        _timeBeforeDecrease = (resourceManager.maxBunnies / (float)resourceManager.GetNumberOfBunnies()) * scaleFactor;
        StartCoroutine(LoseWear());
    }
}
