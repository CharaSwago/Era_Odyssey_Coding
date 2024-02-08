using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour

{
    // Variable publique, mouvement du perso
    public float moveSpeed;
    public float wallSpeed;
    private bool isMoving;

    // Variable publique du saut
    [Range(1,10)]
    public float jumpForce;
    private bool isJumping = false;

    // Variable bool 
    private bool isGrounded;
    private bool isWall;
    private bool isWallJumping = false ; 


    // Variable pour la vitesse de glisse + systeme de glisse
    private bool wallSlideCheck = false;
    private float slideSpeed = 2f;

    // Variable pour le systeme d'accroche au mur 
    public Transform wallCheckRigth;
    private float wallCheckRigthRadius = 0.1f;
    public Transform wallCheckLeft;
    private float wallCheckLeftRadius = 0.1f;
    private bool isWallGrab;

    // Variable pour le systeme de saut sur le mur
    [Header("Wall Jump")]
    [SerializeField] float wallJumpForce = 18f;
    [SerializeField] float wallJumpDirection;
    [SerializeField] Vector2 wallJumpAngle;
    private float waitWallJump = 0.5f;

    // Variable pour le systeme du saut 
    public Transform groundCheck;
    private float groundCheckRadius = 0.1f;
    public LayerMask collisionLayers;

    // L'animation
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb; 

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    // Variable du dash
    private float dashingVelocity = 5f;
    private float dashingTime = 0.45f; 
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDashing = true;
    [SerializeField] private TrailRenderer trailRenderer; 

    void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Jump") && (isGrounded || isWall))
        {
            isJumping = true;
        } else if(isGrounded) {
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

        if(isWall && !isGrounded && Input.GetButton("Horizontal")){
            wallSlide();
        } else if (isWall && !isGrounded  && Input.GetButtonDown("Jump")){
            wallJump();
            StartCoroutine(stopWallJump());
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", characterVelocity);

        isWallGrab = isWall && Input.GetButton("Grab");

        if(isWallGrab){
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(Input.GetAxis("Vertical"), -wallSpeed, float.MaxValue));
            // Définit la gravité à zéro
            rb.gravityScale = 0f; 
        } else {
            // Rétablit la gravité normale si l'objet n'est pas en train de saisir le mur
            rb.gravityScale = 1f; 
        }
    }

    void FixedUpdate()
    {
        // Zone de détection mur
        isWall = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckLeftRadius, collisionLayers) || 
        Physics2D.OverlapCircle(wallCheckRigth.position, wallCheckRigthRadius, collisionLayers);

        // Zone de détection sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        if(!isWallJumping)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        } 

        if(isJumping)
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
        // for ground check
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // for wall check
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckLeftRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(wallCheckRigth.position, wallCheckRigthRadius);
    }

    private IEnumerator stopDashing(){
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }

    private void wallSlide(){
        if(isWall && !isGrounded){
            wallSlideCheck = true; 
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        }else{
            wallSlideCheck = false;
        }
    }

    private void wallJump(){
        if(horizontalMovement < 0f){
            wallJumpDirection = -1f;
        } else {
            wallJumpDirection = 1;
        }
        isWallJumping = true; 
        // Appliquer une force de saut avec la nouvelle direction et angle
        rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
        isWallJumping = false;
    } 

    private IEnumerator stopWallJump(){
        yield return new WaitForSeconds(waitWallJump);
        isWallJumping = false; 
        isWall = false;
    }
}
