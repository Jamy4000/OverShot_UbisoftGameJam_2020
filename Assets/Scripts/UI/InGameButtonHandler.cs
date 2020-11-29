using UnityEngine;
using UnityEngine.SceneManagement;

namespace UbiJam.UI
{
    public class InGameButtonHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _controlPanel;
        [SerializeField]
        private GameObject _mainMenu;

        public void StartGame()
        {
            SceneManager.LoadScene("Main");
        }

        public void Restart()
        {
            SceneManager.LoadScene(gameObject.scene.buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void OpenControlPanel()
        {
            _controlPanel.SetActive(true);
            _mainMenu.SetActive(false);
        }

        public void CloseControlPanel()
        {
            _controlPanel.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }
}
