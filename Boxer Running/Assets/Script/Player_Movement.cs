using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public ObstaclsSystem os;

    public CharacterController controller;
    Touch touch;

    //Movement
    public float _speed_Movement_X = 2f;
    public float _speed_Movement_forward = 2f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform _GroundCheck;
    public float _raduisCheckGround = .4f;
    public LayerMask Groundmask;
    bool isGrounded;
    Vector3 moving;
    public float jumpHeight = 3;
    public float t;
    public float le;
    public Camera main;




    // Update is called once per frame
    void Update()
    {
        //Camera follower system
        if (transform.position.x < 2f)
            main.transform.position = Vector3.Lerp(main.transform.position, new Vector3(2f, transform.position.y+1 , transform.position.z+t), le * Time.deltaTime);
        else if (transform.position.x > 20)
            main.transform.position = Vector3.Lerp(main.transform.position, new Vector3(20f, transform.position.y+1 , transform.position.z +t), le * Time.deltaTime);
        else
            main.transform.position = Vector3.Lerp(main.transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + t), le * Time.deltaTime);

        //check every frame if player is on ground or not
        isGrounded = Physics.CheckSphere(_GroundCheck.position, _raduisCheckGround, Groundmask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;


        //Move the player in forward direction automatically
        controller.Move(transform.forward * _speed_Movement_forward * Time.deltaTime);


        //Move the player in x axis
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {

                float x = touch.deltaPosition.x * Time.deltaTime;
                moving = Vector3.right * x;
                controller.Move(moving * _speed_Movement_X * Time.deltaTime);


            }
        }

        //smooth fall to the ground
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    //Jump system
    public void JumpSystem()
    {
        Debug.Log("rrr");

        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish") JumpSystem();

        if (other.gameObject.tag == "Obstacls")
        {
            // call obstacles system method
            os.DestroyObstacle(other.transform);
            StartCoroutine(SlowMotion());
        }
    }




    //Slow motion Effect
    IEnumerator SlowMotion()
    {
        int timescaling = 1;
        while (timescaling > 0)
        {
            Time.timeScale = 0.2f;
            yield return new WaitForSeconds(.2f);
            Time.timeScale = 1f;
            Debug.Log("gg");
            timescaling--;
        }

    }
}




