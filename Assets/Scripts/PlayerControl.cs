using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] GameObject characterModel;
    private Animator anim;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        var velocity = new Vector3(horizontalInput * walkSpeed, rb.velocity.y, verticalInput * walkSpeed); 

        rb.velocity = velocity; //move based on input

        anim.SetFloat("Speed", velocity.magnitude); //set animation controller to walk or idle based on input

        RotateCharacter(velocity);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!IsGrounded()) return;  //if player already in the air, can't jump

        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); //set y velocity to jumpForce
        jumpSound.Play();
    }

    void RotateCharacter(Vector3 v)
    {
        if (v.z != 0 || v.x != 0)
        {
            var angle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;  //find angle we are moving at
            characterModel.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);    //face angle
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .3f, ground);  //check if bottom collider is within .3f of ground
    }
}
