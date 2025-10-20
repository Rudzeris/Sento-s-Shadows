using Assets.Scripts.Features.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float gravity = -9.81f;

        private CharacterController _controller;
        private IInputHandler _input;

        private float _verticalVelocity;

        [Inject]
        public void Construct(IInputHandler input)
        {
            _input = input;
        }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector2 move = _input.Move;
            Vector3 direction = new Vector3(move.x, 0, move.y);

            Vector3 velocity = transform.TransformDirection(direction) * moveSpeed;
            
            if (_controller.isGrounded)
            {
                if (_verticalVelocity < 0)
                    _verticalVelocity = -1f;

                if (_input.JumpPressed)
                {
                    _verticalVelocity = jumpForce;
                }
            }

            // гравитация
            _verticalVelocity += gravity * Time.deltaTime;
            velocity.y = _verticalVelocity;

            _controller.Move(velocity * Time.deltaTime);
        }
    }
}