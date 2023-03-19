using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    //private float orginalSpeed;
    public float gravity = -9.81f;
    public float jump = 3f;

    public Transform IfGrounded;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    public ShootScript theGun;

    public float smoothTime = 0.1f;
    //float smoothVelocity;

    Vector3 fallingForce;

    bool grounded;

    public float dashingVelocity = 14f;
    public float dashingTime = 0.5f;
    public GameObject playerModel;
    public GameObject pistol;
    public GameObject rifle;
    public GameObject shotgun;
    public GameObject sword;
    public GameObject holder;

    public Vector3 move;
    //private Vector3 lastMoveDir;
    //bool isDashing = false;

    private Animator animator;
    Animation anim;

    public float x;
    public float z;

    public GameObject refRigPistolHolder;
    // Start is called before the first frame update
    void Start()
    {
        animator = playerModel.GetComponent<Animator>();
        animator.SetBool("rifle_idle", false);
    }
    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0f, z).normalized;
        if (sword.activeSelf)
        {
            controller.Move(move * (speed+4) * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        animator.SetBool("rifle_run", false);
        animator.SetBool("rifle_idle", false);
        animator.SetBool("run_pistol", false);
        animator.SetBool("sword_run", false);
        animator.SetBool("sword_idle", false);
        if (pistol.activeSelf)
        {
            if (x != 0 || z != 0)
            {
                animator.SetBool("run_pistol", true);
                animator.SetBool("sword_idle", false);
                animator.SetBool("rifle_run", false);
                animator.SetBool("rifle_idle", false);
                animator.SetBool("sword_run", false);
            }
            else
            {
                animator.SetBool("run_pistol", false);
                animator.SetBool("run_pistol_back", false);
                animator.SetBool("sword_idle", false);
                animator.SetBool("rifle_idle", false);
                animator.SetBool("rifle_run", false);
            }
        }
        if (rifle.activeSelf)
        {
            if (x != 0 || z != 0)
            {
                animator.SetBool("rifle_run", true);
                animator.SetBool("rifle_idle", false);
                animator.SetBool("sword_idle", false);
                animator.SetBool("sword_run", false);
                animator.SetBool("run_pistol", false);
            }
            else
            {
                animator.SetBool("rifle_run", false);
                animator.SetBool("rifle_idle", true);
                animator.SetBool("sword_idle", false);
                animator.SetBool("sword_run", false);
                animator.SetBool("run_pistol", false);
            }
        }
        if (shotgun.activeSelf)
        {
            if (x != 0 || z != 0)
            {
                animator.SetBool("rifle_run", true);
                animator.SetBool("rifle_idle", false);
                animator.SetBool("sword_idle", false);
                animator.SetBool("sword_run", false);
                animator.SetBool("run_pistol", false);
            }
            else
            {
                animator.SetBool("rifle_run", false);
                animator.SetBool("rifle_idle", true);
                animator.SetBool("sword_idle", false);
                animator.SetBool("sword_run", false);
                animator.SetBool("run_pistol", false);
            }
        }
        if (sword.activeSelf)
        {
            if (x != 0 || z != 0)
            {
                animator.SetBool("rifle_run", false);
                animator.SetBool("rifle_idle", false);
                animator.SetBool("sword_idle", false);
                animator.SetBool("sword_run", true);
            }
            else
            {
                animator.SetBool("rifle_run", false);
                animator.SetBool("rifle_idle", false);
                animator.SetBool("sword_idle", true);
                animator.SetBool("sword_run", false);
            }
        }
        //Debug.Log(animator.GetBool("run_pistol"));
        //Vector3 lastMoveDir = new Vector3(move.x, 0f).normalized;

        //orginalSpeed = speed;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed + 4;
            animator.SetFloat("multiplier", 1.2f);
        }
       
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed - 4;
        }

        /*
        if(Input.GetKeyDown(KeyCode.K) && !isDashing)
        {
                StartCoroutine(Dash());
                isDashing = true;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            speed = orginalSpeed;
            isDashing = false;
        }
        */


        /*
        float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        */
        fallingForce.y = fallingForce.y + gravity * Time.deltaTime;
        controller.Move(fallingForce * Time.deltaTime);
        /*
        grounded = Physics.CheckSphere(IfGrounded.position, groundDistance, groundMask);

        if(grounded && fallingForce.y < 0)
        {
            fallingForce.y = -1f;
        }

        if(Input.GetButtonDown("Jump") && grounded)
        {
            fallingForce.y = Mathf.Sqrt(jump * -1f * gravity);
        }
        */
    }
    /*
    IEnumerator Dash()
    {
            while (Time.time < Time.time + dashingTime)
            {
            speed = speed + dashingVelocity;

            yield return null;
            }
        speed = orginalSpeed;
    }
    */
}
