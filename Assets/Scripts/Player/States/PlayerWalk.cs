using UnityEngine;

public class PlayerWalk : PlayerState
{
    public PlayerWalk(PlayerController controller) : base(controller)
    {
        Transition toAsk = new Transition(
            isValid: () => Input.GetKeyDown(KeyCode.Q) && m_Controller.IsNPCInFront() && m_Controller.PlayerManager.movementController.IsGrounded,
            getNextState: () => m_Controller.AskState
        );
        Transitions.Add(toAsk);

        Transition toInventory = new Transition(
            isValid: () => Input.GetKeyDown(KeyCode.Tab), 
            getNextState: () => m_Controller.InventoryState
        );
        Transitions.Add(toInventory);
    }

    public override void OnStart()
    {
        Debug.Log("PlayerWalk: OnStart");
        m_Controller.PlayerManager.EnableInput(true);
    }

    public override void OnUpdate()
    {
    }

    public override void OnFinish()
    {
        Debug.Log("PlayerWalk: OnFinish");
    }
}