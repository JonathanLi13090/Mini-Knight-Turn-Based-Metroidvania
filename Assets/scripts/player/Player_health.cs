using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_health : MonoBehaviour
{
    public Text Health;
    public int max_health;
    private int current_health;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void take_damage(int damage)
    {
        if(current_health > 0)
        {
            current_health -= damage;
            Health.text = current_health.ToString();
        }
        else
        {
            Die();
        }

    }

    public void Die()
    {
        Debug.Log("player died");
    }
}
