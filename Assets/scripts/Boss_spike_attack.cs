using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_spike_attack : MonoBehaviour
{
    public int AttackStartDelay = 2;
    public Transform[] Spikes;
    private int moveSequence = 0;
    public LayerMask what_is_player;

    public void Move()
    {
        if(AttackStartDelay <= 0)
        {
            //player moves, spikes do nothing
            if (moveSequence <= 1)
            {

            }
            //spikes appear in ground
            else if (moveSequence == 2)
            {
                foreach(Transform spike in Spikes)
                {
                    spike.gameObject.SetActive(true);
                }
            }
            //spikes pop up
            else if (moveSequence == 3)
            {
                foreach (Transform spike in Spikes)
                {
                    spike.position += new Vector3(0, 1, 0);
                    Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(spike.position, 0.1f, what_is_player);
                    if (hitPlayer.Length > 0)
                    {
                        foreach (Collider2D player in hitPlayer)
                        {
                            player.GetComponent<Player_health>().take_damage(1);
                        }
                    }
                }
            }
            //spikes go away
            else if (moveSequence == 4)
            {
                foreach (Transform spike in Spikes)
                {
                    spike.position += new Vector3(0, -1, 0);
                    spike.gameObject.SetActive(false);
                }
            }
            moveSequence += 1;
            moveSequence %= 5;
        }
        else
        {
            AttackStartDelay -= 1;
        }
    }

    public void Kill()
    {
        foreach (Transform spike in Spikes)
        {
            spike.gameObject.SetActive(false);
        }
        Destroy(gameObject); 
    }
}
