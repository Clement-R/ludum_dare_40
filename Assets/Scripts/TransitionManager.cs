using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    public static GameObject transitionScreen;
    public static TransitionManager instance;

    public Vector2 startPos1;
    public Vector2 endPos;
    public Vector2 startPos2;

    public float timeToMove = 0.75f;
    public AnimationCurve ac;
    static public float timeToMoveStatic = 0f;

    static private bool _side = true;
    
    static private Vector2 startPos1Static;
    static private Vector2 endPosStatic = Vector2.zero;
    static private Vector2 startPos2Static;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        transitionScreen = GameObject.FindGameObjectWithTag("transition_screen");
        DontDestroyOnLoad(this.gameObject);
        
        startPos1Static = startPos1;
        endPosStatic = endPos;
        startPos2Static = startPos2;
        timeToMoveStatic = timeToMove;
    }

    public static void toggleTransiton(string sceneName)
    {
        instance.StartCoroutine(instance.MoveTransition(sceneName));
    }

    public IEnumerator MoveTransition(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        ao.allowSceneActivation = false;

        // AkSoundEngine.PostEvent("transition_menu", gameObject);

        float t = 0f;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / timeToMoveStatic;
            transitionScreen.transform.position = Vector2.Lerp(startPos1Static, endPosStatic, ac.Evaluate(t / timeToMoveStatic));
            yield return null;
        }

        
        ao.allowSceneActivation = true;

        t = 0f;
        while (t < 1) {
            t += Time.unscaledDeltaTime / timeToMoveStatic;
            transitionScreen.transform.position = Vector2.Lerp(endPosStatic, startPos2Static, ac.Evaluate(t / timeToMoveStatic));
            yield return null;
        }
    }
}
