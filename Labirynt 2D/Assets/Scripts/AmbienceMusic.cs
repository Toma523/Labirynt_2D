using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceMusic : MonoBehaviour
{
    [SerializeField] AudioClip ambience1;
    [SerializeField] AudioClip ambience2;
    [SerializeField] AudioClip ambience3;
    [SerializeField] AudioClip ambience4;
    AudioSource audioSource;
    int a = 1;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ambience1;
        audioSource.volume = 0.2f;
        PlayAmbience();
    }

    void Update() {
        if(!audioSource.isPlaying){
            if(a == 1){
                audioSource.clip = ambience1;
                PlayAmbience();
                return;
            }
            if(a == 2){
                audioSource.clip = ambience2;
                PlayAmbience();
                return;
            }
            if(a == 3){
                audioSource.clip = ambience3;
                PlayAmbience();
                return;
            }
            if(a == 4){
                audioSource.clip = ambience4;
                PlayAmbience();
            }
        }
    }

    public void PlayAmbience(){
        if(a < 4){
            a++;
        }
        else{
            a = 1;
        }
        audioSource.Play();
    }
}
