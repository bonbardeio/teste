using System.Collections;
using UnityEngine;

namespace MobileFPS.GameModes
{
    /// <summary>
    /// Modo sobrevivência: gera ondas infinitas de inimigos.
    /// </summary>
    public class SurvivalModeManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float timeBetweenWaves = 8f;

        private int currentWave = 1;

        private void Start()
        {
            StartCoroutine(WaveLoop());
        }

        private IEnumerator WaveLoop()
        {
            while (true)
            {
                int enemyCount = 3 + currentWave;
                for (int i = 0; i < enemyCount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(0.4f);
                }

                currentWave++;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        private void SpawnEnemy()
        {
            if (spawnPoints.Length == 0 || enemyPrefab == null)
                return;

            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, point.position, point.rotation);
        }
    }
}
