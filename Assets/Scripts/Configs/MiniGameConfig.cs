using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "MiniGameConfig", menuName = "Configs/MiniGameConfig")]
    public class MiniGameConfig : ScriptableObject
    {
        [Header("Основыне параметры")]
        public string id;
        public GameObject prefab;

        [Header("Настройки")]
        public int difficultyLevel = 1;
        public float timeLimit = 0f;

        [Header("Описание"), TextArea]
        public string description;
    }
}
