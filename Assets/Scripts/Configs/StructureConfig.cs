using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "StructureConfig", menuName = "Configs/StructureConfig")]
    public class StructureConfig : ScriptableObject
    {
        [Header("Привязка мини-игры")]
        public MiniGameConfig miniGameConfig;

    }
}
