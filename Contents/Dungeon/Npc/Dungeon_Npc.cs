using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public class Dungeon_Npc : NpcBase
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                if(collision.transform.position.x > this.transform.position.x)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }

    }
}