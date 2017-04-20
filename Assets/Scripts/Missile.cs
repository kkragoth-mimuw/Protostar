using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private float force = 1000.0f;
    private float damage = 50.0f;
    private float lifeSpan = 0.9f;

    private GameObject owner;
    private AudioSource[] sounds;

    public void Start()
    {
        sounds = GetComponentsInChildren<AudioSource>();
    }

    public void fire()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        //sounds[0].Play(); // TODO: fix volume level when firing multiple missiles
    }

    public void fire(GameObject owner)
    {
        fire();
        this.owner = owner;
    }
	
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.tag == Tags.PLAYER_SPAWN) ||
            (other.gameObject.tag == Tags.MISSILE) ||
            (other.gameObject.tag == Tags.BORDER) ||
            (other.gameObject.tag == Tags.DEADLY_BORDER) ||
            (other.gameObject == owner)
        )
            return;

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}

