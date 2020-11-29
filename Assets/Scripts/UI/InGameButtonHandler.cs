using UnityEngine;
using UnityEngine.SceneManagement;

namespace UbiJam.UI
{
    public class InGameButtonHandler : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(gameObject.scene.buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
