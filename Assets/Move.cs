using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    Rigidbody rb;
    public CharacterController cc;
    public Transform cam;
    public Animator ain;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHieght = 10f;
    Vector3 ve;
    public float dashSpeed;
    public float dashTime;
    public bool dashing = true;
    public float dashingPower = 20f;
    public float dashingTime = 12f;
    public float mSpeed;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    

    [SerializeField] public TrailRenderer tr;

    Vector3 move;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        ain = GetComponent<Animator>();
        tr.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && ve.y < 0)
        {
            ve.y = -2f;
        }
        if(isGrounded == false)
        {
            ain.SetBool("inAir", true);
        }
        else
        {
            ain.SetBool("inAir", false);
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        ain.SetBool("rool", false);
        ain.SetBool("sprint", false);
        ain.SetBool("jump", false);
        


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * mSpeed;
            cc.Move(moveDir * moveSpeed * Time.deltaTime);
            ain.SetBool("run", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                mSpeed = 1.25f;
                ain.SetBool("sprint", true);
            }
            else
            {
                mSpeed = 1;
                ain.SetBool("sprint", false);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                mSpeed = 0.15f;
                ain.SetBool("slow", true);
            }
            else
            {
                mSpeed = 1;
                ain.SetBool("slow", false);
            }
        }
        else
        {
            ain.SetBool("run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        { 
            ain.SetBool("jump", true);
            ve.y = Mathf.Sqrt(jumpHieght * -2f * gravity);
           
        }

        if (Input.GetKeyDown("e") && dashing && Input.GetKey(KeyCode.LeftShift))
        {
            ain.SetBool("rool", true);
            StartCoroutine(Dash());
            
        }
        ve.y += gravity * Time.deltaTime;

        cc.Move(ve * Time.deltaTime);
    }
    
    private IEnumerator Dash()
    {
        dashing = false;
        tr.emitting = true;

        ve = new Vector3(transform.forward.x * dashingPower, 0f, transform.forward.z * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        ve = Vector3.zero;
        tr.emitting = false;
        yield return new WaitForSeconds(2);
        dashing = true;
    }
   

}
