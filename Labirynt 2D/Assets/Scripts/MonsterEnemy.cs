using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float detectionDistance = 3f;
    PlayerMovement playerMovement;
    Vector3 playerPosition;
    GameObject player;
    Rigidbody2D myRigidbody;
    Vector2 velocity;
    bool canMove;
    RaycastHit2D hit;
    CircleCollider2D myCollider;

    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.enabled = false;
    }

    void Update()
    {
        playerPosition = player.GetComponent<Transform>().position;
    }

    void FixedUpdate(){
        if(!canMove){
            myRigidbody.velocity = Vector2.zero;
            CheckDistance();
        }
        else{
            Move();
        }
    }

    void Move(){
        velocity = (playerPosition - transform.position).normalized;
        myRigidbody.velocity = velocity * moveSpeed;
    }

    void CheckDistance()
    {
        if(Vector3.Distance(playerPosition, transform.position) <= detectionDistance)
        {
            Vector2 direction = playerPosition - transform.position;

            hit = Physics2D.Raycast(transform.position, direction, 5f);

            Debug.Log(hit.transform.name);

            if(hit.transform.name == "Player"){
                myCollider.enabled = true;
                canMove = true;
            }
        }
    }
}
