using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float wall_check_distance;
    public LayerMask what_is_wall;
    public LayerMask what_is_enemy;
    public Transform right_attackPoint;
    public Transform left_attackPoint;
    public Transform down_attackPoint;
    public float attackRange;
    public int attack_damage;
    public bool is_grounded;
    public float move_distance;
    private bool facingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if(facingLeft == true && Input.GetKey(KeyCode.LeftArrow))
        {
            Flip();
        }
        else if(facingLeft == false && Input.GetKey(KeyCode.RightArrow))
        {
            Flip();
        }
    }

    void Right()
    {
        //facingLeft = false;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, wall_check_distance, what_is_wall);
        if (hitInfo && hitInfo.transform.GetComponent<Portal>())
        {
            PortalSO portal = hitInfo.transform.GetComponent<Portal>().MyPortalSO;
            FindObjectOfType<Area>().OpenPortal(portal);
        }

        if (!hitInfo)
        {
            transform.Translate(move_distance, 0, 0);
            while(true)
            {
                RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);

                

                if (!ground_info)
                {
                    transform.Translate(0, -move_distance, 0);
                }
                else
                {
                    break;
                }
            }
        }
    }

    void Left()
    {
        //facingLeft = true;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.left, wall_check_distance, what_is_wall);
        if (!hitInfo)
        {
            transform.Translate(-move_distance, 0, 0);
            while (true)
            {
                RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
                if (!ground_info)
                {
                    transform.Translate(0, -move_distance, 0);
                }
                else
                {
                    break;
                }
            }
        }

    }

    void Jump()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, wall_check_distance, what_is_wall);
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
        if (groundInfo)
        {
            if (!hitInfo)
            {
                transform.Translate(0, move_distance, 0);
            }
        }  
    }

    void Attack()
    {

    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
