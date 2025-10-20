using Assets.Scripts.Features.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.Player
{

    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private CharacterController _controller;

        private IInputHandler _input;

        [Inject]
        public void Contruct(IInputHandler input)
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

            if (direction.sqrMagnitude > 0.01f)
            {
                _controller.SimpleMove(transform.TransformDirection(direction) * moveSpeed);
            }
        }
    }
}
