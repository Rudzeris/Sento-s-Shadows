namespace Assets.Scripts.Features.Player
{
    using Assets.Scripts.Features.Input;
    using Assets.Scripts.Signals;
    using UnityEngine;
    using Zenject;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float sensitivityX = 2f;
        [SerializeField] private float sensitivityY = 2f;

        private SignalBus _signalBus;

        private float _rotationX;

        private IInputHandler _input;

        private bool isEnable = true;

        [Inject]
        public void Contruct(IInputHandler input, SignalBus signalBus)
        {
            _input = input;
            _signalBus = signalBus;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _signalBus.Subscribe<SelectUISignal>(OnSelectedUI);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<SelectUISignal>(OnSelectedUI);
        }

        private void OnSelectedUI(SelectUISignal signal)
        {
            isEnable = !signal.IsSelected;
        }

        private void LateUpdate()
        {
            if (!isEnable) return;

            Vector2 look = _input.Look;

            float mouseX = look.x * sensitivityX;
            float mouseY = look.y * sensitivityY;

            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, -80f, 80f);

            transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }
    }
}
