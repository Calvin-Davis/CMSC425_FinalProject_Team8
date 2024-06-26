using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spike : MonoBehaviour
{

    private static CanvasGroup fadeScreen;
    private static AudioSource deathSound;
    private static bool dead;
    private static Controller controller;
    private float fadeTime = 1F;
    // Note: despite the name this is not just used for spike but for any player death
    //(spike or robot collsion)

    void Start()
    {
        fadeScreen = FindObjectOfType<CanvasGroup>();
        deathSound = GameObject.Find("deathSound").GetComponent<AudioSource>();
        controller = GameObject.Find("CC").GetComponent<Controller>();
        dead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player collides with something with spike script load the respawn screen while
        //passing name of the scene died on so that the player can respawn if they choose
        if (other.gameObject.tag == "self" && !dead)
        {
            dead = true; // set dead to true so that we don't try to do death actions more than once
            controller.setCanMove(false); // make sure player can't move while we fade to black
            deathSound.PlayDelayed(0);
            PlayerStats.ScenePlayerDiedOn = gameObject.scene.name;
            StartCoroutine(FadeOutCoroutine());
        }
    }

    //Used to fade out screen on death rather than having screen freeze then
    //abruptly snap to respawn screen
    private IEnumerator FadeOutCoroutine()
    {
        while (fadeScreen.alpha <= 0.99) {
            fadeScreen.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
        SceneManager.LoadScene("Respawn");
    }
}
