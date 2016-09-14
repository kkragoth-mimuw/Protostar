using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    
    public static int minVelocity = 200;
    public static int maxVelocity = 300;

    private int positionVariation;
    private int velocity;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        positionVariation = (int) Random.Range(0.0f, 150.0f);
        velocity = Random.Range(minVelocity, maxVelocity);
    }
	
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position
            + new Vector3(Random.Range(-positionVariation, positionVariation), 0,
                Random.Range(-50.0f, 50.0f)),
            velocity * Time.deltaTime); 
    }

    public static void Reset()
    {
        minVelocity = 100;
        maxVelocity = 150;
    }
}

