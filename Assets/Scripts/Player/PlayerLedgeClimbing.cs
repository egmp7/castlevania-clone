using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLedgeClimb : MonoBehaviour
{
    public static event Action OnLedge;
    public static event Action OffLedge;
    public static event Action OnLedgeClimb;
    public static event Action OnLedgeHang;

    [Header("LedgeClimb")]
    [Tooltip("Layer to detect ledges")]
    [SerializeField] LayerMask LedgeLayer;
    
    [Tooltip("Empty GameObject used to detect ledge position")]
    [SerializeField] Transform LedgeCheck;
    
    [Tooltip("Offset to move character to after grabbing ledge")]
    [SerializeField] Vector2 LedgeOffset = new Vector2(-3f,0f);

    [Tooltip("Offset position for character to climb when holding ledge")]
    [SerializeField] Vector2 ClimbTargetOffset = new Vector2(1f,2f); 

    [Tooltip("Offset position for character to when holding ledge")]
    [SerializeField] Vector2 DropTargetOffset = new Vector2(-1f,-2f); 

    [Tooltip("Speed of climbing")]
    [SerializeField][Range(1f, 10f)] float ClimbSpeed = 5f;
    
    [Tooltip("Distance that expands the jumping boxcast")]
    [SerializeField][Range(0f, 2f)] float RaycastDistance = 1f;

    private Rigidbody2D rb;
    private Collider2D playerCollider2D;
    private InputAction moveAction;
    private Coroutine activeCoroutine;
    private Vector2 moveInput;
    private Vector2 ledgeCenterTopPosition;
    private float gravityScale;
    private bool isTouchingLedge;
    private bool ledgeTouchedPreviously;
    private bool isHanging; // happens after player finishes moving to edge
    private bool upKeyHeldDown;
    private bool downKeyHeldDown;
    private bool isFacingRight;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        playerCollider2D = GetComponent<Collider2D>();
        gravityScale = rb.gravityScale;
    }

    private void Update()
    {
        CheckFacingDirection();
        DetectLedge();

        if (isHanging)
        {
            HandleClimbingInput();
        }
    }

    private void CheckFacingDirection() 
    {
        isFacingRight = (LedgeCheck.position - transform.position).x > 0;
    }

    private void DetectLedge()
    {
        // Determine direction based on player facing
        Vector2 rayDirection = isFacingRight ? Vector2.right : Vector2.left;
        Vector2 rayOrigin = LedgeCheck.position;

        // Perform the Raycast to detect ledge collision
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, RaycastDistance, LedgeLayer);

        // Check if the ledge is being touched
        isTouchingLedge = hit.collider != null;

        // If the ledge was touched for the first time
        if (isTouchingLedge && !ledgeTouchedPreviously)
        {
            // Get the ledge's top center position
            Bounds colliderBounds = hit.collider.bounds;
            ledgeCenterTopPosition = new Vector2(colliderBounds.center.x, colliderBounds.max.y);

            // Trigger the OnLedge event
            OnLedge?.Invoke();
            ledgeTouchedPreviously = true;

            // Start ledge grab logic
            StartCoroutine(LedgeGrab());
        }
        // If the ledge was released
        else if (!isTouchingLedge && ledgeTouchedPreviously)
        {            
            ledgeTouchedPreviously = false;
        }
    }


    private void HandleClimbingInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.y > 0 && !upKeyHeldDown)
        {
            upKeyHeldDown = true; // Key pressed
            if (activeCoroutine != null) StopCoroutine(activeCoroutine); // Stop active coroutine if running
            activeCoroutine = StartCoroutine(ClimbLedge());
        }

        if (moveInput.y < 0 && !downKeyHeldDown)
        {
            downKeyHeldDown = true; // Key pressed
            if (activeCoroutine != null) StopCoroutine(activeCoroutine); // Stop active coroutine if running
            activeCoroutine = StartCoroutine(DropLedge());
        }
        if (moveInput.y == 0)
        {
            upKeyHeldDown = false; // Key released
            downKeyHeldDown = false; // Key released
        }
    }

    private IEnumerator LedgeGrab()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        Vector2 flippedLedgeOffset = new (LedgeOffset.x * -1, LedgeOffset.y);
        Vector2 ledgeTarget = ledgeCenterTopPosition + (isFacingRight ? LedgeOffset : flippedLedgeOffset);

        // move
        while (Vector2.Distance(transform.position, ledgeTarget) > 0.1f)
        {
            playerCollider2D.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, ledgeTarget, ClimbSpeed * Time.deltaTime);
            yield return null;
        }

        OnLedgeHang?.Invoke();
        playerCollider2D.enabled = true;
        isHanging = true;
    }

    private IEnumerator ClimbLedge()
    {
        //animator.SetBool("isClimbing", true);

        // Move the character up and over the ledge
        Vector2 climbTarget;

        if (isFacingRight)
        {
            climbTarget = new(
            transform.position.x + ClimbTargetOffset.x,
            transform.position.y + ClimbTargetOffset.y);
        }
        else
        {
            climbTarget = new(
            transform.position.x - ClimbTargetOffset.x,
            transform.position.y + ClimbTargetOffset.y);
        }

        OnLedgeClimb?.Invoke();

        while (Vector2.Distance(transform.position, climbTarget) > 0.1f)
        {
            playerCollider2D.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, climbTarget, ClimbSpeed * Time.deltaTime);
            yield return null;
        }

        playerCollider2D.enabled = true;

        OffLedge?.Invoke();

        // Reset to normal movement after climbing
        //animator.SetBool("isHanging", false);
        //animator.SetBool("isClimbing", false);

        // Reset to normal movement after climbing
        rb.velocity = Vector2.zero; // Ensure no lingering velocity after climbing
        rb.gravityScale = gravityScale;
        isHanging = false;
        upKeyHeldDown = false;
        activeCoroutine = null; // Reset activeCoroutine reference after coroutine finishes
    }

    private IEnumerator DropLedge()
    {
        // Reset player to falling state
        //animator.SetBool("isHanging", false);

        OffLedge?.Invoke();

        // Move the character up and over the ledge
        Vector2 dropTarget;

        if (isFacingRight)
        {
            dropTarget  = new Vector2(
            transform.position.x + DropTargetOffset.x,
            transform.position.y + DropTargetOffset.y);
        }
        else
        {
            dropTarget = new Vector2(
           transform.position.x - DropTargetOffset.x,
           transform.position.y + DropTargetOffset.y);
        }

        while (Vector2.Distance(transform.position, dropTarget) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, dropTarget, ClimbSpeed * Time.deltaTime);
            yield return null;
        }

        rb.velocity = Vector2.zero; // Ensure no lingering velocity after climbing
        rb.gravityScale = gravityScale;
        isHanging = false;
        downKeyHeldDown = false;
        activeCoroutine = null; // Reset activeCoroutine reference after coroutine finishes
    }

    // Gizmos for debugging ledge detection
    private void OnDrawGizmosSelected()
    {
        if(isTouchingLedge) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Vector3 direction = isFacingRight ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(LedgeCheck.position, LedgeCheck.position + direction * RaycastDistance);
    }
}
