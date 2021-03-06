using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAlpha : MonoBehaviour
{
    [HideInInspector] public float fadeTime;
    [HideInInspector] public float disableDelay;
    [HideInInspector] public Color materialColor;

	private Renderer render;
	private bool isFading;
    private bool fadingIn;

	void Start ()
	{
		render = gameObject.GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        render.material.color = new Color(materialColor.r, materialColor.g, materialColor.b, 0);
    }

    public void BeginFadeIn()
    {
        isFading = true;
        fadingIn = true;
    }

    public void BeginFadeOut()
	{
		isFading = true;
        fadingIn = false;
        Invoke("DisableSelf", fadeTime + disableDelay);
    }

    void Fade()
	{
		if (isFading)
		{
            if (fadingIn) //Fade in from clear to color
            {
                render.material.color = Color.Lerp(render.material.color, materialColor, Time.fixedDeltaTime / fadeTime);
            }

            else if (!fadingIn) //Fade out from color to clear

            {
                render.material.color = Color.Lerp(render.material.color,
                new Color(materialColor.r, materialColor.g, materialColor.b, 0),
                Time.fixedDeltaTime / fadeTime);
            }
        }
    }

	void FixedUpdate()
	{
		Fade();
	}

    void DisableSelf()
    {
        gameObject.SetActive(false);
    }

}
