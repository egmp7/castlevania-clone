using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private bool isIdling;
    private bool isWalking;
    private bool isRunning;
    private bool isJumping;
    private bool isOnGround;
    private bool isTouchingWall;
    private bool isFacingRight;

    public bool IsIdling
    {
        get { return isIdling; }
        set
        {
            isIdling = value;
            isWalking = false;
            isRunning = false;
            Debug.Log("IsIdling set to: " + isIdling);
        }
    }

    public bool IsRunning
    {
        get { return isRunning; }
        set
        {
            isRunning = value;
            isIdling = false;
            isWalking = false;
            Debug.Log("IsRunning set to: " + isRunning);
        }
    }

    public bool IsWalking
    {
        get { return isWalking; }
        set
        {
            isWalking = value;
            isIdling = false;
            isRunning = false;
            Debug.Log("IsWalking set to: " + isWalking);
        }
    }

    public bool IsJumping
    {
        get { return isJumping; }
        set
        {
            isJumping = value;
            isOnGround = false;
            Debug.Log("IsJumping set to: " + isJumping);
        }
    }

    public bool IsOnGround
    {
        get { return isOnGround; }
        set
        {
            isOnGround = value;
            isJumping = false;
            Debug.Log("IsOnGround set to: " + isOnGround);
        }
    }

    public bool IsTouchingWall
    {
        get { return isTouchingWall; }
        set
        {
            isTouchingWall = value;
            Debug.Log("IsTouchingWall set to: " + isTouchingWall);
        }
    }

    public bool IsFacingRight
    {
        get { return isFacingRight; }
        set
        {
            isFacingRight = value;
            Debug.Log("IsFacingRight set to: " + isFacingRight);
        }
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(10, 10, 200, 20), "IsIdling: " + isIdling);
        GUI.Label(new Rect(10, 30, 200, 20), "IsWalking: " + isWalking);
        GUI.Label(new Rect(10, 50, 200, 20), "IsRunning: " + isRunning);
        GUI.Label(new Rect(10, 70, 200, 20), "IsJumping: " + isJumping);
        GUI.Label(new Rect(10, 90, 200, 20), "IsOnGround: " + isOnGround);
        GUI.Label(new Rect(10, 110, 200, 20), "IsTouchingWall: " + isTouchingWall);
        GUI.Label(new Rect(10, 130, 200, 20), "IsFacingRight: " + isFacingRight);
    }
}
