using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.Euler(new Vector3(90, 0, -160));
	}
}
