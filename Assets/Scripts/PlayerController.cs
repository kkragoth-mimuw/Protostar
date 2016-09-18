using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private float velocity;
    private float timeBetweenAttacks;
    private float sinceLastAttack;

    private static float maxTimeBetweenAttacks = 0.5f;
    private static float minTimeBetweenAttacks = 0.03f;

    private const float startVelocity = 250.0f;
    private const float velocityMultiplier = 50.0f;

    private GameObject player;
    private GameController gc;

    public GameObject missile;

    public void IncreaseVelocity(float v)
    {
        velocity += v;
    }

    public void DecreaseTimeBetweenAttacks(float dt)
    {
        timeBetweenAttacks -= dt;
        if (timeBetweenAttacks <= minTimeBetweenAttacks)
            timeBetweenAttacks = minTimeBetweenAttacks;
    }
        
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        makeCameraFollow();
    
        timeBetweenAttacks = maxTimeBetweenAttacks;
        velocity = startVelocity;

        gc = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
    }

    public void Rotate(Vector3 r)
    {
        player.transform.rotation = Quaternion.Euler(r);
    }

    public void Move(Vector3 m)
    {
        velocity = startVelocity + velocityMultiplier * gc.WaveMultipler();
        player.transform.position += velocity * m * Time.deltaTime; 
    }

    public void Shoot()
    {
        timeBetweenAttacks = maxTimeBetweenAttacks - 0.05f * gc.WaveMultipler();
        if (timeBetweenAttacks <= minTimeBetweenAttacks)
            timeBetweenAttacks = minTimeBetweenAttacks;

        if (sinceLastAttack >= timeBetweenAttacks)
        {
            GameObject firedMissile;
            firedMissile = Instantiate(missile, player.transform.position, player.transform.rotation) as GameObject;
            firedMissile.GetComponent<Missile>().fire(player);

            for (int i = 1; i <= gc.WaveMultipler() / 2; i++)
            {
                for (int j = -1; j <= 1;)
                {
                    firedMissile = Instantiate(missile, player.transform.position, player.transform.rotation * Quaternion.Euler(new Vector3(0, j * i * 15, 0))) as GameObject;
                    firedMissile.GetComponent<Missile>().fire(player);
                    j += 2;
                }
            }
            sinceLastAttack = 0.0f; 
        }
    }
	
    void Update()
    {
        sinceLastAttack += Time.deltaTime;
    }

    public void Die()
    {
        gc.GameOver();
        makeCameraUnfollow();
        Destroy(player);
        Destroy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.ENEMY)
        {
            this.Die();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.DEADLY_BORDER)
        {
            this.Die();
        }
    }

    private void makeCameraFollow()
    {
        GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA)
            .GetComponent<UnityStandardAssets._2D.Camera2DFollow>()
            .target = player.GetComponentsInChildren<Transform>()[1];
    }

    private void makeCameraUnfollow()
    {
        GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA)
            .GetComponent<UnityStandardAssets._2D.Camera2DFollow>()
            .target = GameObject.FindGameObjectWithTag(Tags.PLAYER_SPAWN).transform;
    }
}

