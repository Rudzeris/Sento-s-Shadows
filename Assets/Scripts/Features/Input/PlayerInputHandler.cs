using UnityEngine;

namespace Assets.Scripts.Features.Input
{
    public class PlayerInputHandler : MonoBehaviour, IInputHandler
    {
        private PlayerInputActions _actions;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Interact { get; private set; }
        public bool JumpPressed { get; private set; }

        private void Awake()
        {
            _actions = new PlayerInputActions();

            Initialize();
        }

        private void Initialize()
        {
            _actions.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
            _actions.Player.Move.canceled += ctx => Move = Vector2.zero;

            _actions.Player.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();
            _actions.Player.Look.canceled += ctx => Look = Vector2.zero;

            _actions.Player.Interact.performed += ctx => Interact = true;
            _actions.Player.Interact.canceled += ctx => Interact = false;

            _actions.Player.Jump.performed += ctx => JumpPressed = true;
            _actions.Player.Jump.canceled += ctx => JumpPressed = false;
        }

        private void OnEnable() => _actions.Enable();
        private void OnDisable() => _actions.Disable();
    }
}
