using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLedgeClimb : MonoBehaviour
{
    public static event Action OnLedgeHang;
    public static event Action OnLedgeClimb;
    public static event Action OnLedgeRelease;

    [Header("LedgeClimb")]
    [Tooltip("Layer to detect ledges")]
    [SerializeField] LayerMask LedgeLayer;
    
    [Tooltip("Empty GameObject used to detect ledge position")]
    [SerializeField] Transform LedgeDetector0;

    [Tooltip("Empty GameObject used to detect ledge position")]
    [SerializeField] Transform LedgeDetector1;

    [Tooltip("Radius of the Ledge Check")]
    [SerializeField][Range(0.01f,1f)] float LedgeDetectorRadius = 0.25f;

    [SerializeField] Vector2 BoxcastSize = new(2f, 2f);

    [Tooltip("Offset after hanging on ledge")]
    [SerializeField] Vector2 Offset0 = new Vector2(0f,0f);

    [Tooltip("Offset position when climbing")]
    [SerializeField] Vector2 Offset1 = new Vector2(1f,2f);

    [Tooltip("Time before activating the Ledge Detector When releasing")]
    [SerializeField][Range(0.01f, 1f)] float ReleaseThresholdTime = 0.25f;

    private PlayerState playerState;
    private Rigidbody2D rb;
    private InputAction moveAction;
    private Vector2 moveInput;
    private float gravityScale;
    private bool isLedgeDetected;
    private bool canGrabLedge = true;

    private Vector2 initPosition;
    private Vector2 endClimbPosition;

    // event guards
    private bool isOnClimbEventTriggered;
    private bool isOnHangEventTriggered;
    private bool isOnReleaseEventTriggered;


    private void OnEnable()
    {
        PlayerAnimation.OnClimbAnimationEnded += OnClimbAnimationEnded;
    }
    private void OnDisable()
    {
        PlayerAnimation.OnClimbAnimationEnded -= OnClimbAnimationEnded;
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    private void Update()
    {
        DetectLedge();

        if (isOnHangEventTriggered)
        {
            HandleClimbingInput();
        }
    }

    private void DetectLedge()
    {
        Collider2D hitCollider1 = null;
        RaycastHit2D hitCollider0 = Physics2D.BoxCast(
            new Vector2(LedgeDetector0.position.x,LedgeDetector0.position.y),
            BoxcastSize,
            0f,
            Vector2.right,
            0f,
            LedgeLayer);

        if (hitCollider0.collider == null) // can detect ledge
        {
            hitCollider1 = Physics2D.OverlapCircle(
            LedgeDetector1.position,
            LedgeDetectorRadius,
            LedgeLayer);
        }

        // Check if the ledge is being touched
        isLedgeDetected = hitCollider1 != null;

        if (isLedgeDetected && !isOnHangEventTriggered && canGrabLedge)
        {

            Debug.Log("OnLedgeHang");
            OnLedgeHang?.Invoke();
            isOnHangEventTriggered = true;
            canGrabLedge = false;

            if (playerState.IsFacingRight)
            {
                initPosition = new(
                LedgeDetector1.position.x + Offset0.x,
                LedgeDetector1.position.y + Offset0.y);

                endClimbPosition = new(
                LedgeDetector1.position.x + Offset1.x,
                LedgeDetector1.position.y + Offset1.y);
            }
            else
            {
                initPosition = new(
                LedgeDetector1.position.x - Offset0.x,
                LedgeDetector1.position.y + Offset0.y);

                endClimbPosition = new(
                LedgeDetector1.position.x - Offset1.x,
                LedgeDetector1.position.y + Offset1.y);
            }

            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            transform.position = initPosition;
        }
        
    }

    private void HandleClimbingInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.y > 0 && !isOnClimbEventTriggered)
        {
            isOnClimbEventTriggered = true; 
            Debug.Log("OnLedgeClimb");
            OnLedgeClimb?.Invoke();
        }

        if (moveInput.y < 0 && !isOnReleaseEventTriggered)
        {
            isOnReleaseEventTriggered = true;
            Debug.Log("OnLedgeRelease");
            OnLedgeRelease?.Invoke();
            rb.gravityScale = gravityScale;
            isOnClimbEventTriggered = false;
            isOnHangEventTriggered = false;
            Invoke(nameof(AllowLedgeGrab), ReleaseThresholdTime);
        }
        if (moveInput.y == 0)
        {
            isOnClimbEventTriggered = false;
            isOnReleaseEventTriggered = false;
        }
    }

    private void AllowLedgeGrab()  => canGrabLedge = true;

    private void OnClimbAnimationEnded()
    {
        transform.position = endClimbPosition;
        rb.gravityScale = gravityScale;
        isOnClimbEventTriggered = false;
        isOnHangEventTriggered = false;
        canGrabLedge = true;
    }

    private void OnDrawGizmos()
    {
        if(isLedgeDetected) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector2(LedgeDetector0.position.x, LedgeDetector0.position.y), 
            BoxcastSize);
        Gizmos.DrawWireSphere(LedgeDetector1.position, LedgeDetectorRadius);
    }
}
