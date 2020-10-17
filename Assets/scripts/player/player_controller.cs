using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float wall_check_distance;
    public GameObject turn_controller;
    public LayerMask what_is_portals;
    public LayerMask what_is_wall;
    public LayerMask what_is_enemy;
    public LayerMask what_is_ladder;
    public LayerMask what_is_checkpoint;
    public Transform attackPoint;
    public Transform down_attackPoint;
    public float attackRange;
    public int attack_damage;
    public bool is_grounded;
    public float move_distance;
    private bool facingLeft = true;

    public bool MoveMade = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(what_is_checkpoint.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //MoveMade =Left();
            MoveMade = HorizontalMove(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //MoveMade = Right();
            MoveMade = HorizontalMove(true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveMade = Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveMade = Down();
        }
        if (facingLeft == false && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Flip();
        }
        else if(facingLeft == true && Input.GetKey(KeyCode.RightArrow))
        {
            Flip();
        }
    }

    bool HorizontalMove(bool moving_right)
    {
        //turn_controller.GetComponent<turn_controller>().move_for_turn(); // == < <= >= > bool
        bool moved = false;
        Vector2 move_direction_vector = moving_right ? Vector2.right : Vector2.left;
        float move_direction = moving_right ? move_distance : -move_distance;

        

        RaycastHit2D portalInfo = Physics2D.Raycast(transform.position, move_direction_vector, wall_check_distance, what_is_portals);
        if (portalInfo)
        {
            if (portalInfo.transform.GetComponent<Portal>())
            {
                PortalSO portal = portalInfo.transform.GetComponent<Portal>().MyPortalSO;
                FindObjectOfType<Area>().OpenPortal(portal);
            }
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, move_direction_vector, wall_check_distance, what_is_wall);
        if (!hitInfo)
        {
            checkForCheckpoint(move_direction_vector);
            moved = true;
            transform.Translate(move_direction, 0, 0);
            int fallen_distance = 0;
            while (fallen_distance < 20)
            {
                fallen_distance += 1;
                RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
                RaycastHit2D down_portal_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_portals);
                if (!ground_info)
                {
                    transform.Translate(0, -move_distance, 0);
                    if (down_portal_info)
                    {
                        PortalSO portal1 = down_portal_info.transform.GetComponent<Portal>().MyPortalSO;
                        FindObjectOfType<Area>().OpenPortal(portal1);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (fallen_distance >= 20)
            {
                Debug.Log("Player Fell off of map");
                GetComponent<Player_health>().Die();
            }
        }

        

        return moved;
    }

    void checkForCheckpoint(Vector2 move_direction)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, move_direction, move_distance, what_is_checkpoint);
        if (hitInfo)
        {
            FindObjectOfType<Area>().SetCheckpoint(hitInfo.transform);
        }
    }

    //bool Right()
    //{
    //    //turn_controller.GetComponent<turn_controller>().move_for_turn();
    //    bool moved = false;
    //    RaycastHit2D portalInfo = Physics2D.Raycast(transform.position, Vector2.right, wall_check_distance, what_is_portals);
    //    if (portalInfo)
    //    {
    //        if (portalInfo.transform.GetComponent<Portal>())
    //        {
    //            PortalSO portal = portalInfo.transform.GetComponent<Portal>().MyPortalSO;
    //            FindObjectOfType<Area>().OpenPortal(portal);
    //        }
    //    }
        
    //    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, wall_check_distance, what_is_wall);
    //    if (!hitInfo)
    //    {
    //        moved = true;
    //        transform.Translate(move_distance, 0, 0);
    //        while(true)
    //        {
    //            RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
    //            RaycastHit2D down_portal_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_portals);
    //            if (!ground_info)
    //            {
    //                transform.Translate(0, -move_distance, 0);
    //                if (down_portal_info)
    //                {
    //                    PortalSO portal1 = down_portal_info.transform.GetComponent<Portal>().MyPortalSO;
    //                    FindObjectOfType<Area>().OpenPortal(portal1);
    //                    break;
    //                }
    //            }
    //            else
    //            {
    //                break;
    //            }
    //        }
    //    }
    //    return moved;
    //}

    //bool Left()
    //{
    //    bool moved = false;

    //    RaycastHit2D portalInfo = Physics2D.Raycast(transform.position, Vector2.left, wall_check_distance, what_is_portals);
    //    if (portalInfo)
    //    {
    //        if (portalInfo.transform.GetComponent<Portal>())
    //        {
    //            PortalSO portal = portalInfo.transform.GetComponent<Portal>().MyPortalSO;
    //            FindObjectOfType<Area>().OpenPortal(portal);
    //        }
    //    }
    //    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.left, wall_check_distance, what_is_wall);
    //    if (!hitInfo)
    //    {
    //        moved = true;
    //        transform.Translate(-move_distance, 0, 0);
    //        while (true)
    //        {
    //            RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
    //            RaycastHit2D down_portal_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_portals);
    //            if (!ground_info)
    //            {
    //                transform.Translate(0, -move_distance, 0);
    //                if (down_portal_info)
    //                {
    //                    PortalSO portal = down_portal_info.transform.GetComponent<Portal>().MyPortalSO;
    //                    FindObjectOfType<Area>().OpenPortal(portal);
    //                    break;
    //                }
    //            }
    //            else
    //            {
    //                break;
    //            }
    //        }
    //    }
    //    return moved;
    //}

    bool Jump()
    {
        bool moved = false;

        checkForCheckpoint(Vector2.up);

        RaycastHit2D portalInfo = Physics2D.Raycast(transform.position, Vector2.up, wall_check_distance, what_is_portals);
        if (portalInfo)
        {
            if (portalInfo.transform.GetComponent<Portal>())
            {
                PortalSO portal = portalInfo.transform.GetComponent<Portal>().MyPortalSO;
                FindObjectOfType<Area>().OpenPortal(portal);
            }
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, wall_check_distance, what_is_wall);
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
        RaycastHit2D ladderInfo = Physics2D.Raycast(transform.position, Vector2.up, wall_check_distance, what_is_ladder);
        if (ladderInfo)
        {
            moved = true;
            transform.Translate(0, move_distance, 0);
        }     
        else
        {
            if (groundInfo)
            {
                if (!hitInfo)
                {
                    moved = true;
                    transform.Translate(0, move_distance, 0);
                }
            }
        }
        
        return moved;
    }

    bool Down()
    {
        checkForCheckpoint(Vector2.down);

        bool moved = false;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_ladder);
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
        if (!groundInfo)
        {
            if (hitInfo)
            {
                moved = true;
                transform.Translate(0, -move_distance, 0);
            }
        }
        
        return moved;
    }

    void Attack()
    {
        int kickDirection;
        if (facingLeft == false) kickDirection = 1;
        else kickDirection = 2;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, what_is_enemy);
        if(hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("enemy hit");
                enemy.GetComponent<enemy_damage>().TakeDamage(attack_damage, kickDirection);
            }
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
