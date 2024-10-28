using UnityEngine;

public class StateController : MonoBehaviour
{
    [HideInInspector] public State currentState;
    [HideInInspector] public Rigidbody2D rb;

    public IdleState idleState = new();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(idleState);
    }

    void Update()
    {
        currentState?.OnStateUpdate();
    }

    public void ChangeState(State newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter(this);
    }
}