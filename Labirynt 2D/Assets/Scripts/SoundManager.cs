using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header ("PLAYER:")]
    [Header ("Flashlight")]
    [SerializeField] AudioClip flashlight1;
    [Header ("Walking")]
    [SerializeField] AudioClip walking1;
    [SerializeField] AudioClip walking2;
    [Header ("Running")]
    [SerializeField] AudioClip running1;
    [SerializeField] AudioClip running2;
    [Header ("Death")]
    [SerializeField] AudioClip death1;
    AudioSource audioSource;
    bool a = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalking(){
        // Set random clip out of available
        if(a == true){
            a = false;
            audioSource.clip = walking1;
        }
        else{
            a = true;
            audioSource.clip = walking2;
        }
        // Play clip
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    public void PlayRunning(){
        // Set random clip out of available
        if(a == true){
            a = false;
            audioSource.clip = running1;
        }
        else{
            a = true;
            audioSource.clip = running2;
        }
        // Play clip
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    public void PlayFlashlight(){
        // Play clip
        audioSource.clip = flashlight1;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void PlayDeath(){
        // Play clip
        audioSource.clip = death1;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }
}