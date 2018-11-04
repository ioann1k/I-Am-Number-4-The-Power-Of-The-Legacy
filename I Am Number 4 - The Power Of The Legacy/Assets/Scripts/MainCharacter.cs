using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private const float initialJumpForce = 2.0f;

    private Vector2 jump;

    [Tooltip("The force with which the player would jump (type of variable - float).")]
    public float jumpForce = 2.0f;

    private Animator animationsController;

    [SerializeField]
    private LayerMask groundLayerMask;

    public Transform groundCheck;

    private const float groundedRadius = 0.2f;

    private bool isOnGround = true;
    private bool isRollingOver = false;

    Rigidbody2D rb;

    // Use this for initialization
    void Awake()
    {
        animationsController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");

        jump = new Vector2(0.0f, 6.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        Jump();
        RollOver();
    }

    private void GroundCheck()
    {
        isOnGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayerMask);
        foreach (Collider2D collider in colliders)
        {
            GameObject colliderGameObject = collider.gameObject;
            if (colliderGameObject != gameObject)
            {
                isOnGround = true;
            }
        }

        animationsController.SetBool("Ground", isOnGround);
        animationsController.SetFloat("vSpeed", rb.velocity.y);
    }

    private void Jump()
    {
        bool shouldJump = Input.GetKeyDown(KeyCode.Space);
        bool animationsControllerIsOnGround = animationsController.GetBool("Ground");
        bool isRunning = false;

        var animationInfo = animationsController.GetCurrentAnimatorClipInfo(0);
        foreach (var animation in animationInfo)
        {
            string currentAnimationName = animation.clip.name;
            if (currentAnimationName == "Run")
            {
                isRunning = true;

                break;
            }
        }

        if (isOnGround && shouldJump && animationsControllerIsOnGround && isRunning)
        {
            isOnGround = false;
            animationsController.SetBool("Ground", isOnGround);
            rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);

            isRollingOver = false;
        }
    }

    private void RollOver()
    {
        bool shouldRollOver = Input.GetKeyDown(KeyCode.LeftControl);
        bool animationsControllerIsOnGround = animationsController.GetBool("Ground");
        if (shouldRollOver)
        {
            if (isOnGround && animationsControllerIsOnGround)
            {
                animationsController.Play("Roll over");

                isRollingOver = true;
            }
            else
            {
                isRollingOver = false;
            }
        }
    }
}
