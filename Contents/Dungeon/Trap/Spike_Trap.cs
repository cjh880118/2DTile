using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spike_Trap : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D boxCollider;
    int damage;

    public int Damage { get => damage; set => damage = value; }

    void Start()
    {
        damage = 10;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void SpikeOn()
    {
        boxCollider.enabled = true;
    }

    private void SkikeOff()
    {
        boxCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("TT");
    }

}
