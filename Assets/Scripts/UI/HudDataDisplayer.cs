using UnityEngine;
using UbiJam.Gameplay;
using UbiJam.Events;
using System.Collections;
using UbiJam.Player;
using UbiJam.Inputs;
using System;

namespace UbiJam.UI
{
    /// <summary>
    /// Handle the data displayed during the game, like the speed or the current lap
    /// </summary>
    public class HudDataDisplayer : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _countdownText;
        [SerializeField] private TMPro.TextMeshProUGUI _timerText;
        [SerializeField] private GameObject _endGameGO;
        [SerializeField] private TMPro.TextMeshProUGUI _finalScoreText;
        [SerializeField] private TMPro.TextMeshProUGUI _pointText;
        [SerializeField] private GameObject _pauseMenu;

        private GameManager _gameManager;
        private GameTimer _gameTimer;

        private void Awake()
        {
            OnGameEnded.Listeners += DestroyUI;
            OnGameStarted.Listeners += DisableCountdownText;
        }

        private void Start()
        {
            _gameTimer = GameTimer.Instance;
            _gameManager = GameManager.Instance;
            _timerText.text = "Time Remaining: " + GameSettings.Instance.GameTime.ToString("N1") + " sec.";
            _countdownText.text = "Prepare yourself...";

            InputManager.Instance.SlingshotInputs.OnPauseAction.started += UpdatePauseMenu;
            InputManager.Instance.SlingshotInputs.OnPauseAction.canceled += UpdatePauseMenu;
            InputManager.Instance.CharacterInputs.OnPauseAction.started += UpdatePauseMenu;
            InputManager.Instance.CharacterInputs.OnPauseAction.canceled += UpdatePauseMenu;

            _endGameGO.SetActive(false);
        }

        private void Update()
        {
            if (!_gameManager.IsStarted)
            {
                DisplayCountdown();
            }
            else if (_gameManager.IsRunning)
            {
                // Update game timer
                _timerText.text = "Time Remaining: " + _gameTimer.RemainingGameTimeSeconds.ToString("N1") + " sec.";
                _pointText.text = "Neighbors saved: " + _gameManager.CurrentScore;
            }
        }

        private void OnDestroy()
        {
            OnGameEnded.Listeners -= DestroyUI;
            OnGameStarted.Listeners -= DisableCountdownText;
        }

        private void UpdatePauseMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        }

        private void DisplayCountdown()
        {
            _countdownText.text = GameTimer.Instance.GetCountdownText;
        }

        private void DisableCountdownText(OnGameStarted _)
        {
            _countdownText.text = "GO GO GO GO";
            StartCoroutine(WaitForTwoSeconds());

            IEnumerator WaitForTwoSeconds()
            {
                yield return new WaitForSeconds(2.0f);
                _countdownText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// We don't really need the hub once the race is done, so we gonna destroy it
        /// </summary>
        private void DestroyUI(OnGameEnded _)
        {
            this.enabled = false;
            _finalScoreText.text = _pointText.text;
            _endGameGO.SetActive(true);
        }
    }
}