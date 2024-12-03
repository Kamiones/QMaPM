using UnityEngine;

public class PlayerAsk : PlayerState
{
    public PlayerAsk(PlayerController controller) : base(controller)
    {
        Transition toWalk = new Transition(
            isValid: () => Input.GetKeyDown(KeyCode.Q) && m_Controller.IsNPCInFront() && m_Controller.PlayerManager.movementController.IsGrounded,
            getNextState: () => m_Controller.WalkState
        );
        Transitions.Add(toWalk);
    }

    public override void OnStart()
    {
        // Remover la verificación adicional
        /*
        if (!m_Controller.IsNPCInFront())
        {
            m_Controller.HideDialogue();
            m_Controller.WalkState.OnStart();
            return;
        }
        */

        Debug.Log("PlayerAsk: OnStart");
        m_Controller.PlayerManager.EnableInput(false);
        // ...mostrar interfaz de diálogo...
    }

    public override void OnUpdate()
    {
        // Lógica de interacción con NPCs y recopilación de pistas
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ...código para interactuar con NPCs...
        }
    }

    public override void OnFinish()
    {
        Debug.Log("PlayerAsk: OnFinish");
        m_Controller.PlayerManager.EnableInput(true);
        // ...ocultar interfaz de diálogo...
    }
}