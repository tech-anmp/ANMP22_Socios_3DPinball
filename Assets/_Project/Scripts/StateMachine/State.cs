using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Header("State Base")]
    [SerializeField]
    protected State m_NextState;

    public StateMachine StateMachine { get; set; }

    public abstract void Enter(State from);
    public abstract void Exit(State to);
    public abstract void Tick();

    public abstract string GetName();
}