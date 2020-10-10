using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public float screen_width;
    public float screen_height;
    private float half_screen_width;
    private float half_screen_height;
    public GameObject player;
    private float current_player_x;
    private float current_player_y;

    // Start is called before the first frame update
    void Start()
    {
        half_screen_width = screen_width / 2;
        half_screen_height = screen_height / 2;
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
        current_player_x = player.transform.position.x;
        current_player_y = player.transform.position.y;

        if(current_player_x > transform.position.x + half_screen_width)
        {
            transform.Translate(screen_width, 0, 0);
        }
        else if(current_player_x < transform.position.x - half_screen_width)
        {
            transform.Translate(-screen_width, 0, 0);
        }
        else if(current_player_y > transform.position.y + half_screen_height)
        {
            transform.Translate(0, screen_height, 0, 0);
        }
        else if(current_player_y < transform.position.y - half_screen_height)
        {
            transform.Translate(0, -screen_height, 0);
        }
    }
}
