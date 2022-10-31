using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAutoProjectile : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public float fireRate = 1f;
    public float nextFire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFire){
            Instantiate (bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
