using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running,
        Success,
        Failure
    }

    [SerializeField] State state = State.Running;
    [SerializeField] private bool started;

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();

    public State Update()
    {
        // if the node has not started Start the Node.
        if (!started)
        {
            OnStart();
            started = true;
        }

        // set the state and run the nodes child update logic
        state = OnUpdate();

        if (state == State.Failure || state == State.Success)
        {
            OnStop();
            started = false;
        }

        return state;
    }

}
