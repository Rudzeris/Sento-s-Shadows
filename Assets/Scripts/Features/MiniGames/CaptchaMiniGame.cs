using Assets.Scripts.Signals;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.MiniGames
{
    public class CaptchaMiniGame : MonoBehaviour, IMiniGame
    {
        [SerializeField] private TextMeshProUGUI _captchaText;
        [SerializeField] private TMP_InputField _inputText;
        [SerializeField] private TextMeshProUGUI _toolTipText;
        [SerializeField, Min(3)] private int _countCharsInCaptcha = 4;
        [SerializeField]
        private string _captchaChars =
            "1234567890!@#$%^&*()-_=+qwertyuiop[]asdfghjkl;'zxcvbnm,./" +
            "QWERTYUIOP{}ASDFGHJKL:ZXCVBNM<>";

        private SignalBus _signalBus;
        private CancellationTokenSource _cts;
        private string _currentCaptcha;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void StartGame()
        {
            _signalBus.Fire(new SelectUISignal(true));
            gameObject.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            ResetCaptcha();
        }

        public void OnSuccess()
        {
            _signalBus.Fire(new MiniGameCompletedSignal(true));
            ExitMiniGame();
        }

        public void OnFail()
        {
            _signalBus.Fire(new MiniGameCompletedSignal(false));
        }

        private void ExitMiniGame()
        {
            _signalBus.Fire(new SelectUISignal(false));

            gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            CancelCaptchaTask();
        }

        private void CancelCaptchaTask()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private void ResetCaptcha()
        {
            CancelCaptchaTask();
            _toolTipText.text = "";
            _inputText.text = "";
            _cts = new CancellationTokenSource();
            CreateCaptcha(_cts.Token).Forget();
        }

        private async UniTaskVoid CreateCaptcha(CancellationToken token)
        {
            try
            {
                _captchaText.text = new string('#', _countCharsInCaptcha);
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);

                string captcha = "";
                for (int i = 0; i < _countCharsInCaptcha; i++)
                {
                    captcha += _captchaChars[UnityEngine.Random.Range(0, _captchaChars.Length)];
                    _captchaText.text = captcha + new string('#', _countCharsInCaptcha - i - 1);

                    await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: token);
                }

                _inputText.text = "";
                _captchaText.text = captcha;
                _currentCaptcha = captcha;
            }
            catch (OperationCanceledException)
            {
                // Игнорируем отмену
            }
        }

        public void OnResetClick()
        {
            ResetCaptcha();
        }

        public void OnExitClick()
        {
            OnFail();
            ExitMiniGame();
        }

        public void OnDoneClick()
        {
            if (string.Equals(_inputText.text, _currentCaptcha, StringComparison.Ordinal))
            {
                OnSuccess();
            }
            else
            {
                _toolTipText.text = "Неверно, попробуйте снова!";
                OnFail();
            }
        }
    }
}