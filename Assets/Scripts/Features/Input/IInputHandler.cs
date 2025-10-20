using UnityEngine;

namespace Assets.Scripts.Features.Input
{
    public interface IInputHandler
    {
        public Vector2 Move { get; }
        public Vector2 Look { get; }
        public bool Interact { get; }
    }
}
