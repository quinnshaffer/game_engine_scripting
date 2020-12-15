using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioSource src;
    static bool hasPlayed;
    void Start()
    {
        src = this.GetComponent<AudioSource>();
        hasPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {


        hasPlayed = false;
    }
    public static void playSound(string snd)
    {
        if (!hasPlayed)
        {
            src.clip = (AudioClip)Resources.Load(snd);
            src.Play();
            hasPlayed = true;
        }
    }
    public static void playImportantSound(string snd)
    {
            src.clip = (AudioClip)Resources.Load(snd);
            src.Play();
        hasPlayed = true;
    }
    public static void playGhostKill(){
        playSound("blip");
    }
    public static void playPlayerDead()
    {
        playSound("playerDead");
    }
    public static void playGhostDown()
    {
        playImportantSound("ghostDown");
    }
}
