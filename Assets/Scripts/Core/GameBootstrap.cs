using UnityEngine;
using UnityEngine.SceneManagement;

namespace MobileFPS.Core
{
    /// <summary>
    /// Inicializa o jogo com configurações recomendadas para Android.
    /// Anexe este script em um GameObject da cena de menu inicial.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Performance Android")]
        [SerializeField] private int targetFrameRate = 60;
        [SerializeField] private bool forceSingleThreadedRendering = true;

        private void Awake()
        {
            // Mantém FPS estável em dispositivos móveis intermediários.
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.vSyncCount = 0;

            // Em alguns Androids ajuda a reduzir stutter em cenas leves.
            if (forceSingleThreadedRendering)
            {
                QualitySettings.maxQueuedFrames = 2;
            }
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
