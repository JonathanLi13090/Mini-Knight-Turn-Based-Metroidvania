using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_controller : MonoBehaviour
{
    public GameObject[] moveable_stuff;
    public player_controller player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (!player) player = FindObjectOfType<player_controller>();
        if (player.MoveMade)
        {
            move_for_turn();
            player.MoveMade = false;
        }
    }

    public void move_for_turn()
    {
        Debug.Log("turn Controller");
        foreach(GameObject game_objects in moveable_stuff)
        {
            if(game_objects) game_objects.GetComponent<enemy_controller>().move(); 
        }
    }
}
