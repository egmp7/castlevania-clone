using UnityEngine;

public abstract class State
{
    protected StateController sc;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected bool grounded;
    protected float xInput;
    protected float yInput;

    public void OnStateEnter(StateController stateController)
    {
        Debug.Log("OnStateEnter State");
        sc = stateController;
        OnEnter();
    }

    protected virtual void OnEnter()
    {
        Debug.Log("OnEnter State");
    }

    public void OnStateUpdate()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
    }

    public void OnStateExit()
    {
        OnExit();
    }

    protected virtual void OnExit()
    {
    }
}