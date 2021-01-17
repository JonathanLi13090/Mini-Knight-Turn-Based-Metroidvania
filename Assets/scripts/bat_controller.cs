using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bat_controller : MonoBehaviour
{
    //public float wall_check_distance;
    //public float move_distance;
    public GameObject self;
    public LayerMask what_is_wall;
    public LayerMask what_is_player;
    public float attack_range;
   // public bool going_right = true;
   // public Transform attack_point;
    public int attack_damage;
    public UnityEngine.Tilemaps.Tilemap groundTileMap;
    //public Transform Player;

    // Start is called before the first frame update
    public void Start()
    {
        groundTileMap = GameObject.FindGameObjectWithTag("ground").GetComponent<UnityEngine.Tilemaps.Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Move()
    {
        if (GetComponent<enemy_damage>().isDead) return;

        //if (Die_after)
        //{
        //    Debug.Log("die after move");
        //    self.GetComponent<enemy_damage>().TakeDamage(1, 3, false);
        //}

        List<Node> path = FindPath.FindEnemyPath(groundTileMap, transform.position, GameObject.FindGameObjectWithTag("player").transform.position);
        Vector2 moveDirection = path[path.Count - 1].pos - (Vector2Int) UtilityTilemap.GetGridPos(groundTileMap, transform.position);
        //returns list of nodes, last node is destination

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll((Vector2)transform.position + moveDirection, attack_range, what_is_player);
        if (hitPlayer.Length > 0)
        {
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<Player_health>().take_damage(attack_damage);
            }
        }
        transform.Translate(moveDirection);
    }
}
