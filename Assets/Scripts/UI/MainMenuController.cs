using MobileFPS.Core;
using UnityEngine;

namespace MobileFPS.UI
{
    /// <summary>
    /// Menu inicial: Jogar, Arsenal, Configurações e Sair.
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameBootstrap bootstrap;

        public void Play()
        {
            bootstrap.LoadScene("CityDestroyed");
        }

        public void OpenArsenal()
        {
            bootstrap.LoadScene("Arsenal");
        }

        public void OpenSettings()
        {
            bootstrap.LoadScene("Settings");
        }

        public void Exit()
        {
            bootstrap.QuitGame();
        }
    }
}
