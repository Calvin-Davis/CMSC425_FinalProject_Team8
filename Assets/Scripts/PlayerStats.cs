using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    //used to pass informateion about player between scenes
    //allows for correct location of respawn and sending player to correct level
    //after level completion
    public static string ScenePlayerDiedOn;
    public static string PlayerNextLevel;
}
