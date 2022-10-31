using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float playerHealth = 10;
    public KeyCode jumpKey;
    public float speed = 10f;
    void Update()
    {
        if(playerHealth <= 0){
            // Game over screen
        }


        WASD_Movement();
    }

    private void WASD_Movement(){
        Vector3 pos = transform.position;
        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }
        transform.position = pos;
    }
}
