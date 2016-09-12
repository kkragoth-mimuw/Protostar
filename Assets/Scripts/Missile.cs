using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{

    private float force = 1000.0f;
    private float damage = 50.0f;
    private float lifeSpan = 1.5f;

    private GameObject owner;

    public void fire()
    {
        //GetComponent<Rigidbody>().detectCollisions = false;
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * force);
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
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
        Debug.Log("Kolizja");
    }
}

