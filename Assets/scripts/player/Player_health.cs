using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    
    public int max_health;
    private int current_health;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        setPlayerHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void take_damage(int damage)
    {
        if(current_health > 0)
        {
            Debug.Log("Player take_damage()");
            current_health -= damage;
            setPlayerHealthText();
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

    private void setPlayerHealthText()
    {
        FindObjectOfType<UIHandler>().SetPlayerHealth(current_health);
    }
}
