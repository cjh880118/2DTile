using JHchoi.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovePoint : MonoBehaviour
{
    public delegate void EventHandler(object sender, MapMovePointType type);
    public event EventHandler MoveMap;
    public MapMovePointType type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Map Move");
            MoveMap?.Invoke(this, type);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Map Move");
            MoveMap?.Invoke(this, type);
        }
    }

}
