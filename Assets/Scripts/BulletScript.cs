using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    float speed = 15f;
    float timeToLive = 4f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


        //Bullet movement
        Vector3 newPosition = transform.position;
        newPosition += transform.rotation * new Vector3(0, Time.deltaTime * speed, 0);
        transform.position = newPosition;

        // Bullet live time
        // destroy bullet to prevent memory leak
        Destroy(gameObject, timeToLive);            

    }
}
