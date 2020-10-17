using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_health : MonoBehaviour
{
    //public int max_health = 1;
    //private int current_health;

    // Start is called before the first frame update
    void Start()
    {
        //current_health = max_health;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void take_damage(int damage)
    {
        Die();

    }

    public void Die()
    {
        Debug.Log("player died");
        FindObjectOfType<Area>().RespawnPlayer();
    }

    
}
