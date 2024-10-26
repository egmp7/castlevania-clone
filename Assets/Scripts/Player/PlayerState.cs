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

    public bool IsIdleing
    {
        get { return isIdling; }
        set 
        { 
            isIdling = value;
            isWalking = false;
            isRunning = false;
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
        }
    }

    public bool IsJumping
    {
        get { return isJumping; }
        set 
        { 
            isJumping = value;
            isOnGround = false;
        }
    }

    public bool IsOnGround
    {
        get { return isOnGround; }
        set
        { 
            isOnGround = value; 
            isJumping = false;
        }
    }

    public bool IsTouchingWall
    {
        get { return isTouchingWall; }
        set
        { isTouchingWall = value; }
    }

    public bool IsFacingRight
    {
        get { return isFacingRight; }
        set
        {
            isFacingRight = value;
        }
    }
}
