using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public Renderer rend;
    public float startingAlpha;
    public float fadeTime;
    private float currentLerpTime = 0.0f;
    private bool isFading = false;

    public float waitTime;

    void Start()
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("FadeIn script requires a Renderer component on the same GameObject!");
                return;
            }
        }

        Color startingColor = rend.material.GetColor("_TintColor");
        startingColor.a = startingAlpha;
        rend.material.color = startingColor;

        StartCoroutine(DoTheThing());

    }

    IEnumerator DoTheThing()
    {
        Debug.Log("Starting Fade out!");
        yield return StartCoroutine(FadeObject(false));
        Debug.Log("Waiting!");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Starting Fade in!");
        yield return StartCoroutine(FadeObject(true));
        Debug.Log("Done!");
    }

    public void FadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(FadeObject(true));
        }
    }

    public void FadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(FadeObject(false));
        }
    }

    IEnumerator FadeObject(bool inOrOut)
    {
        currentLerpTime = 0.0f;
        isFading = true;
        Color startingColor = rend.material.color;
        startingColor.a = inOrOut ? 0.0f : 1.0f;
        rend.material.color = startingColor;

        while (currentLerpTime < fadeTime)
        {
            currentLerpTime += Time.deltaTime;
            float progress = inOrOut ? (currentLerpTime / fadeTime) : (1 - (currentLerpTime / fadeTime));

            Color newColor = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, progress);
            rend.material.color = newColor;

            yield return null;
        }
        isFading = false;
    }
}
