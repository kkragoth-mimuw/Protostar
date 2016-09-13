using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    /* Classes */
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
        
    /* Constants */
    public float timeBetweenWaves = 5f;

    /* Enums */
    public enum GameState { NotStarted = 0, Playing, Paused, GameOver };
    public enum SpawnState { SPAWNING = 0, WAITING, COUNTING };

    /* Public variables */
    public Wave[] waves;
    public float waveCountdown;
    private float searchCountdown = 1f;
    public Transform[] spawnPoints;


    /* Private variables */
    private SpawnState state = SpawnState.COUNTING;
    private GameState gameState;
    private GameObject eventSystem;
    private GameGUI gui;

    private int nextWave = 0;

    private int score;
    private int highScore;

    /* Prefabs */

    /* Methods */
    void Start()
    {
        waveCountdown = timeBetweenWaves;
        gui = GameObject.FindGameObjectWithTag("Gui").GetComponent<GameGUI>();
    }
	
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                // Begin new wave;
                WaveCompleted();

            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }

        }
        else
        {
            waveCountdown -= Time.deltaTime;
            gui.SetNextWaveInText((waveCountdown >= 0) ? waveCountdown : 0);
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        gui.SetNextWaveInVisible(false);
        gui.SetWaveStatusText("enemies spawning...");

        state = SpawnState.SPAWNING;
        
        for (int i = 0; i <_wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        gui.SetWaveCompletedVisible(false);
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            return GameObject.FindGameObjectWithTag(Tags.ENEMY) != null;
    
        }
        else
        {
            return true;
        }
    }

    void WaveCompleted()
    {
        gui.SetWaveStatusText("WAVE COMPLETED");
        gui.SetWaveCompletedVisible(true);
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        gui.SetNextWaveInText(waveCountdown);
        gui.SetNextWaveInVisible(true);

        nextWave++;

        if (nextWave >= waves.Length)
        {
            nextWave = 0;
        }

    }
}

