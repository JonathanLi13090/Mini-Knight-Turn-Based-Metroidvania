using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbing_spider_controller : MonoBehaviour
{
    public float wall_check_distance;
    public float move_distance;
    public GameObject self;
    public LayerMask what_is_wall;
    public LayerMask what_is_player;
    public float attack_range;
    public bool going_up = true;
    public Transform attack_point;
    public int attack_damage;
    public bool Die_after;

    // Start is called before the first frame update
    public void Start()
    {
        groundTileMap = GameObject.FindGameObjectWithTag("ground").GetComponent<UnityEngine.Tilemaps.Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public UnityEngine.Tilemaps.Tilemap groundTileMap;
    public Transform Player;

    public void Move()
    {
        if (GetComponent<enemy_damage>().isDead) return;

        List<Node> path = FindPath.FindEnemyPath(groundTileMap, transform.position, GameObject.FindGameObjectWithTag("player").transform.position);
        Vector2 moveDirection = path[path.Count - 1].pos - (Vector2Int)UtilityTilemap.GetGridPos(groundTileMap, transform.position);
        //returns list of nodes, last node is destination

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack_point.position, attack_range, what_is_player);
        if (hitPlayer.Length > 0)
        {
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<Player_health>().take_damage(attack_damage);
            }
        }
        if (going_up == true)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, wall_check_distance, what_is_wall);
            if (!hitInfo)
            {
                transform.Translate(0, move_distance, 0);
            }
            else
            {
                Flip();
            }
        }
        else if (going_up == false)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, wall_check_distance, what_is_wall);
            if (!hitInfo)
            {
                transform.Translate(0, -move_distance, 0);
            }
            else
            {
                Flip();
            }
        }

    }

    void Flip()
    {
        going_up = !going_up;
        Vector2 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }
}
