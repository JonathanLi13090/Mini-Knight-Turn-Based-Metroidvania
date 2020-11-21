using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float wall_check_distance;
    public Animator animator;
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
    public bool attacked;

    public bool MoveMade = false;
    bool onLadder = false;
    public Vector2 MovePos; //(-1, 0), (0, 1) etc
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();

        if(!MoveMade) HandleMove();

        
        
    }

    bool LeftArrow;
    bool LeftArrowDown;
    bool LeftArrowUp;
    bool RightArrow;
    bool RightArrowDown;
    bool RightArrowUp;
    bool JumpButton;
    bool JumpButtonUp;
    bool JumpButtonDown;
    bool AttackButton;
    bool AttackButtonUp;
    bool AttackButtonDown;
   
    void HandleMove()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !onLadder)
        {
            Attack();
        }
        else if (AttackButton && onLadder && (LeftArrowDown || RightArrowDown))
        {
            if (LeftArrowDown && !facingLeft || RightArrowDown && facingLeft) Flip();
            Attack();
        }
        else if (!AttackButton)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveMade = HorizontalMovement(false);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveMade = HorizontalMovement(true);
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
            else if (facingLeft == true && Input.GetKey(KeyCode.RightArrow))
            {
                Flip();
            }
        }
    }

    void HandleInputs()
    {
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    if (!LeftArrow) LeftArrowDown = true;
        //    else LeftArrowDown = false;
        //    LeftArrow = true;
        //    LeftArrowUp = false;
        //}
        //else
        //{
        //    if (LeftArrow) LeftArrowUp = true;
        //    else LeftArrowUp = false;
        //    LeftArrow = false;
        //    LeftArrowDown = false;
        //}
        HandleInput(Input.GetKey(KeyCode.LeftArrow), ref LeftArrow, ref LeftArrowUp, ref LeftArrowDown);
        HandleInput(Input.GetKey(KeyCode.RightArrow), ref RightArrow, ref RightArrowUp, ref RightArrowDown);
        HandleInput(Input.GetKey(KeyCode.UpArrow), ref JumpButton, ref JumpButtonUp, ref JumpButtonDown);
        HandleInput(Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Jump") > 0.1f, ref AttackButton, ref AttackButtonUp, ref AttackButtonDown);
    }

    void HandleInput(bool input, ref bool button, ref bool up, ref bool down)
    {
        if (input)
        {
            if (!button) down = true;
            else down = false;
            button = true;
            up = false;
        }
        else
        {
            if (button) up = true;
            else up = false;
            button = false;
            down = false;
        }
    }

    bool HorizontalMovement(bool moving_right)
    {
        attacked = false;
        animator.SetBool("is walk", true);
        animator.SetBool("is attacking", false);
        animator.SetBool("is climbing", false);
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
            MovePos = new Vector2(move_direction, 0);
            FindObjectOfType<AudioHandler>().PlaySound("Player", "walk");
            moved = true;
            onLadder = false;
            checkForCheckpoint(move_direction_vector);
        //    transform.Translate(move_direction, 0, 0);
        //    int fallen_distance = 0;
        //    while (fallen_distance < 20)
        //    {
        //        fallen_distance += 1;
        //        RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
        //        RaycastHit2D down_portal_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_portals);
        //        if (!ground_info)
        //        {
        //            //checkForCheckpoint(Vector2.down);
        //            transform.Translate(0, -move_distance, 0);
        //            if (down_portal_info)
        //            {
        //                PortalSO portal1 = down_portal_info.transform.GetComponent<Portal>().MyPortalSO;
        //                FindObjectOfType<Area>().OpenPortal(portal1);
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    if (fallen_distance >= 20)
        //    {
        //        Debug.Log("player fell of map or more than 20 blocks");
        //        GetComponent<Player_health>().Die();
        //    }
        }
        return moved;
    }

    public void Move()
    {
        transform.Translate(MovePos);
        //if jump, dont do groundcheck bool?
        if(MovePos.y < 1)
        {
            int fallen_distance = 0;
            while (fallen_distance < 20)
            {
                fallen_distance += 1;
                RaycastHit2D ground_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
                RaycastHit2D down_portal_info = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_portals);
                if (!ground_info)
                {
                    //checkForCheckpoint(Vector2.down);
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
                    //FindObjectOfType<AudioHandler>().PlaySound("Player", "fall_sound");
                    break;
                }
            }
            if (fallen_distance >= 20)
            {
                Debug.Log("player fell of map or more than 20 blocks");
                GetComponent<Player_health>().Die();
            }
        }  
    }


    public void checkForCheckpoint(Vector2 move_direction)
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, move_direction, move_distance, what_is_checkpoint);
        bool hitInfo = UtilityTilemap.CheckTileType("Portal", transform.position + new Vector3(move_direction.x, 0, 0), "checkpoint_1");
        if (hitInfo)
        {
            //FindObjectOfType<Area>().SetCheckpoint(hitInfo.transform);   
            FindObjectOfType<Area>().SetCheckpoint(transform.position + new Vector3(move_distance, 0, 0));
            FindObjectOfType<AudioHandler>().PlaySound("Player", "checkpoint_sound");
        }
    }

    bool Jump()
    {
        bool moved = false;

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
            if (!hitInfo)
            {
                FindObjectOfType<AudioHandler>().PlaySound("Player", "ladder");
                onLadder = true;
                animator.SetBool("is climbing", true);
                animator.SetBool("is attacking", false);
                moved = true;
                MovePos = new Vector2(0, move_distance);
                //transform.Translate(0, move_distance, 0);
            }
           
        }     
        else
        {
            if (groundInfo)
            {
                if (!hitInfo)
                {
                    FindObjectOfType<AudioHandler>().PlaySound("Player", "player_jump");
                    onLadder = false;
                    moved = true;
                    MovePos = new Vector2(0, move_distance);
                    //transform.Translate(0, move_distance, 0);
                }
            }
        }
        return moved;
    }

    bool Down()
    {
        bool moved = false;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_ladder);
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
        if (!groundInfo)
        {
            if (hitInfo)
            {
                onLadder = true;
                moved = true;
                checkForCheckpoint(Vector2.down);
                transform.Translate(0, -move_distance, 0);
            }
        }
        else
        {
            onLadder = false;
        }
        return moved;
    }

    void Attack()
    {
        if(attacked == false)
        {
            animator.SetBool("is attacking", true);
            int kickDirection;
            if (facingLeft == false) kickDirection = 1;
            else kickDirection = 2;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, what_is_enemy);
            if (hitEnemies.Length > 0)
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    FindObjectOfType<AudioHandler>().PlaySound("Player", "enemy_hurt");
                    enemy.GetComponent<enemy_damage>().TakeDamage(attack_damage, kickDirection);  
                }
            }
            attacked = true;
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
