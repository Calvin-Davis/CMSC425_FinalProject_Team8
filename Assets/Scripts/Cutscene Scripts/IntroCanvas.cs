using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroCanvas : MonoBehaviour, ISignalReceiver
{
    public TextMeshProUGUI tmp;
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
                tmp.SetText("It's been 424 days since our ship was last invaded by the Roguebots! ");
                break;
            case 2:
                tmp.SetText("Make that 0 days... To the escape pod!");
                break;
            default:
                tmp.SetText("");
                break;
        }

        calls++;
    }

}
