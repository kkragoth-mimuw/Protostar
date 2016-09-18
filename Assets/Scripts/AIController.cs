using UnityEngine;
using System.Collections;

/* Very basic AI to follow player */
public class AIController : MonoBehaviour
{
    private int velocity;
    public static int minVelocity = 200;
    public static int maxVelocity = 300;

    private int positionVariation;
    private const float minPositionVar = 0.0f;
    private const float maxPoisitionVar = 150.0f;
    private const float posVar = 50.0f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        positionVariation = (int) Random.Range(minPositionVar, maxPoisitionVar);
        velocity = Random.Range(minVelocity, maxVelocity);
    }
	
    void Update()
    {
        /* Move towards player position with a little offset */
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position
            + new Vector3(Random.Range(-positionVariation, positionVariation), 0,
                Random.Range(-posVar, posVar)),
            velocity * Time.deltaTime); 
    }

    public static void Reset()
    {
        minVelocity = 200;
        maxVelocity = 300;
    }
}

