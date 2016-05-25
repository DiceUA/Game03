using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    float playerSpeed = 10f;
    float rotationSpeed = 100f;
    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        //Ship direction of movement. Get new position of ship.
        Vector3 newPosition = transform.position; 
        newPosition += transform.rotation * new Vector3(0, Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed, 0);

        //Rotate ship
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed);

        //Boundaries top bottom left right
        if (transform.position.y > GameScript.ScreenHeight)
            newPosition.y = GameScript.ScreenHeight;
        if (transform.position.y < -GameScript.ScreenHeight)
            newPosition.y = -GameScript.ScreenHeight;
        if (transform.position.x > GameScript.ScreenWidth)
            newPosition.x = GameScript.ScreenWidth;
        if (transform.position.x < -GameScript.ScreenWidth)
            newPosition.x = -GameScript.ScreenWidth;

        // Moves ship to new position.
        transform.position = newPosition;



    }
}
