using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_controller : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject[] moveable_enemies;
    public player_controller player;
    private List<GameObject> turn_queue = new List<GameObject>();
    public bool queue_empty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        moveable_enemies = GameObject.FindGameObjectsWithTag("Enemy");
        platforms = GameObject.FindGameObjectsWithTag("moving_platform");
        if (!player) player = FindObjectOfType<player_controller>();
        if(turn_queue.Count <= 0)
        {
            queue_empty = true;
        }
        if (player.MoveMade && turn_queue.Count <= 0)
        {
            move_for_turn_platforms();
            //player.Move();
            turn_queue.Add(player.gameObject);
            move_for_turn();
            //player.MoveMade = false;
        }

        if(turn_queue.Count > 0)
        {
            if(turn_queue[0]) turn_queue[0].SendMessage("Move");
            turn_queue.RemoveAt(0);
        }

        if (player.MoveMade && turn_queue.Count == 0)
        {
            player.MoveMade = false;
        }
    }

    void move_for_turn_platforms()
    {
        foreach (GameObject game_objects in platforms)
        {
            //game_objects.GetComponent<MovingPlatform>().Move();
            turn_queue.Add(game_objects);
        }
    }

    public void move_for_turn()
    {
        foreach(GameObject game_objects in moveable_enemies)
        {
            //game_objects.GetComponent<enemy_controller>().Move();
            turn_queue.Add(game_objects);
        }
        
    }
}
