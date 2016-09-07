using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CnControls;

public class GameGUI : MonoBehaviour
{
    /* Public UI objects */
    public GameObject GUIPanel;
    public GameObject InfoPanel;

    /* Private variables */
    private GameController gameController;
    private PlayerController playerController;
    private GameObject player;

    private bool hasGameEnded;
    private bool isGamePaused;


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
            0f,
            0f,
            CnInputManager.GetAxis("Vertical"));

        Vector3 rotation = new Vector3(-90.0f * CnInputManager.GetAxis("Mouse Y"), 180.0f * CnInputManager.GetAxis("Mouse X"),
            -180.0f *CnInputManager.GetAxis("Horizontal"));

        Debug.Log(rotation);

        player.transform.Rotate(rotation * Time.deltaTime); //.rotation += 20 * rotation;
        //player.transform.localEulerAngles = rotation;

        //player.transform.position += 2.5f * movement * player.transform.forward;
        player.transform.position += movement.z * transform.forward;
        player.transform.position += movement.x * transform.right;


        //Debug.Log(movement);
    }

    private void DrawPlayerStats()
    {
        
    }



        
}

