using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_controller : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject[] moveable_enemies;
    public player_controller player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        moveable_enemies = GameObject.FindGameObjectsWithTag("Enemy");
        platforms = GameObject.FindGameObjectsWithTag("moving_platform");
        if (!player) player = FindObjectOfType<player_controller>();
        if (player.MoveMade)
        {
            move_for_turn();
            player.MoveMade = false;
            player.move_player();
        }
    }

    void move_for_turn_platforms()
    {
        foreach (GameObject game_objects in platforms)
        {
            game_objects.GetComponent<MovingPlatform>().move();
        }
    }

    public void move_for_turn()
    {
        foreach(GameObject game_objects in moveable_enemies)
        {
            game_objects.GetComponent<enemy_controller>().move(); 
        }
        
    }
}
