using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialMessages : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public string[] strings;
    private int idx = 0;

    // Start is called before the first frame update
    void Start()
    {
        tmp.SetText(strings[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void progress()
    {
        tmp.SetText(strings[++idx]);
    }
}
