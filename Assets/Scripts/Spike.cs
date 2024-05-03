using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spike : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private float fadeTime = 0.25F;
    // Note: despite the name this is not just used for spike but for any player death
    //(spike or robot collsion)
    void Start()
    {
        canvasGroup = FindObjectOfType<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if player collides with something with spike script load the respawn screen while
        //passing name of the scene died on so that the player can respawn if they choose
        if (other.gameObject.tag == "self")
        {
            PlayerStats.ScenePlayerDiedOn = this.gameObject.scene.name;
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        while (canvasGroup.alpha <= 0.99) {
            canvasGroup.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
        SceneManager.LoadScene("Respawn");
    }
}
