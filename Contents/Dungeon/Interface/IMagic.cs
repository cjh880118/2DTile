using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public abstract class IMagic : MonoBehaviour
    {
        protected Vector3 dirVec;
        private int damage;
        private float moveSpeed;

        public int Damage { get => damage; set => damage = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

        public virtual void ShootMagic(int _dir)
        {
            dirVec = IntToDirectVector(_dir);
        }

        public void FixedUpdate()
        {
            if (dirVec != null)
                transform.Translate(dirVec * moveSpeed * Time.deltaTime);
        }

        Vector3 IntToDirectVector(int _lastDir)
        {
            Vector3 result = Vector3.zero;
            switch (_lastDir)
            {
                case 0:
                    return result = new Vector3(0, 1, 0);
                case 1:
                    return result = new Vector3(-1, 1, 0);
                case 2:
                    return result = new Vector3(-1, 0, 0);
                case 3:
                    return result = new Vector3(-1, -1, 0);
                case 4:
                    return result = new Vector3(0, -1, 0);
                case 5:
                    return result = new Vector3(1, -1, 0);
                case 6:
                    return result = new Vector3(1, 0, 0);
                default:
                    return result = new Vector3(1, 1, 0);
            }
        }

        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.collider.name);
        }


    }
}