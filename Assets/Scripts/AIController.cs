using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{

    private GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
	
    }
	
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position
            + new Vector3(Random.Range(-150.0f, 150.0f), 0, Random.Range(-150.0f, 150.0f))
            , Random.Range(200, 1500) * Time.deltaTime); 
    }
}

