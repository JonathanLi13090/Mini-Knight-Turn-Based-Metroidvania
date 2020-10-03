using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_controller : MonoBehaviour
{
    public GameObject[] moveable_stuff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move_for_turn()
    {
        Debug.Log("turn Controller");
        foreach(GameObject game_objects in moveable_stuff)
        {
            game_objects.GetComponent<enemy_controller>().move(); 
        }
    }
}
