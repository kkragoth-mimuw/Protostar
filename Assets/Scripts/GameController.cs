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

    /* Private variables */
    private SpawnState state = SpawnState.COUNTING;
    private GameState gameState;
    private GameObject eventSystem;
    private GameGUI gui;

    private GameObject[] spawnPoints;

    private int waveMultipler = 1;
    private int nextWave = 0;

    private int score;
    private int highScore;

    /* Prefabs */

    /* Methods */
    void Start()
    {
        gameState = GameState.NotStarted;

        gui = GameObject.FindGameObjectWithTag(Tags.GUI).GetComponent<GameGUI>();
        gui.ShowMenuPanel();
        spawnPoints = GameObject.FindGameObjectsWithTag(Tags.SPAWNER);

        Time.timeScale = 0.0f;
    }
	
    public void StartGame()
    {
        gameState = GameState.Playing;
        waveCountdown = timeBetweenWaves;
        Time.timeScale = 1.0f;

        // Spawn Player
    }

    public void PauseGame()
    {
        gameState = GameState.Paused;
        Time.timeScale = 0.0f;
    }


    void Update()
    {
        if (gameState == GameState.Playing)
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
    }

    IEnumerator SpawnWave(Wave wave)
    {
        gui.SetNextWaveInVisible(false);
        gui.SetWaveStatusText("fight!");
        gui.AdvanceWaveCount();

        state = SpawnState.SPAWNING;
        
        for (int i = 0; i < waveMultipler * wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);

            gui.SetWaveCompletedVisible(false);
        }

        state = SpawnState.WAITING;


        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        GameObject go = Instantiate(_enemy, sp.position, sp.rotation) as GameObject;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            Debug.Log(GameObject.FindGameObjectWithTag(Tags.ENEMY));
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
            waveMultipler++;
        }

        AIController.minVelocity += 50;
        AIController.maxVelocity += 50;

    }
}

