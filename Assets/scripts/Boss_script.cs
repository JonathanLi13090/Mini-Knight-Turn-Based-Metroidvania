using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_script : MonoBehaviour
{
    public int max_health;
    public int current_health;
    public RawImage health_bar;
    private float health_bar_length;
    private float H;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        health_bar_length = health_bar.rectTransform.sizeDelta.x;
        H = health_bar.rectTransform.sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int attack_damage)
    {
        current_health -= attack_damage;
        current_health = Mathf.Clamp(current_health, 0, max_health);
        float healthPercent = (float)current_health / (float)max_health;
        health_bar.rectTransform.sizeDelta = new Vector2(health_bar_length * healthPercent, H);
        if(current_health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
