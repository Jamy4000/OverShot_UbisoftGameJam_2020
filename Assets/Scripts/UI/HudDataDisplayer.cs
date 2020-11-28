using UnityEngine;
using UbiJam.Gameplay;
using UbiJam.Events;
using System.Collections;

namespace UbiJam.UI
{
    /// <summary>
    /// Handle the data displayed during the game, like the speed or the current lap
    /// </summary>
    public class HudDataDisplayer : MonoBehaviour
    {
        [Header("Timer Texts")]
        /// <summary>
        /// Simple reference to the countdown text at the start of the game
        /// </summary>
        [SerializeField] private TMPro.TextMeshProUGUI _countdownText;
        /// <summary>
        /// Simple reference to the time text at the start of the game
        /// </summary>
        [SerializeField] private TMPro.TextMeshProUGUI _timerText;
        
        private GameManager _gameManager;
        private GameTimer _gameTimer;

        private void Start()
        {
            _gameTimer = GameTimer.Instance;
            _gameManager = GameManager.Instance;

            // Update game timer
            _timerText.text = "Time Remaining: " + GameSettings.Instance.GameTime.ToString("N1") + " sec.";
            _countdownText.text = "Prepare yourself...";
            OnGameEnded.Listeners += DestroyUI;
            OnGameStarted.Listeners += DisableCountdownText;
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
                _timerText.text = "Time Remaining: " + _gameTimer.GameTimeSeconds.ToString("N1") + " sec.";
            }
        }

        private void OnDestroy()
        {
            OnGameEnded.Listeners -= DestroyUI;
            OnGameStarted.Listeners -= DisableCountdownText;
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
            Destroy(gameObject);
        }
    }
}