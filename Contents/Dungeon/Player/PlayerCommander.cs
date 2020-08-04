using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public class PlayerCommander : IPlayer
    {

        // Start is called before the first frame update
        Animator animator;
        void Start()
        {
            animator = this.gameObject.GetComponent<Animator>();
            base.InitIPlayer(10, 10, 3, 10);
        }


        protected override void Attack()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("공격");
            }
        }

        protected override void Skill_1()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("스킬1");
            }
        }

        protected override void Skill_2()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("스킬2");
            }
        }
    }
}