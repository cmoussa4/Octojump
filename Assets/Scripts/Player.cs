using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   //Float Variables
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpVelocity = 5f;
    //Bools
    private bool canJump = true;
    //Layermask
    [SerializeField] LayerMask jumpLayer;
    //Component References
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    public Animator animator;
    //getting components in awake function
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }
    //simple movement script using transforms
    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }



    }
    //jumping with rigidbody physics
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && GroundCheck() && canJump)
        {
            animator.SetBool("isJumping", true);
            rb.velocity += Vector2.up * jumpVelocity;
            canJump = false;
            StartCoroutine(AnimatorWait());
        }
        
    }
    
    //bool for checking if the player hits the ground through raycasting at the bottom of the sprite and detecting the specified jumplayer
    private bool GroundCheck()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.05f, jumpLayer);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //animator.SetBool("isJumping", false);
            canJump = true;
        }
    }

    IEnumerator AnimatorWait()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("isJumping", false);
    }
}
