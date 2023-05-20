using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float runSpeed = 2.5f;
    [SerializeField] Slider staminaSlider;
    [SerializeField] float stDepleteMultiplier = 2f;
    [SerializeField] GameObject flashlight;
    [SerializeField] AudioClip flashlightClick;
    [SerializeField] float walkingSoundBrake = 0.5f;
    [SerializeField] AudioClip walking1;
    [SerializeField] AudioClip walking2;
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    AudioSource audioSource;
    SoundManager soundManager;
    bool isRunning;
    bool canRun = true;
    float stPoints = 0.8f;
    bool a = true;
    float saveWalkingSoundBrake;
    bool isDead;

    void Start(){
        myRigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        soundManager = FindAnyObjectByType<SoundManager>();
        saveWalkingSoundBrake = walkingSoundBrake;
    }

    void Update(){
        if(isDead){return;}
        LookAtMouse();
        if(myRigidbody2D.velocity != Vector2.zero && a){
            a = false;
            StartCoroutine(WalkingSound());
        }
    }

    void LookAtMouse(){
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.eulerAngles = new Vector3 (0, 0, angle);
    }

    IEnumerator WalkingSound(){
        if(isRunning && canRun){
            soundManager.PlayRunning();
        }
        else{
            soundManager.PlayWalking();
        }
        yield return new WaitForSeconds(walkingSoundBrake);
        a = true;
    }

    void FixedUpdate(){
        if(isDead){return;}
        if(staminaSlider.value < stPoints * stDepleteMultiplier){
            canRun = false;
            walkingSoundBrake = saveWalkingSoundBrake;
        }
        else{
            canRun = true;
        }

        //*********** Running state **************//
        if(isRunning && canRun){
            myRigidbody2D.velocity = moveInput * runSpeed;
            // Deplete stamina bar
            staminaSlider.value = staminaSlider.value - stPoints * stDepleteMultiplier;
            return;
        }
        
        //********** Walking state ***********//
        myRigidbody2D.velocity = moveInput * moveSpeed;
        // Refill stamina bar
        if(!isRunning){
            staminaSlider.value = staminaSlider.value + stPoints;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        // Check if touching any end tile
        if(other.CompareTag("End tiles")){
            SceneManager.LoadScene(0);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<MonsterEnemy>()){
            Debug.Log("Ouch!");
            isDead = true;
            soundManager.PlayDeath();
            StartCoroutine(Death());
        }
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    //********************** On input functions **********************//

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnRun(InputValue value){
        isRunning = true;
        walkingSoundBrake = saveWalkingSoundBrake / 2;
    }

    void OnStopRunning(InputValue value){
        isRunning = false;
        walkingSoundBrake = saveWalkingSoundBrake;
    }

    void OnLight(InputValue value){
        if(flashlight.GetComponent<Light2D>().isActiveAndEnabled){
            flashlight.GetComponent<Light2D>().enabled = false;
        }
        else{
            flashlight.GetComponent<Light2D>().enabled = true;
        }
        soundManager.PlayFlashlight();
    }

    void OnEscape(InputValue value){
        Debug.Log("Quit!");
        Application.Quit();
    }
}