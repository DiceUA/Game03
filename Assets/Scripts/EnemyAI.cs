using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    GameObject playerShip;
    public float rotationSpeed = 50f;
    public float shipSpeed = 5f;
    //shoot cooldown
    float delay = 0.5f;
    float cooldown = 0f;
    public GameObject bullet;
    RaycastHit2D hit;

    // Use this for initialization
    void Start () {
        playerShip = Player.Ship;
        if (!bullet)
            bullet = (GameObject)Resources.Load("Prefabs\\Blue_Bullet");
        
    }
	
	// Update is called once per frame
	void Update () {
        // Try find player
        if (playerShip == null)
        {
            playerShip = Player.Ship;
        }
        // if player ship not found
        // do nothing
        if (playerShip == null)
            return;

        
        // Direction to player ship
        Vector3 direction = playerShip.transform.position - transform.position;
        direction.Normalize();

        // Rotate nose to face player
        float angle = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI - 90;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);

        // Movement
        Vector3 newPosition = transform.position;
        newPosition += transform.rotation * new Vector3(0, Time.deltaTime * shipSpeed, 0);
        transform.position = newPosition;


        // Shooting
        // Calculate nose spawn position
        // noseSpawnPoint always face forward direction
        // 1.76f - offset from center to nose
        cooldown -= Time.deltaTime;
        Vector3 noseSpawnPoint = transform.rotation * new Vector3(0, 1.76f, 0);        
        Debug.DrawRay(transform.position + noseSpawnPoint, noseSpawnPoint * 10, Color.green);
        hit = Physics2D.Raycast(transform.position + noseSpawnPoint, noseSpawnPoint);

        // if player ship gets into aim
        // shoot it
        if(hit.collider != null && Vector3.Distance(transform.position, playerShip.transform.position) < 20)
            if (hit.collider.tag == "Player" && cooldown <= 0 )
            {
                cooldown = delay;                
                Instantiate(bullet, transform.position + noseSpawnPoint, transform.rotation);
            }


    }
}
