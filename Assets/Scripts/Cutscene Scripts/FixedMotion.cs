using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script serves as a general script for making an object move in a 
// straight line in a certain direction for a certain amount of time when
// a scene is loaded, useful for cutscenes.

// The only use currently is the spaceship in the ending cutscene.
public class FixedMotion : MonoBehaviour
{
    // Vector3 representing the vector along which to move
    public Vector3 motion;
    // float representing how long it should take to move along that vector
    public float motionTime;
    // whether it should disappear at the end of the motion
    public bool vanishAtEnd = true;
    // The scene to flip to at the end of the motion, if desired
    public string sceneChange;

    // tracker for coroutine
    private float progress;

    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        progress = 0.0f;

        while (progress < motionTime)
        {
            progress += Time.deltaTime;
            transform.localPosition += (motion * Time.deltaTime / motionTime);

            yield return null;
        }

        if (vanishAtEnd)
        {
            this.gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(sceneChange))
        {
            SceneManager.LoadScene(sceneChange);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
