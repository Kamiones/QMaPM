
using System.Collections.Generic;

public abstract class PlayerState
{
    protected PlayerController m_Controller;
    public List<Transition> Transitions = new List<Transition>();

    public PlayerState(PlayerController controller)
    {
        m_Controller = controller;
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnFinish();
}