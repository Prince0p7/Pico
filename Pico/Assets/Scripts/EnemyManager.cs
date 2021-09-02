using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    float health;
    float HealthMultiplier = 5;
    float MaxHealth = 50;
    float lastTimeAttacked;
    [SerializeField] Slider HealthBar;
    [HideInInspector] public bool isdead;
    SpriteRenderer sprite;
    AIPath aiPath;
    AIDestinationSetter destinationSetter;
    Transform _Player;
    public float ActivateRadius;
    public List<Transform> PlayerList = new List<Transform>();
    [SerializeField] Transform waypoints;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        aiPath = GetComponentInParent<AIPath>();
        destinationSetter = GetComponentInParent<AIDestinationSetter>();
        health = MaxHealth;
    }
    void Update()
    {
        EnemyHealthBar();
        PlayerFinding();
        EnemyAI();
    }
    void EnemyAI()
    {
        if (aiPath.desiredVelocity.x >= .01f)
        {
            sprite.flipX = false;
        }
        else if (aiPath.desiredVelocity.x <= .01f)
        {
            sprite.flipX = true;
        }
        destinationSetter.target = _Player;
    }
    void PlayerFinding()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, ActivateRadius);
        foreach (Collider2D c in col)
        {
            if (c.GetComponent<Player>() != null)
            {
                Debug.DrawRay((Vector2)transform.position, (Vector2)c.transform.position - (Vector2)transform.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2)c.transform.position - (Vector2)transform.position);
                if (hit)
                {
                    Player P = hit.collider.GetComponent<Player>();
                    if (P != null)
                    {
                        if (!PlayerList.Contains(P.transform))
                        {
                            PlayerList.Add(P.transform);
                        }

                        if (PlayerList.Contains(P.transform))
                        {
                            if (P.Freeze)
                            {
                                PlayerList.Remove(P.transform);
                            }
                        }
                    }
                }
            }
        }

        if (PlayerList.Count > 0)
        {
            _Player = GetClosestPlayer(PlayerList);
        }
        else
        {
            _Player = transform.parent;
        }
    }
    IEnumerator _PlayerSetter()
    {
        _Player = transform.parent;
        yield return new WaitForSeconds(2f);
        _Player = waypoints.GetChild(Random.Range(0,1));
    }
    Transform GetClosestPlayer(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    void EnemyHealthBar()
    {
        if (health == MaxHealth) lastTimeAttacked = 0;
        if (health < MaxHealth) HealthRegeneration();
        lastTimeAttacked = Mathf.Clamp(lastTimeAttacked, 0, 3);
        health = Mathf.Clamp(health, 0, MaxHealth);
        HealthBar.value = health / MaxHealth;
        if (health <= 0) isdead = true;
    }
    public void Dead()
    {
        Destroy(transform.parent.gameObject);
    }
    public void TakeDamage(float damage)
    {
        lastTimeAttacked = 0;
        health -= damage * Time.deltaTime;
    }
    void HealthRegeneration()
    {
        lastTimeAttacked += Time.deltaTime;
        if (health > 0)
        {
            if (lastTimeAttacked >= 3)
            {
                health += HealthMultiplier * Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null)
        {
            if (!_player.Freeze)
            {
                _player.Freeze = true;
                Debug.Log(collision.collider.name + " Has been infected");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ActivateRadius);
    }
}