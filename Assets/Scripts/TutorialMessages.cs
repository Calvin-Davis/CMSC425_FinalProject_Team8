using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialMessages : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public string[] strings;
    public AudioSource talkSound;
    private int idx = 0;

    // Start is called before the first frame update
    void Start()
    {
        tmp.SetText(strings[0]);
        talkSound.pitch = Random.Range(0.9f, 1.2f);
        talkSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Displays tutorial text as player moves through tutorial
    public void progress()
    {
        if (talkSound.isPlaying)
        {
            talkSound.Stop();
        }
        talkSound.pitch = Random.Range(0.9f, 1.2f);
        talkSound.Play();
        tmp.SetText(strings[++idx]);
    }
}
