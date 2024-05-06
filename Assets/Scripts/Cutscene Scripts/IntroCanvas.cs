using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Script used to manage the text pop-ups in the Intro cutscene.
public class IntroCanvas : MonoBehaviour, ISignalReceiver
{
    public TextMeshProUGUI tmp;
    public GameObject panel;
    public AudioSource talkSound;
    private int calls = 0;

    // Start is called before the first frame upd
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respondToSignal(int i)
    {
        switch (calls)
        {
            case 0:
                panel.SetActive(true);
                if (talkSound.isPlaying)
                {
                    talkSound.Stop();
                }
                talkSound.Play();
                tmp.SetText("It's been 424 days since our ship was last invaded by the Roguebots! ");
                break;
            case 2:
                panel.SetActive(true);
                if (talkSound.isPlaying)
                {
                    talkSound.Stop();
                }
                talkSound.Play();
                tmp.SetText("Make that 0 days... To the escape pod!");
                break;
            default:
                panel.SetActive(false);
                tmp.SetText("");
                break;
        }

        calls++;
    }

}
