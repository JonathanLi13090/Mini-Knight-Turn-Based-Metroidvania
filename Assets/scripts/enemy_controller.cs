using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller : MonoBehaviour
{
    public float wall_check_distance;
    public float move_distance;
    public LayerMask what_is_wall;
    public LayerMask what_is_player;
    public float attack_range;
    public bool going_right = true;
    public Transform attack_point;
    public int attack_damage;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack_point.position, attack_range, what_is_player);
        if(hitPlayer.Length > 0)
        {
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<Player_health>().take_damage(attack_damage);
            }
        }
        if (going_right == true)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, wall_check_distance, what_is_wall);
            if (!hitInfo)
            {
                //transform.position += Vector3.left * move_distance;
                transform.Translate(move_distance, 0, 0);
                Debug.Log("right");
            }
            else
            {
                Debug.Log("hit wall");
                going_right =! going_right; 
            }
        }
        else if(going_right == false)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.left, wall_check_distance, what_is_wall);
            if (!hitInfo)
            {
                transform.Translate(-move_distance, 0, 0);
                Debug.Log("left");
            }
            else
            {
                going_right = !going_right;
            }
        }
    }

    void Flip()
    {
        going_right = !going_right;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
