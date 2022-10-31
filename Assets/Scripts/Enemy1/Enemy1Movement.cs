using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Movement : MonoBehaviour
{
    private GameObject player;
    public float speed = 5f;
    void Start()
    {
        player = GameObject.Find("Hero");
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
        player.transform.position, speed * Time.deltaTime);
        //transform.position = transform.position.MoveTowards(transform.position, player.transform.position, 5);
    }
    void OnCollisionEnter2D(Collision2D col){
        // Different enemies will deal different damage
        if(col.gameObject.name == "Hero"){
            PlayerMovement.playerHealth -= 0.5f;
        }
    }

}
