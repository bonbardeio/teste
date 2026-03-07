using UnityEngine;
using UnityEngine.Events;

namespace MobileFPS.GameModes
{
    /// <summary>
    /// Modo missão: objetivos de eliminação e captura de área.
    /// </summary>
    public class MissionModeManager : MonoBehaviour
    {
        [Header("Objetivo: Eliminar")]
        [SerializeField] private int targetKills = 20;

        [Header("Objetivo: Capturar Área")]
        [SerializeField] private float captureTimeRequired = 10f;

        public UnityEvent OnMissionCompleted;

        private int currentKills;
        private float captureTimer;
        private bool missionDone;

        public void RegisterKill()
        {
            if (missionDone)
                return;

            currentKills++;
            ValidateMission();
        }

        public void AddCaptureProgress(float delta)
        {
            if (missionDone)
                return;

            captureTimer = Mathf.Clamp(captureTimer + delta, 0f, captureTimeRequired);
            ValidateMission();
        }

        private void ValidateMission()
        {
            if (currentKills >= targetKills && captureTimer >= captureTimeRequired)
            {
                missionDone = true;
                OnMissionCompleted?.Invoke();
            }
        }
    }
}
