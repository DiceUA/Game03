using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {

    [SerializeField]
    GameObject playerPrefab;
    GameObject playerShip;
    [SerializeField]
    List<GameObject> enemies;
    float spawnDelay = 3f;
    float spawnTimer;
    public Camera cam;
    public static float ScreenHeight { get; private set; }
    public static float ScreenWidth { get; private set; }
    // Player have 3 tries(respawns)
    int lives = 3;

    // All garbage Gameobjects that will be destroyed every X seconds
    public static List<GameObject> garbage;

    public static int score;


    // Use this for initialization
    void Start() {
        score = 0;
        garbage = new List<GameObject>();
        playerPrefab = (GameObject)Resources.Load("Prefabs\\Player");
        
        if (enemies.Count == 0)
            enemies.Add((GameObject)Resources.Load("Prefabs\\EnemyType1"));

        if (!cam)
            cam = Camera.main;
        // Get screen width and height in Unity points
        ScreenHeight = cam.orthographicSize;
        ScreenWidth = ScreenHeight * cam.aspect;
        Debug.Log(cam.orthographicSize);

        SpawnPlayer();
    }

    // Update is called once per frame
    void Update() {
              
        // If screen change resolution get new
        ScreenHeight = cam.orthographicSize;
        ScreenWidth = ScreenHeight * cam.aspect;

        // When Health reach 0 destroy player ship and end game
        if (Player.Health <= 0 && playerShip != null)
        {
            Destroy(playerShip);
            lives--;
            // This will pause game - set timeflow zero. Update still works.
            //Time.timeScale = 0f;
        }        
                    

        // Enemies spawner
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && playerShip != null)
        {
            spawnTimer = spawnDelay;

            SpawnEnemy(Random.Range(1, 4));
        }

        // if R pressed respawn player
        if (playerShip == null && Input.GetKeyUp(KeyCode.R) && lives > 0)
            SpawnPlayer();

    }

    // Pass location from 1 to 4.
    // Spawns enemy somewhere
    void SpawnEnemy(int location)
    {

        Vector3 spawnLocation = Vector3.zero;
        // Left - 1
        switch (location)
        {
            case 1:
                spawnLocation.x = -ScreenWidth - 2; // left
                break;
            case 2:
                spawnLocation.y = ScreenHeight + 2; // top
                break;
            case 3:
                spawnLocation.x = ScreenWidth + 2; // right
                break;
            case 4:
                spawnLocation.y = -ScreenHeight - 2; // bottom
                break;
            default:
                break;
        }
        Instantiate(enemies[Random.Range(0, enemies.Count - 1)], spawnLocation, Quaternion.identity);
        
        //Call garbage collector when enemy spawn
        GarbageCollector();                
    }

    /// <summary>
    /// Spawns player
    /// </summary>
    void SpawnPlayer()
    {
        playerShip = (GameObject)Instantiate(playerPrefab);        
        //Time.timeScale = 1f;
    }
    /// <summary>
    /// Collects particles and other garbage
    /// and destroy it
    /// if called in update need to set timer
    /// </summary>
    void GarbageCollector()
    {
        if(garbage.Count > 0)
            foreach (var item in garbage)
            {
                Destroy(item);
            }
    }
    
    void OnGUI()
    {
        // Show health and lives in left corner
        GUI.Label(new Rect(10, 10, 100, 50), "Score: " + score +"\nLives: " + lives + "\nHP: " + Player.Health);

        // Show help message
        GUI.Label(new Rect(200, 10, 300, 50), "W A S D - to fly\nShoot with mouse 1");

        // Show respawn button
        if (lives>0 && playerShip == null)
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Press R to instantly respawn");
        
        // Check Gameover and print Text
        if (lives <= 0)
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Game Over!");
    }

}
