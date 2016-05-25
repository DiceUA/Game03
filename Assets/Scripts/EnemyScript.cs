using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {


    void Start()
    {
        
    }

    void Update()
    {

    }


    // If player collide with enemy ship
    // player must lose health and destroy
    // enemy ship then get invul
    // I make Friendlyfire colliding :3 just to test mechanics 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player.Ship.GetComponent<Player>().MakeEthereal();
            Player.Health--;
            Debug.Log(Player.Health);
        }
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }

        // Show explosion
        // then add it to garbage to be destroyed
        // look for garbage in GameScript.cs       
        GameObject gb = (GameObject)Instantiate((GameObject)Resources.Load("Prefabs\\Explosion"),transform.position,Quaternion.identity);
        GameScript.garbage.Add(gb);
        Destroy(gameObject);
        GameScript.score++;

    }


}
