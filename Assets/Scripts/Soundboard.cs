using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundboard : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static Soundboard main;

    /// <summary>
    /// AudioClips for all available sounds in the game 
    /// </summary>
    public AudioClip soundLaser;
    public AudioClip soundShoot;
    public AudioClip soundDamage;
    public AudioClip soundSentryDeath;

    /// <summary>
    /// Property for the AudioSource used to play AudioClips
    /// </summary>
    public AudioSource player;

    void Start()
    {
        if (main == null)
        {
            main = this;
            player = GetComponent<AudioSource>(); // Gets a reference to the AudioSource
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static void PlayLaser() // plays laser audio in 2D space
    {
        main.player.PlayOneShot(main.soundLaser);
    }
    public static void PlayShoot() // plays shoot audio in 2D space
    {
        main.player.PlayOneShot(main.soundShoot);
    }
    public static void PlayDamage() // plays damage audio in 2D space
    {
        main.player.PlayOneShot(main.soundDamage);
    }
    public static void PlaySentryDeath() // plays death audio in 2D space
    {
        main.player.PlayOneShot(main.soundSentryDeath);
    }
}
