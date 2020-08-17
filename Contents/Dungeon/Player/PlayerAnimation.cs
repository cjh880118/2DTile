using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator anim;
        public string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
        public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
        int lastDirection;
        bool isAtkPossible = true;

        public GameObject obj;
        GameObject[] objs = new GameObject[10];


        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 intput)
        {
            string[] directionArray = null;

            if (intput.magnitude < 0.01)
            {
                directionArray = staticDirections;
            }
            else
            {
                directionArray = runDirections;
                lastDirection = DirectionToIndex(intput);
            }

            anim.Play(directionArray[lastDirection]);
          
        }

        private int DirectionToIndex(Vector2 _dir)
        {
            Vector2 norDir = _dir.normalized;
            float step = 360 / 8;
            float offset = step / 2;
            float angle = Vector2.SignedAngle(Vector2.up, norDir);
            angle += offset;

            if (angle < 0)
            {
                angle += 360;
            }

            float stepCount = angle / step;
            return Mathf.FloorToInt(stepCount);
        }
      
    }
}