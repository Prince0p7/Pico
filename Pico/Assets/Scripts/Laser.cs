using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Laser : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LineRenderer lr;
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject startVFX;
    [SerializeField] GameObject endVFX;
    float _damage = 20;
    float _damageGiven;
    List<ParticleSystem> particles = new List<ParticleSystem>();
    public int Kills;
    [SerializeField] TextMeshProUGUI killText;
    void Start()
    {
        FillList();
        DisableLaser();
    }
    public void LaserUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EnableLaser();
        }
        if(Input.GetMouseButton(0))
        {
            UpdateLaser();
        }
        if(Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }
        killText.text = Kills.ToString();
    }
    void EnableLaser()
    {
        lr.enabled = true;
        for(int i = 0; i< particles.Count; i++)
        {
            particles[i].Play();
        }
    }
    void UpdateLaser()
    {
        var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        lr.SetPosition(0, (Vector2) FirePoint.position);
        startVFX.transform.position = (Vector2)FirePoint.position;
        lr.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2  )transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);

        if(hit)
        {
            lr.SetPosition(1, hit.point);
            EnemyManager damage = hit.transform.GetComponent<EnemyManager>();
            if (damage != null)
            {
                damage.TakeDamage(_damage);
                _damageGiven += _damage * Time.deltaTime;
                if (damage.isdead)
                {
                    Kills++;
                    Debug.Log(transform.name + " has Killed " + damage.name + "Damage Given :" + Mathf.RoundToInt(_damageGiven));
                    damage.Dead();
                }
            }
        }

        endVFX.transform.position = lr.GetPosition(1);
    }
    public void DisableLaser()
    {
        lr.enabled = false;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }
    void FillList()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
           var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
           var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }
}