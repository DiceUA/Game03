using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    [SerializeField]
    float delay = 0.25f;
    float cooldown = 0f;
    public GameObject bullet;

	// Use this for initialization
	void Start () {
        if (!bullet)
            bullet = (GameObject)Resources.Load("Prefabs\\Blue_Bullet");
	    
	}
	
	// Update is called once per frame
	void Update () {

        // Rapid fire every delay seconds
        cooldown -= Time.deltaTime;
	    if(Input.GetButton("Fire1") && cooldown <= 0)
        {
            cooldown = delay;
            Fire();                                   
        }
	}

    /// <summary>
    /// Fires bullet
    /// </summary>
    void Fire()
    {
        // Instantiate bullet in front of ship
        // pos - spawn point on nose
        // bullet will fly forward (read bulletscript)
        Vector3 pos = transform.rotation * new Vector3(0, 1.85f, 0);
        Instantiate(bullet, transform.position + pos, transform.rotation);
    }
}
