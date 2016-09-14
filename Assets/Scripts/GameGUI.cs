using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CnControls;

public class GameGUI : MonoBehaviour
{

    public bool gameRunning = false;
    public bool playAudio;
    public bool swapJoystick;
    
    /* Public UI objects */
    public GameObject MenuPanel;
    public GameObject QuitPrompt;
    public GameObject GameOverPanel;

    public Text text;
    public GameObject WaveCompleted;
    public Text WaveCompletedText;
    public Text PlayResume;
    public Text GameOverText;

    public GameObject NextWaveIn;
    public Text NextWaveInText;

    public Text WaveCount;

    /* Private variables */

    private string movementAxisX = Tags.H; //"Horizontal";
    private string movementAxisY = Tags.V; //"Vertical";
    private string fireAxisX = Tags.MX; //"Mouse X";
    private string fireAxisY = Tags.MY; //"Mouse Y";

    private Android android;

    private GameController gameController;
    private PlayerController playerController;
    private GameObject player;

    private bool hasGameEnded;
    private bool isGamePaused = false;
    private int waveCount = 0;

    public GameObject missile;

    private float time = 0.0f;

    /* Methods */
    void Start()
    {
        playAudio = true;
        swapJoystick = false;
        gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
        //playerController = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }
	
    void Update()
    {
        if (!MenuPanel.active)
            HandleJoystick();
    }

    public void ResetWaveCount()
    {
        waveCount = 0;
        WaveCount.text = "WAVE: " + waveCount.ToString();
    }

    public void AdvanceWaveCount()
    {
        waveCount++;
        WaveCount.text = "WAVE: " + waveCount.ToString();
    }

    public void SetWaveCompletedVisible(bool visible)
    {
        WaveCompleted.SetActive(visible);
    }

    public void SetWaveStatusText(string status)
    {
        WaveCompletedText.text = status;
    }

    public void SetNextWaveInVisible(bool visible)
    {
        NextWaveIn.SetActive(visible);
    }

    public void SetNextWaveInText(float time)
    {
        NextWaveInText.text = "next wave in: " + time.ToString("#.00");
    }
    /* Private methods */

    private void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    private void resumeTime()
    {
        Time.timeScale = 1.0f;
    }

    private void HandleJoystick()
    {
        
        Vector3 movement = new Vector3(
            CnInputManager.GetAxis(movementAxisX),
            0f,
            CnInputManager.GetAxis(movementAxisY)
        );

        Vector3 rotation = new Vector3(0,
            Mathf.Atan2(CnInputManager.GetAxis(fireAxisX),
                CnInputManager.GetAxis(fireAxisY)
            ) * Mathf.Rad2Deg,
            0
        );

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
            return;
        }

        PlayerController pc = player.GetComponent<PlayerController>();
        pc.Move(movement);
        pc.Rotate(rotation);
       
        if ((CnInputManager.GetAxis(fireAxisX) != 0) || (CnInputManager.GetAxis(fireAxisY) != 0))
        {
            pc.Shoot();
        }
    }

    private void DrawPlayerStats()
    {
        
    }

    public void Play()
    {
        CloseMenuPanel();
        if (!gameRunning)
        {
            PlayResume.text = "RESUME";
            gameController.StartGame();
            WaveCount.text = "WAVE: " + waveCount.ToString(); 
        }

        gameController.ResumeGame();
        gameRunning = true;
    }

    public void Retry()
    {
        WaveCount.text = "";
        GameOverPanel.SetActive(false);
        gameController.Retry();
        waveCount = 0;
    }

    public void PauseMenu()
    {
        ShowMenuPanel();
        gameController.PauseGame();
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ShowQuitPrompt()
    {
        QuitPrompt.SetActive(true);
    }

    public void CloseQuitPrompt()
    {
        QuitPrompt.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
    }

    public void CloseMenuPanel()
    {
        MenuPanel.SetActive(false);
    }

    public void AudioChanged(bool isclick)
    {
        if (isclick)
        {
            GetComponent<AudioSource>().volume = 1.0f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }

    }

    public void SwapJoystickChanged(bool isclick)
    {
        if (!isclick)
        {
            movementAxisX = Tags.H;  //"Horizontal";
            movementAxisY = Tags.V;  // "Vertical";
            fireAxisX = Tags.MX;     // "Mouse X";
            fireAxisY = Tags.MY;     //"Mouse Y";
        }
        else
        {
            movementAxisX = Tags.MX;
            movementAxisY = Tags.MY;
            fireAxisX = Tags.H;
            fireAxisY = Tags.V;
        }
    }

    public void GameOver()
    {
        GameOverText.text = "GAME OVER! \n" +
            "YOU SURVIVED " + (((waveCount - 1) > 0) ? waveCount - 1 : 0).ToString() + " WAVES";
        GameOverPanel.SetActive(true);
        WaveCount.text = "";
    }
}

