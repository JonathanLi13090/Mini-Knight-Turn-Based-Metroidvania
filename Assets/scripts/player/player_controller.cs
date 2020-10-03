using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float wall_check_distance;
    public GameObject turn_controller;
    public LayerMask what_is_wall;
    public LayerMask what_is_enemy;
    public Transform attackPoint;
    public Transform left_attackPoint;
    public Transform down_attackPoint;
    public float attackRange;
    public int attack_damage;
    public bool is_grounded;
    public float move_distance;
    public bool facingLeft = false;

    public bool MoveMade = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
            
            MoveMade = Left();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            MoveMade = Right();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            MoveMade = Jump();
        }
        if(facingLeft == false && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Flip();
        }
        else if(facingLeft == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            Flip();
        }
    }

    bool Right()
    {
        //turn_controller.GetComponent<turn_controller>().move_for_turn();
        bool moved = false;
        RaycastHit2D portalInfo = Physics2D.Raycast(transform.position, Vector2.right, wall_check_distance);
        if (portalInfo)
        {
            if (portalInfo.transform.GetComponent<Portal>())
            {
                PortalSO portal = portalInfo.transform.GetComponent<Portal>().MyPortalSO;
                FindObjectOfType<Area>().OpenPortal(portal);
            }
        }
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, wall_check_distance, what_is_wall);
        if (!hitInfo)
        {
            moved = true;
            transform.Translate(move_distance, 0, 0);
            while(true)
            {
                RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
                RaycastHit2D down_portal_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance);
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
        }
        return moved;
    }

    bool Left()
    {
        //turn_controller.GetComponent<turn_controller>().move_for_turn();
        bool moved = false;
        RaycastHit2D portalInfo = Physics2D.Raycast(transform.position, Vector2.left, wall_check_distance);
        if (portalInfo)
        {
            if (portalInfo.transform.GetComponent<Portal>())
            {
                PortalSO portal = portalInfo.transform.GetComponent<Portal>().MyPortalSO;
                FindObjectOfType<Area>().OpenPortal(portal);
            }
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.left, wall_check_distance, what_is_wall);
        if (!hitInfo)
        {
            moved = true;
            transform.Translate(-move_distance, 0, 0);
            while (true)
            {
                RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
                RaycastHit2D down_portal_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance);
                if (!ground_info)
                {
                    transform.Translate(0, -move_distance, 0);
                    if (down_portal_info)
                    {
                        PortalSO portal = down_portal_info.transform.GetComponent<Portal>().MyPortalSO;
                        FindObjectOfType<Area>().OpenPortal(portal);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return moved;

    }

    bool Jump()
    {
        bool moved = false;
        RaycastHit2D portalInfo = Physics2D.Raycast(transform.position, Vector2.up, wall_check_distance);
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
        if (groundInfo)
        {
            if (!hitInfo)
            {
                transform.Translate(0, move_distance, 0);
                moved = true;
            }
        }
        return moved;
    }

    void Attack()
    {
        int kickDir;

        if (facingLeft == false) kickDir = 1;
        else kickDir = 2;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, what_is_enemy);
        if (hitEnemies.Length > 0)
        {
            
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Enemy Hit");
                enemy.GetComponent<enemy_damage>().TakeDamage(attack_damage, kickDir);
            }
        }
        //else if(facingLeft == true)
        //{
        //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(left_attackPoint.position, attackRange, what_is_enemy);
        //    if (hitEnemies.Length > 0)
        //    {
        //        foreach (Collider2D enemy in hitEnemies)
        //        {
        //            Debug.Log("Enemy Hit");
        //            enemy.GetComponent<enemy_damage>().TakeDamage(attack_damage, 2);
        //        }
        //    }
        //}
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
