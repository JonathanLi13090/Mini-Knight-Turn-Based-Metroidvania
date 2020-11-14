using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public Area AreaScript;
    public float screen_width;
    public float screen_height;
    private float half_screen_width;
    private float half_screen_height;
    public GameObject player;
    private float current_player_x;
    private float current_player_y;

    public struct ScreenRange
    {
        public Vector2 lowerPoint;
        public Vector2 upperPoint;
    }
    public ScreenRange screenRange;
    bool hasScreenRangeSetup = false;

    // Start is called before the first frame update
    void Start()
    {
        half_screen_width = screen_width / 2;
        half_screen_height = screen_height / 2;
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

       // setScreenRange();

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasScreenRangeSetup)
        {
            setScreenRange();
            hasScreenRangeSetup = true;
        }

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
        current_player_x = player.transform.position.x;
        current_player_y = player.transform.position.y;

        if(current_player_x > transform.position.x + half_screen_width)
        {
            transform.Translate(screen_width, 0, 0);
            setScreenRange();
        }
        else if(current_player_x < transform.position.x - half_screen_width)
        {
            transform.Translate(-screen_width, 0, 0);
            setScreenRange();
        }
        else if(current_player_y > transform.position.y + half_screen_height)
        {
            transform.Translate(0, screen_height, 0, 0);
            setScreenRange();
        }
        else if(current_player_y < transform.position.y - half_screen_height)
        {
            transform.Translate(0, -screen_height, 0);
            setScreenRange();
        }
    }

    void setScreenRange()
    {
        screenRange.lowerPoint = new Vector2(transform.position.x + 0.5f - half_screen_width, transform.position.y + 0.5f - half_screen_height);
        screenRange.upperPoint = new Vector2(transform.position.x - 0.5f + half_screen_width, transform.position.y - 0.5f + half_screen_height);
        AreaScript.ScreenRangeChanged(screenRange);
    }
}
