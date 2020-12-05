using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_damage : MonoBehaviour
{
    public int max_health;
    public GameObject self;
    private int current_health;
    public int knockback_distance = 1;
    public float wall_check_distance = 1f;
    public LayerMask what_is_wall;
    public bool isDead
    {
        get { return current_health <= 0; }
        set { current_health = value ? max_health : 0; }
    }

    void Start()
    {
        //current_health = max_health;
        isDead = true;
    }

    public void TakeDamage(int damage, int direction, bool die_after)
    {
        current_health -= damage;

        if (current_health <= 0 && die_after)
        {
            self.GetComponent<enemy_controller>().Die_after = true;
            Debug.Log("set die after true");
        }

        else if (current_health <= 0)
        {
            Die();
        }

        else
        {
            if (direction == 1)
            {

                RaycastHit2D wallDetection = Physics2D.Raycast(transform.position, Vector2.right, wall_check_distance, what_is_wall);
                if (!wallDetection)
                {
                    transform.Translate(knockback_distance, 0, 0);
                }
            }
            else if (direction == 2)
            {

                RaycastHit2D wallDetection = Physics2D.Raycast(transform.position, Vector2.left, wall_check_distance, what_is_wall);
                if (!wallDetection)
                {
                    transform.Translate(-knockback_distance, 0, 0);
                }
            }
        }
    }



    void Die()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
