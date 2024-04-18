using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenWipe : MonoBehaviour
{

    public float wipeTime;
    public float waitTime;
    public float betweenTime;
    public int iterations = 1;
    public Vector3 motion;

    public string sceneToLoad;

    public GameObject receiverObject;
    private ISignalReceiver receiver;
    public GameObject canvasReceiverObject;
    private ISignalReceiver canvasReceiver;

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
        for (int i = 0; i < iterations; i++)
        {
            yield return StartCoroutine(Wipe(false));
            canvasReceiver.respondToSignal(i);
            yield return new WaitForSeconds(waitTime);
            canvasReceiver.respondToSignal(i);
            yield return StartCoroutine(Wipe(true));
            receiver.respondToSignal(i);
            yield return new WaitForSeconds(betweenTime);
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator Wipe(bool inOrOut)
    {
        progress = 0.0f;

        while (progress < wipeTime)
        {
            progress += Time.deltaTime;
            transform.localPosition = basePos + ((progress / wipeTime) * motion * (inOrOut ? -1 : 1));

            yield return null;
        }

        basePos = transform.localPosition;
    }
}
