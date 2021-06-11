////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////                                                                                                                    //////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    ///////////////////////////////////////
////////////////////////////                    /////////////////////////////////////////////////////                      ///////////    /////////////////////////////////////
////////////////////////////                    /////////////////////////////////////////////////////                      //////////     //////////////////////////////////////
////////////////////////////                    /////////////////////////////////////////////////////                      //////////     ///////////////////////////////////////
////////////////////////////                    /////////////////////////////////////////////////////                      //////////     //////////////////////////////////////
////////////////////////////                    /////////////////////////////////////////////////////                      /////////      ///////////////////////////////////////

///////////////////////////////////////////////////////////              ///////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////              ///////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////              ///////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////              ///////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////              ///////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////              ///////////////////////////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#region [[[Movement]]]

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
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
    public Animator Run_Anim;
    float x = 0;

    // Health
    static int health = 3;
    static int damage = 1;
    public Image[] healthBar;
    public Image _damage_IMG;


    //Coin Collector system
  
    Coins_Manager coins_Manager;


    private void Start()
    {
        coins_Manager = FindObjectOfType<Coins_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Effects_System.AllowToPlay)
        {


            ////Camera follower system
            //if (transform.position.x < -1f)
            //    main.transform.position = Vector3.Lerp(main.transform.position, new Vector3(transform.position.x , transform.position.y + 3, transform.position.z + t), le * Time.deltaTime);
            //else if (transform.position.x > 1)
            //    main.transform.position = Vector3.Lerp(main.transform.position, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z + t), le * Time.deltaTime);
            //else
            //    main.transform.position = Vector3.Lerp(main.transform.position, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z + t), le * Time.deltaTime);

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
                    x = touch.deltaPosition.x * Time.deltaTime;
                    moving = Vector3.right * x;
                    controller.Move(moving * _speed_Movement_X * Time.deltaTime);

                }
            }
        }

        //smooth fall to the ground
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    //Jump system
    public void JumpSystem()
    {

        if (isGrounded)
        {


            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //Play Animation
            Run_Anim.SetBool("AllowToJump", true);

            //  Run_Anim.SetBool("AllowToJump", false);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Jumpper") JumpSystem();

        if (other.gameObject.tag == "Obstacls")
        {
            Vibration.Vibrate(1);
            Damage();
            os.DestroyObstacle(other.transform);
            StartCoroutine(SlowMotion());
        }

        if (other.gameObject.tag == "Coin")
        {
            coins_Manager.AddCoin(other.transform.position, 1);

            coins_Manager.Collect_Coins(other.transform);
            Destroy(other.gameObject);
        
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


    //Dammage function when player hit an obstacle
    public void Damage()
    {
        _damage_IMG.DOFade(1, .2f).OnComplete(() => _damage_IMG.DOFade(0, .2f));
        health -= damage;
        healthBar[health].enabled = false;
        // check if health equal 0 
        if (health == 0)
        {
            Debug.Log("die");
            //Game Over
            //Restart or Home
        }
    }
}

#endregion


