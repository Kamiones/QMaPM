using UnityEngine;

public class PlayerInventory : PlayerState
{
    public PlayerInventory(PlayerController controller) : base(controller)
    {
        Transition toWalk = new Transition(
            isValid: () => Input.GetKeyDown(KeyCode.Tab),
            getNextState: () => m_Controller.WalkState
        );
        Transitions.Add(toWalk);
    }

    public override void OnStart()
    {
        Debug.Log("PlayerInventory: OnStart");
        m_Controller.PlayerManager.EnableInput(false);
        m_Controller.ShowInventory();
        
    }

    public override void OnUpdate()
    {
        // ...lógica de interacción con el inventario...
    }

    public override void OnFinish()
    {
        Debug.Log("PlayerInventory: OnFinish");
        m_Controller.PlayerManager.EnableInput(true);
        m_Controller.HideInventory();
        
    }
}