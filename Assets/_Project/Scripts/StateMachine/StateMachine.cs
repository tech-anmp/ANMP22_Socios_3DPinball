using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    static public StateMachine Instance { get { return m_Instance; } }
    static protected StateMachine m_Instance;

    public State[] m_States;
    public State CurrentState {  get { if (m_StateStack.Count == 0) return null; return m_StateStack[m_StateStack.Count - 1]; } }

    protected List<State> m_StateStack = new List<State>();
    protected Dictionary<string, State> m_StateDict = new Dictionary<string, State>();

    protected void OnEnable()
    {
        m_Instance = this;

        // We build a dictionnary from state for easy switching using their name.
        m_StateDict.Clear();

        if (m_States.Length == 0)
            return;

        for(int i = 0; i < m_States.Length; ++i)
        {
            m_States[i].StateMachine = this;
            m_StateDict.Add(m_States[i].GetName(), m_States[i]);
        }

        m_StateStack.Clear();

        PushState(m_States[0].GetName());
    }

    protected void Update()
    {
        if(m_StateStack.Count > 0)
        {
            m_StateStack[m_StateStack.Count - 1].Tick();
        }
    }

    public void SwitchState(string newState)
    {
        State state = FindState(newState);
        if (state == null)
        {
            Debug.LogError("Can't find the state named " + newState);
            return;
        }

        m_StateStack[m_StateStack.Count - 1].Exit(state);
        state.Enter(m_StateStack[m_StateStack.Count - 1]);
        m_StateStack.RemoveAt(m_StateStack.Count - 1);
        m_StateStack.Add(state);
    }

	public State FindState(string stateName)
	{
		State state;
		if (!m_StateDict.TryGetValue(stateName, out state))
		{
			return null;
		}

		return state;
	}

    public void PopState()
    {
        if(m_StateStack.Count < 2)
        {
            Debug.LogError("Can't pop states, only one in stack.");
            return;
        }

        m_StateStack[m_StateStack.Count - 1].Exit(m_StateStack[m_StateStack.Count - 2]);
        m_StateStack[m_StateStack.Count - 2].Enter(m_StateStack[m_StateStack.Count - 2]);
        m_StateStack.RemoveAt(m_StateStack.Count - 1);
    }

    public void PushState(string name)
    {
        State state;
        if(!m_StateDict.TryGetValue(name, out state))
        {
            Debug.LogError("Can't find the state named " + name);
            return;
        }

        if (m_StateStack.Count > 0)
        {
            m_StateStack[m_StateStack.Count - 1].Exit(state);
            state.Enter(m_StateStack[m_StateStack.Count - 1]);
        }
        else
        {
            state.Enter(null);
        }
        m_StateStack.Add(state);
    }
}