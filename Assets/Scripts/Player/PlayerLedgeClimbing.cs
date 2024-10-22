using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLedgeClimb : MonoBehaviour
{
    // Single triggers
    public static event Action OnLedgeHangStart;
    public static event Action OnLedgeHangEnd;
    public static event Action OnLedgeClimbStart;
    public static event Action OnLedgeClimbEnd;
    public static event Action OnLedgeReleaseStart;
    public static event Action OnLedgeReleaseEnd;

    [Header("LedgeClimb")]
    [Tooltip("Layer to detect ledges")]
    [SerializeField] LayerMask LedgeLayer;
    
    [Tooltip("Empty GameObject used to detect ledge position")]
    [SerializeField] Transform LedgeCheck;

    [Tooltip("Offset for the second check that avoid colliding with the wall")]
    [SerializeField] Vector3 LedgeUpperCheck = new Vector2(0,1f);

    [Tooltip("Radius of the Ledge Check")]
    [SerializeField] float LedgeCheckRadius = 0.25f;

    [Tooltip("Offset after hanging on ledge")]
    [SerializeField] Vector2 Offset0 = new Vector2(0f,0f);

    [Tooltip("Offset position when climbing")]
    [SerializeField] Vector2 Offset1 = new Vector2(1f,2f);

    [Tooltip("Offset position when realising ledge")]
    [SerializeField] Vector2 Offset2 = new Vector2(0f, 0f);

    [Tooltip("Speed of ledge hang")]
    [SerializeField][Range(1f, 10f)] float LedgeHangSpeed = 5f;

    [Tooltip("Time to wait before starting the climb")]
    [SerializeField][Range(0f, 2f)] private float WaitTime = 1f;

    private Rigidbody2D rb;
    private Collider2D playerCollider2D;
    private InputAction moveAction;
    private Coroutine activeCoroutine;
    private Vector2 moveInput;
    private Vector2 ledgeCornerPosition;
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
            rb.velocity = Vector3.zero;
            HandleClimbingInput();
        }
    }

    private void CheckFacingDirection() 
    {
        isFacingRight = (LedgeCheck.position - transform.position).x > 0;
    }

    private void DetectLedge()
    {
        Collider2D hitCollider1 = null;
        Collider2D hitCollider0 = Physics2D.OverlapCircle(
            LedgeCheck.position + LedgeUpperCheck,
            LedgeCheckRadius,
            LedgeLayer);

        if (hitCollider0 == null)
        {
            hitCollider1 = Physics2D.OverlapCircle(
            LedgeCheck.position,
            LedgeCheckRadius,
            LedgeLayer);
        }

        // Check if the ledge is being touched
        isTouchingLedge = hitCollider1 != null;

        // If the ledge was touched for the first time
        if (isTouchingLedge && !ledgeTouchedPreviously)
        {
            // Get the ledge's corner position
            Bounds colliderBounds = hitCollider1.bounds;
            float cornerX = isFacingRight ? colliderBounds.min.x : colliderBounds.max.x;
            float cornerY = colliderBounds.max.y;
            ledgeCornerPosition = new Vector2(cornerX, cornerY);

            ledgeTouchedPreviously = true;

            // Start ledge grab logic
            StartCoroutine(LedgeHang());
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
            activeCoroutine = StartCoroutine(LedgeClimb());
        }

        if (moveInput.y < 0 && !downKeyHeldDown)
        {
            downKeyHeldDown = true; // Key pressed
            if (activeCoroutine != null) StopCoroutine(activeCoroutine); // Stop active coroutine if running
            LedgeRelease();
        }
        if (moveInput.y == 0)
        {
            upKeyHeldDown = false; // Key released
            downKeyHeldDown = false; // Key released
        }
    }

    private IEnumerator LedgeHang()
    {
        OnLedgeHangStart();
        playerCollider2D.enabled = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        Vector2 flippedOffset0 = new (Offset0.x * -1, Offset0.y);
        Vector2 ledgeTarget = ledgeCornerPosition + (isFacingRight ? Offset0 : flippedOffset0);

        // Move the character up and towards the ledge
        while (Vector2.Distance(transform.position, ledgeTarget) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, ledgeTarget, LedgeHangSpeed * Time.deltaTime);
            yield return null;
        }

        OnLedgeHangEnd?.Invoke();
        playerCollider2D.enabled = true;
        isHanging = true;
    }

    private IEnumerator LedgeClimb()
    {
        OnLedgeClimbStart?.Invoke();

        Vector2 climbTarget;

        if (isFacingRight)
        {
            climbTarget = new(
            ledgeCornerPosition.x + Offset1.x,
            ledgeCornerPosition.y + Offset1.y);
        }
        else
        {
            climbTarget = new(
            ledgeCornerPosition.x - Offset1.x,
            ledgeCornerPosition.y + Offset1.y);
        }

        yield return new WaitForSeconds(WaitTime);

        transform.position = climbTarget;
        OnLedgeClimbEnd?.Invoke(); 
        rb.gravityScale = gravityScale;
        isHanging = false;
        upKeyHeldDown = false;
        activeCoroutine = null; 

    }

    private void LedgeRelease()
    {
        OnLedgeReleaseStart?.Invoke();

        Vector2 dropTarget;

        if (isFacingRight)
        {
            dropTarget  = new Vector2(
            transform.position.x + Offset2.x,
            transform.position.y + Offset2.y);
        }
        else
        {
            dropTarget = new Vector2(
            transform.position.x - Offset2.x,
            transform.position.y + Offset2.y);
        }

        transform.position = dropTarget;
        OnLedgeReleaseEnd?.Invoke();
        rb.gravityScale = gravityScale;
        isHanging = false;
        downKeyHeldDown = false;
        activeCoroutine = null; 
    }

    private void OnDrawGizmos()
    {
        if(isTouchingLedge) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LedgeCheck.position, LedgeCheckRadius);
        Gizmos.DrawWireSphere(LedgeCheck.position + LedgeUpperCheck, LedgeCheckRadius);
    }
}
