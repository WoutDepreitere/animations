using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemy;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {

        //move left and right
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0)
            transform.localScale = new Vector3(4, 4,1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-4, 4,1);

        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
        {
            Jump();
            jumpTimeCounter = jumpTime;
            isJumping = true;
        }
            
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
            

        //higher jump
        if (Input.GetKey(KeyCode.UpArrow) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //sword slash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //sword animation
            anim.SetTrigger("attack");
            if (isHit())
            {
                Debug.Log("Hit!!");
            }
        }

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);

        if (!isGrounded())
        {
            anim.SetTrigger("airborne"); //bool!
        }
        

    }

    

    //logic
    private void Jump()
    {
        if (isGrounded())
        {
            anim.SetTrigger("jump");
        }
        body.velocity = Vector2.up * speed;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer | enemy);
        return raycastHit.collider != null;
    }

    private bool isHit()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.5f, enemy);
        return raycastHit.collider != null;
    }
}
