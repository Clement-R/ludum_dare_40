using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    public static GameObject transitionScreen;
    public static TransitionManager instance;

    public AnimationCurve ac;
    static public float timeToMove = 0.75f;

    static private bool _side = true;
    
    static private int startPos1 = -1078;
    static private int endPos = 168;
    static private int startPos2 = 1511;
    
    private void Awake()
    {
        instance = this;
        
        transitionScreen = GameObject.FindGameObjectWithTag("transition_screen");
        DontDestroyOnLoad(this.gameObject);
    }

    public static void toggleTransiton(string sceneName)
    {
        instance.StartCoroutine(instance.MoveTransition(sceneName));
    }

    public IEnumerator MoveTransition(string sceneName)
    {
        // AkSoundEngine.PostEvent("transition_menu", gameObject);
        float t = 0f;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime/ timeToMove;
            transitionScreen.transform.localPosition = Vector3.Lerp(new Vector2(startPos1, 0), new Vector2(endPos, 0), ac.Evaluate(t / timeToMove));
            yield return null;
        }

        yield return SceneManager.LoadSceneAsync(sceneName);
        
        t = 0f;
        while (t < 1) {
            t += Time.unscaledDeltaTime / timeToMove;
            transitionScreen.transform.localPosition = Vector3.Lerp(new Vector2(endPos, 0), new Vector2(startPos2, 0), ac.Evaluate(t / timeToMove));
            yield return null;
        }
    }
}
