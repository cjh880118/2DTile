using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public class PlayerMovement : MonoBehaviour
    {
        private float moveH, moveV;
        [SerializeField] private float moveSpeed = 1.0f;


        private void FixedUpdate()
        {
            moveH = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            moveV = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            this.gameObject.transform.Translate(new Vector3(moveH, moveV));

            Vector2 intputDir = new Vector2(moveH, moveV);

            Vector2 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) - this.gameObject.transform.position;
            //FindObjectOfType<PlayerAnimation>().SetDirection(intputDir);//, direction);

        }
    }
}