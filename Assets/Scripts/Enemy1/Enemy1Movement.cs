using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Movement : MonoBehaviour
{
    //[SerializedField] private GameObject player;
    private GameObject player;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            player.transform.position, speed * Time.deltaTime);
        //transform.position = transform.position.MoveTowards(transform.position, player.transform.position, 5);
    }
}
