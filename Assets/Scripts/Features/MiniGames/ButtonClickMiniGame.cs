using Assets.Scripts.Signals;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.MiniGames
{
    public class ButtonClickMiniGame : MonoBehaviour, IMiniGame
    {
        [SerializeField] private Button[] buttons;
        [SerializeField] private int[] correctSequence;

        [Header("Цвета")]
        [SerializeField] private Color sequenceColor = Color.yellow;
        [SerializeField] private Color successColor = Color.green;
        [SerializeField] private Color failColor = Color.red;

        [Header("Настройки анимации")]
        [SerializeField] private float changeColorDuration = 0.3f;
        [SerializeField] private float waitChangeTime = 0.5f;
        [SerializeField] private float waitBetweenButtons = 1f;

        private int _currentIndex = 0;
        private SignalBus _signalBus;

        private Color[] _originalColors;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _originalColors = new Color[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                _originalColors[i] = buttons[i].GetComponent<Image>().color;
            }

            gameObject.SetActive(false);
        }

        public void StartGame()
        {
            _signalBus.Fire<SelectUISignal>(new SelectUISignal(true));

            _currentIndex = 0;
            foreach (var button in buttons)
            {
                button.interactable = false;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnButtonClick(button).Forget());
            }

            // Включаем объект и курсор
            gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            ViewSequence().Forget();
        }

        private async UniTaskVoid ViewSequence()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            for (int i = 0; i < correctSequence.Length; i++)
            {
                int index = correctSequence[i];
                Image targetImage = buttons[index].GetComponent<Image>();
                Color origColor = _originalColors[index];

                await targetImage.DOColor(sequenceColor, changeColorDuration).AsyncWaitForCompletion();
                await UniTask.Delay(TimeSpan.FromSeconds(waitChangeTime));
                await targetImage.DOColor(origColor, changeColorDuration).AsyncWaitForCompletion();

                await UniTask.Delay(TimeSpan.FromSeconds(waitBetweenButtons));
            }

            foreach (var button in buttons)
                button.interactable = true;
        }

        private async UniTaskVoid OnButtonClick(Button clicked)
        {
            int expectedIndex = correctSequence[_currentIndex];

            if (buttons[expectedIndex] == clicked)
            {
                await clicked.image.DOColor(successColor, changeColorDuration).AsyncWaitForCompletion();

                _currentIndex++;
                if (_currentIndex >= correctSequence.Length)
                    OnSuccess();
            }
            else
            {
                OnFail();
            }
        }

        public void OnSuccess()
        {
            Debug.Log("Мини-игра пройдена");
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = _originalColors[i];
            }
            _signalBus.Fire(new MiniGameCompletedSignal(true));

            // Скрываем игру и возвращаем курсор в режим игры
            gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _signalBus.Fire<SelectUISignal>(new SelectUISignal(false));
        }

        public void OnFail()
        {
            Debug.Log("Мини-игра провалена");

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = _originalColors[i];
            }

            _signalBus.Fire(new MiniGameCompletedSignal(false));
            _currentIndex = 0;

            // Скрываем игру и возвращаем курсор в режим игры
            gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _signalBus.Fire<SelectUISignal>(new SelectUISignal(false));
        }
    }
}