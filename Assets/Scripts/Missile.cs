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
        //GetComponent<Rigidbody>().detectCollisions = false;
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * force);
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        sounds[0].Play();
        //Debug.Log(transform.forward);
    }

    public void fire(GameObject owner)
    {
        //GetComponent<Rigidbody>().detectCollisions = false;
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * force);
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        this.owner = owner;
        //Debug.Log(transform.forward);
    }
	
    // Update is called once per frame
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
            (other.gameObject == owner) ||
            (other.gameObject.tag == Tags.BORDER) ||
            (other.gameObject.tag == Tags.DEADLY_BORDER)
            )
            return;

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}

