using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    /* Constants */

    /* Enums */
    public enum GameState { NotStarted = 0, Playing, Paused, GameOver };

    /* Private variables */
    private GameState gameState;
    private GameObject eventSystem;

    private int score;
    private int highScore;

    /* Prefabs */

    /* Methods */
    void Start()
    {
	
    }
	
    void Update()
    {
	
    }
}

