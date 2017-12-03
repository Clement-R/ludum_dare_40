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
    
    public float scaleFactor = 1f;
    public Text textTools;
    public Image toolsProgressBar;

    public List<SpriteRenderer> wallsObjects = new List<SpriteRenderer>();
    public Sprite[] wallSprite = new Sprite[6];

    static private List<SpriteRenderer> _wallsObjects = new List<SpriteRenderer>();
    private static Sprite[] wallSpriteStatic = new Sprite[6];
    private static Wall[] _walls = new Wall[4];
    private float _timeBeforeDecrease;


    [SerializeField]
    private int _maxToolbox = 5;
    static private int _maxToolboxStatic = 5;
    static private int _remainingToolbox;
    

    private void Start()
    {
        _maxToolboxStatic = _maxToolbox;

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

        // _remainingToolbox = _maxToolboxStatic;
        _remainingToolbox = 1;

        _timeBeforeDecrease = (ResourceManager.maxBunniesStatic / (float) ResourceManager.GetNumberOfBunnies()) * scaleFactor;

        wallSpriteStatic = wallSprite;

        StartCoroutine(LoseWear());
    }

    private void Update()
    {
        toolsProgressBar.fillAmount = _remainingToolbox / (float) _maxToolboxStatic;
        textTools.text = _remainingToolbox.ToString() + " / " + _maxToolboxStatic.ToString();
    }

    static public int GetRemainingToolbox()
    {
        return _remainingToolbox;
    }

    static public int GetMaxToolbox()
    {
        return _maxToolboxStatic;
    }

    public static void GainToolbox()
    {
        AkSoundEngine.PostEvent("Play_metal", Camera.main.gameObject);
        _remainingToolbox++;
        _remainingToolbox = Mathf.Clamp(_remainingToolbox, 0, _maxToolboxStatic);
    }

    public static void RepairWall(GameObject wall)
    {
        int index = 0;
        foreach (var wallObject in _wallsObjects)
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
                    _wallsObjects[index].sprite = wallSpriteStatic[_walls[index].health];

                    AkSoundEngine.PostEvent("Play_reparation", _wallsObjects[index].gameObject);

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
        _wallsObjects[wallIndex].sprite = wallSprite[_walls[wallIndex].health];

        _timeBeforeDecrease = (ResourceManager.maxBunniesStatic / (float)ResourceManager.GetNumberOfBunnies()) * scaleFactor;
        StartCoroutine(LoseWear());
    }
}
