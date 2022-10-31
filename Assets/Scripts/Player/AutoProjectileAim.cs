using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoProjectileAim : MonoBehaviour
{
    public float speed = 15f;
    Rigidbody2D rb;
    Vector2 moveDirection;
    GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        target = FindClosestEnemy();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col){
        if(col.gameObject.tag.Equals("Enemy1")){
            Destroy (col.gameObject);
            Destroy (this.gameObject);
        }
    }

    void OnBecameInvisible(){
        Destroy(this.gameObject);
    }

    //Copied from Unity Documentation for GameObject. Will find the closest enemy
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy1");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
