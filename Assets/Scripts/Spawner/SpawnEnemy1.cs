using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy1 : MonoBehaviour
{
    private GameObject player;
    public float SpawnTime = 1f;
    private float timer = 0f;
    public GameObject Enemy1Prefab;

    void Start(){
        player = GameObject.Find("Hero");
    }

    void Update(){
         if(timer <= SpawnTime){
            timer += Time.deltaTime;
         }else{
            timer = 0;
            //Spawn enemy
            GameObject p = GameObject.Instantiate(Enemy1Prefab) as GameObject;
            Vector2 playerPos = player.transform.position;
            float[] xRanges = {Random.Range(playerPos.x - 230, playerPos.x - 180), Random.Range(playerPos.x + 180, playerPos.x + 230)};
            float[] yRanges = {Random.Range(playerPos.x -120, playerPos.x - 80), Random.Range(playerPos.x + 120, playerPos.x + 80)};
            float x = xRanges[Random.Range(0,2)];
            float y = yRanges[Random.Range(0,2)];
            p.transform.position = new Vector3(x, y, 0f);
         }
    }
}
