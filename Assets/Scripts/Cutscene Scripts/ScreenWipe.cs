using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the screen wipe plane during the Intro cutscene as well
// as the entire timeline for the cutscene's execution.
public class ScreenWipe : MonoBehaviour
{
    // The time it takes for the screen blocker to move out of the way
    public float wipeTime;
    // The time it should give for each part of the cutscene, between screen blocker open and close
    public float waitTime;
    // The time it should give between each part of the cutscene, between screen blocker close and open
    public float betweenTime;
    // Number of times it should wipe in and out
    public int iterations = 1;
    // Vector3 specifying how it should move
    public Vector3 motion;

    // Scene to load at end of cutscene
    public string sceneToLoad;

    // Receiver object: These are the robots that appear
    public GameObject receiverObject;
    private ISignalReceiver receiver;
    // Canvas receiver: This is the canvas managing the text that appears and changes
    public GameObject canvasReceiverObject;
    private ISignalReceiver canvasReceiver;

    // Used in coroutine for managing progress
    private Vector3 basePos;
    private float progress;

    // Start is called before the first frame update
    void Start()
    {
        receiver = receiverObject.GetComponent<ISignalReceiver>();
        canvasReceiver = canvasReceiverObject.GetComponent<ISignalReceiver>();
        basePos = transform.localPosition;
        StartCoroutine(DoTheThing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoTheThing()
    {

        // Repeat this process as desired
        for (int i = 0; i < iterations; i++)
        {
            // Blocker starts in view; moves out of view to reveal scene
            yield return StartCoroutine(Wipe(false));
            // Sends signal to canvas to display text
            canvasReceiver.respondToSignal(i);
            // Blocker waits for waitTime seconds before covering the scene again
            yield return new WaitForSeconds(waitTime);
            // Sends signal to canvas to stop displaying text
            canvasReceiver.respondToSignal(i);
            // Blocker moves back into view and covers the scene
            yield return StartCoroutine(Wipe(true));
            // Sends signal to robots to appear
            receiver.respondToSignal(i);
            // Waits betweenTime seconds before starting over again or moving to next scene
            yield return new WaitForSeconds(betweenTime);
        }
        // Loads first level
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator Wipe(bool inOrOut)
    {
        progress = 0.0f;

        // Note that there are not two separate coroutines for wiping in and out; both are handled here
        while (progress < wipeTime)
        {
            progress += Time.deltaTime;
            transform.localPosition = basePos + ((progress / wipeTime) * motion * (inOrOut ? -1 : 1));

            yield return null;
        }

        basePos = transform.localPosition;
    }
}
