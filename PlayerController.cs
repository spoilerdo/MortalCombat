using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //speed
    private int speed = 2;
    private int slidingspeed = 5;

    //jump variabelen
    private float MaxJumpForce = 7;
    private float JumpForce;
    private float MaxJumpDuration = 0.2f; //or .2f
    private float JumpDuration;
    private bool IsJumping = false;
    private bool OnGround;
    private bool IsFalling = false;
    private bool IsTakingOff = false;

    private Rigidbody2D rb;

    //overige bools
    private bool IsRight = true;
    private bool IsAttacking = false;
    private bool IsSliding = false;
    [HideInInspector]
    static public bool IsDamaged = false;

    Animator animator;

    Vector3 Scale;

    void Start() //Word geroepen in het begin van het script (eenmalig)
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Scale = rb.transform.localScale;
    }

    void FixedUpdate() //natuurkundige berekeningen
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(MoveHorizontal * speed, 0);

        DoAttack();
        DoJump(MoveVertical);
        DoSlide();
        Flip(MoveHorizontal);
        Animation();
    }
    #region Do Methods
    //Als je op (linkse) enter drukt dan maakt het karakter een aanval.
    void DoAttack()
    {
        if (Input.GetKeyUp(KeyCode.Return) || IsAttacking == true)
        {
            IsAttacking = false;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsAttacking = true;
        }
    }
    //als je op spatie klikt dan springt het karakter.
    void DoJump(float moveVertical)
    {
        if (moveVertical > 0.1f)//Als hij op de W klikt dan word dit > 0.1f
        {
            if (!IsJumping)
            {
                //Time.deltaTime moet je doen zodat een computer met een 
                //snellere processor het poppetje niet sneller laat springen.
                JumpDuration += Time.deltaTime;
                JumpForce += Time.deltaTime;
                IsTakingOff = true;
                if (JumpDuration < MaxJumpDuration)
                {
                    Vector2 velocity = new Vector2(rb.velocity.x, JumpForce);
                    rb.velocity = velocity;
                }
                else
                {
                    IsJumping = true;
                }
            }
        }
        if (!OnGround && moveVertical < 0.1f)
        {
            IsFalling = true;
        }
    }
    //Als je op de rechter shift drukt dan kan het poppetje sliden zo lang hij wil
    // TODO Fix this function
    void DoSlide()
    {
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            IsSliding = !IsSliding;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            IsSliding = true;
        }
    }
    #endregion 
    void Flip(float moveHorizontal) //zorgt ervoor dat het karakter de goede kant op kijkt.
    {
        if (moveHorizontal > 0 && !IsRight || moveHorizontal < 0 && IsRight)
        {
            IsRight = !IsRight;
            //rb.transform.localScale *= -1;
            Scale.x *= -1;
            rb.transform.localScale = Scale;
        }
    }
    //Deze void zorgt ervoor dat de animatie worden uitgevoerd op het goede moment.
    void Animation()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsAttacking", IsAttacking);
        animator.SetBool("IsJumping", IsTakingOff);
        animator.SetBool("IsSliding", IsSliding);
        animator.SetBool("IsDamaged", IsDamaged);

        if(IsDamaged == true)
        {
            IsDamaged = !IsDamaged;
        }
        //animator.SetFloat("Direction", rb.velocity.x);
        //animator.SetInteger("AttackCombo", AB.Combo);
    }
    #region Collision Methods
    //Als hij de grond raatk dan worden de volgende commando's uitgevoerd.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == ("Ground"))
        {
            OnGround = true;
            IsFalling = false;
            IsJumping = false;
            IsTakingOff = false;
            JumpDuration = 0;
            JumpForce = MaxJumpForce;
        }
    }
    //Als hij de grond verlaat dan worden de volgende commando's uitgevoerd.
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == ("Ground"))
        {
            OnGround = false;
        }
    }
    #endregion
}
