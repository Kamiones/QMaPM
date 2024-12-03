using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerManager PlayerManager { get; private set; }

    #region States
    public PlayerWalk WalkState { get; private set; }
    public PlayerAsk AskState { get; private set; }
    public PlayerInventory InventoryState { get; private set; }

    private PlayerState m_CurrentState;
    #endregion
    [SerializeField] InventoryManager inventoryManager;
    private float interactionDistance = 3f; // Distancia para detectar NPC

    public bool IsNPCInFront()
    {
        RaycastHit hit;
        // Offset para evitar que el raycast detecte al propio jugador
        Vector3 origin = transform.position + transform.forward * 0.1f; 
        if (Physics.Raycast(origin, transform.forward, out hit, interactionDistance))
        {
            bool IsNPC = hit.collider.CompareTag("NPC");
            Debug.Log($"IsNPC: {IsNPC}, Hit: {hit.collider.name}");
            return IsNPC;
        }
        Debug.Log("IsNPC: false, No hit");
        return false;
    }

    private void Awake()
    {
        PlayerManager = GetComponent<PlayerManager>();

        WalkState = new PlayerWalk(this);
        AskState = new PlayerAsk(this);
        InventoryState = new PlayerInventory(this);

        // Remover inicialización de LayerMask
        // npcLayerMask = LayerMask.GetMask("NPC");

        // Modificar la transición para verificar primero la pulsación de la tecla E
        Transition toAsk = new Transition(
            isValid: () => Input.GetKeyDown(KeyCode.Q) && IsNPCInFront() && PlayerManager.movementController.IsGrounded,
            getNextState: () => AskState
        );
        WalkState.Transitions.Add(toAsk);

        StartStateMachine();
    }

    private void Update()
    {
        foreach (var transition in m_CurrentState.Transitions)
        {
            if (transition.IsValid())
            {
                m_CurrentState.OnFinish();
                m_CurrentState = transition.GetNextState();
                m_CurrentState.OnStart();
                break;
            }
        }
        m_CurrentState.OnUpdate();
    }

    private void StartStateMachine()
    {
        m_CurrentState = WalkState;
        m_CurrentState.OnStart();
    }

    public void ShowDialogue()
    {
        // Mostrar interfaz de diálogo
        // ...código para mostrar diálogo...
    }

    public void HideDialogue()
    {
        // Ocultar interfaz de diálogo
        // ...código para ocultar diálogo...
    }

    public void ShowInventory()
    {
        inventoryManager.ShowInventory();
    }

    public void HideInventory()
    {
        inventoryManager.HideInventory();
    }
}