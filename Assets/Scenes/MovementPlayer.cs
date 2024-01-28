using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    float speed_player = 6f;
    public Rigidbody2D rb;
    public Animator move_animation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // float axis_x = Input.GetAxis("Horizontal");
        // float axis_y = Input.GetAxis("Vertical");
        // Vector2 movement = new Vector2(speed_player * axis_x, speed_player * axis_y);
        // movement *= Time.deltaTime;
        // transform.Translate(movement);
        Vector3 horizontal= new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0.0f);
        move_animation.SetFloat("Horizontal", horizontal.x);
        move_animation.SetFloat("Vertical", horizontal.y);
        move_animation.SetFloat("Magnitude", horizontal.magnitude);
        rb.MovePosition(transform.position + horizontal * Time.deltaTime * speed_player);

        //transform.position = transform.position + horizontal * Time.deltaTime * speed_player;

    }
}
