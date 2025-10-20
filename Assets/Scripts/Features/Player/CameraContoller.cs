namespace Assets.Scripts.Features.Player
{
    using Assets.Scripts.Features.Input;
    using UnityEngine;
    using Zenject;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float sensitivityX = 2f;
        [SerializeField] private float sensitivityY = 2f;

        private float _rotationX;

        private IInputHandler _input;

        [Inject]
        public void Contruct(IInputHandler input)
        {
            _input = input;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
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
