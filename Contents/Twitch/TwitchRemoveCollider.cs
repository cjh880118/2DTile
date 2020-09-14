using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchRemoveCollider : MonoBehaviour
{
    public delegate void PlayerRemoveHandler(GameObject player, string name);
    public PlayerRemoveHandler playerRemoveMsg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerRemoveMsg?.Invoke(collision.gameObject, collision.name);
    }
}
