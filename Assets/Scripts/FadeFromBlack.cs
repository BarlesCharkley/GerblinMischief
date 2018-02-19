using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeFromBlack : MonoBehaviour
{
	[SerializeField] private Texture2D fadeTexture;
    [SerializeField] private float fadeTime = 1f;
	//public float fadeSpeed = 0.1f;

	private int drawDepth = -1000;
	private float alpha = 1f;
	private int fadeDir = -1;

    //void OnEnable()
    //{
    //    //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
    //    SceneManager.sceneLoaded += OnLevelFinishedLoading;
    //}

    //void OnDisable()
    //{
    //    //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
    //    SceneManager.sceneLoaded -= OnLevelFinishedLoading.
    //}

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);

        alpha = 1;
        BeginFade(-1);
    }

    void OnGUI ()
	{
        //alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha += fadeDir * Time.deltaTime / fadeTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect(0,0, Screen.width, Screen.height), fadeTexture);
	}

	public float BeginFade (int direction)
	{
		fadeDir = direction;
        return (fadeTime);
		//return (fadeSpeed);
	}

	//void OnLevelWasLoaded()
	//{
	//	alpha = 1;
	//	BeginFade (-1);
	//}


}