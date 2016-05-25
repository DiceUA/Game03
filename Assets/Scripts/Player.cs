using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    //Starting invul
    float invulTimer = 3f;
    public static int Health { get; set; }    
    public static GameObject Ship { get; set; }
    bool isInvul { get; set; }    

    void Start ()
    {
        // Find gameobject to get easy access from other scripts
        Ship = GameObject.FindGameObjectWithTag("Player");
        Health = 3;
        isInvul = false;
        MakeEthereal();        
    }

    void Update ()
    {
        //Debug.Log(isInvul);
        // Check invul status of player
        // after 1s break invul
        if (isInvul)
        {
            invulTimer -= Time.deltaTime;
        }
        if (invulTimer <= 0)
        {
            invulTimer = 1f;
            MakeReal();
        }
    }

    /// <summary>
    /// Make player undestructible for X seconds
    /// </summary>
    public void MakeEthereal()
    {
        if (!isInvul)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            isInvul = true;
            // to see when player invul set model color to blue
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

        } 
    }
    /// <summary>
    /// Make player vunerable
    /// </summary>
    public void MakeReal()
    {
        if (isInvul)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            isInvul = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }     
    }  
    
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.tag);
        // If player hit by bullet
        // lose health
        // make undestructible
        // destroy bullet that hit player
        if (col.gameObject.tag == "Bullet")
        {
            Health--;
            MakeEthereal();
            Destroy(col.gameObject);
            Debug.Log(Player.Health);
        }
    }      
}

