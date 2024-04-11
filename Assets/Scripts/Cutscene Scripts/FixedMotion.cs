using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixedMotion : MonoBehaviour
{

    public Vector3 motion;
    public float motionTime;
    public bool vanishAtEnd = true;
    public string sceneChange;

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
