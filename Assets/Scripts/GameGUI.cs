using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CnControls;

public class GameGUI : MonoBehaviour
{
    /* Constants */



    /* Public UI objects */
    public GameObject GUIPanel;
    public GameObject InfoPanel;

    public Text text;
    public GameObject WaveCompleted;
    public Text WaveCompletedText;

    public GameObject NextWaveIn;
    public Text NextWaveInText;

    /* Private variables */
    private GameController gameController;
    private PlayerController playerController;
    private GameObject player;

    private bool hasGameEnded;
    private bool isGamePaused = false;

    public GameObject missile;

    private float time = 0.0f;

    /* Methods */
    void Start()
    {
        //gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
        //playerController = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
    void Update()
    {
        HandleJoystick();
    }

    public void SetWaveCompletedVisible(bool visible)
    {
        //WaveCompleted.enabled = visible;
        WaveCompleted.SetActive(visible);
        //Debug.Log(visible)
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
            CnInputManager.GetAxis("Horizontal"),
            0f,
            CnInputManager.GetAxis("Vertical")
            );

        Vector3 rotation = new Vector3(0,
            Mathf.Atan2(CnInputManager.GetAxis("Mouse X"),
                CnInputManager.GetAxis("Mouse Y")
            ) * Mathf.Rad2Deg,
            0);

        //player.transform.Rotate(rotation * Time.deltaTime); //.rotation += 20 * rotation;
        //player.transform.localEulerAngles = rotation;
        player.transform.rotation = Quaternion.Euler(rotation);

        //text.text =  rotation.y.ToString();
        //text.text((String) rotation.y);
        //player.transform.position += 2.5f * movement * player.transform.forward;
        //player.transform.position += movement.y * transform.forward;
        //player.transform.position += movement.x * transform.right;
        player.transform.position += 7.5f * movement;
    
       
        if (time >= 0.15f)
        {
            if ((CnInputManager.GetAxis("Mouse X") != 0) || (CnInputManager.GetAxis("Mouse Y") != 0))
            {
                GameObject firedMissile;
                firedMissile = Instantiate(missile, player.transform.position, player.transform.rotation) as GameObject;
                firedMissile.GetComponent<Missile>().fire(player);
                isGamePaused = true;
                time = 0.0f;
            }
        }
        else
        {
            time += Time.deltaTime;
        }

        //Debug.Log(movement);
    }

    private void DrawPlayerStats()
    {
        
    }



        
}

