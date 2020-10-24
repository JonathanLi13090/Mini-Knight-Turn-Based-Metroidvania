using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_health : MonoBehaviour
{
    public int total_lives;
    public int current_lives;
    public GameObject UI_handler;


    // Start is called before the first frame update
    void Start()
    {
        current_lives = total_lives;
        UI_handler = GameObject.FindGameObjectWithTag("UI_HANDLER");
        UI_handler.GetComponent<UIhandler>().SetPlayerHealth(current_lives);
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
        current_lives -= 1;
        if(current_lives > 0)
        {
            UI_handler.GetComponent<UIhandler>().SetPlayerHealth(current_lives);
            FindObjectOfType<Area>().RespawnPlayer();
        }
        else
        {
            Debug.Log("out of lives");
            SceneManager.LoadScene("Out_of_lives");
        }
    }
}
