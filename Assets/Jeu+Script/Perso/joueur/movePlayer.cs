using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour

{
    // Variable publique, mouvement du perso
    public float moveSpeed;
    public float wallSpeed;
    private bool isMoving;

    // Variable publique, saut + force du saut + savoir si le perso touche le sol
    [Range(1,10)]
    public float jumpForce;
    public bool isJumping = false;
    private bool isGrounded;
    private bool isWall;
    private float slideSpeed = 1.5f;

    // Variable pour le systeme d'accroche au mur 
    public Transform wallCheckRigth;
    public float wallCheckRigthRadius;
    public Transform wallCheckLeft;
    public float wallCheckLeftRadius;
    public bool wallGrab;

    // Variable pour le systeme du saut sur le mur
    private bool isWallJumping;
    private float wallJumpingDirection;
    public float wallJumpingTime;
    private float WallJumpingCounter;
    public float wallJumpingDuration;


    // Variable pour le systeme du saut 
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    // L'animation du saut
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb; 

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    public float dashingVelocity = 5f;
    private float dashingTime = 0.5f; 
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDashing = true;
    [SerializeField] private TrailRenderer trailRenderer; 

    void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            canDashing = true;
        }

        if (Input.GetButtonDown("Dash") && canDashing){
            canDashing = false;
            isDashing = true;
            trailRenderer.emitting = true;
            dashingDir = new Vector2(horizontalMovement, Input.GetAxis("Vertical"));
            if(dashingDir == Vector2.zero){
                dashingDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(stopDashing());
        }

        if(isDashing){
            rb.velocity = dashingDir.normalized * dashingVelocity;
        }

        if(isWall && !isGrounded){
            wallSide();
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", characterVelocity);

        wallGrab = isWall && Input.GetButton("Grab");

        if(wallGrab){
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(Input.GetAxis("Vertical"),  -wallSpeed, float.MaxValue));
            rb.gravityScale = 0f; // Définit la gravité à zéro
        } 
        else {
            rb.gravityScale = 1f; // Rétablit la gravité normale si l'objet n'est pas en train de saisir le mur
        }

    }

    void FixedUpdate()
    {
        isWall = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckLeftRadius, collisionLayers) || 
        Physics2D.OverlapCircle(wallCheckRigth.position, wallCheckRigthRadius, collisionLayers);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = false;
        } 
    }
    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f){
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckLeftRadius);
        Gizmos.DrawWireSphere(wallCheckRigth.position, wallCheckRigthRadius);
    }

    private IEnumerator stopDashing(){
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }

    private void wallSide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
    }

}
