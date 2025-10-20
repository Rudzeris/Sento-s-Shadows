using Assets.Scripts.Features.Input;
using Assets.Scripts.Features.Sctructures;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 3f;
    
        private IInputHandler _input;

        [Inject]
        public void Contruct(IInputHandler input)
        {
            _input = input;
        }

        private void Update()
        {
            if (_input.Interact)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance))
                {
                    if (hit.collider.TryGetComponent(out Structure structure))
                    {
                        structure.Interact();
                    }
                }
            }
        }
    }
}
