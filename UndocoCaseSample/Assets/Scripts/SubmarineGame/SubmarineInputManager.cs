using UnityEngine;

public class SubmarineInputManager : MonoBehaviour
{
    private SubmarineInputActions _submarineInputActions;
    
        public Vector2 moveInput { get; private set; }
    
        private void Awake()
        {
            _submarineInputActions = new SubmarineInputActions();
    
            RegisterInputActions();
        }
    
        private void OnEnable()
        {
            _submarineInputActions.Enable();
        }
    
        private void OnDisable()
        {
            _submarineInputActions.Disable();
        }
    
        private void RegisterInputActions()
        {
            _submarineInputActions.Submarine.Move.performed += context => moveInput = context.ReadValue<Vector2>();
            _submarineInputActions.Submarine.Move.canceled += context => moveInput = Vector2.zero;
        }
}
