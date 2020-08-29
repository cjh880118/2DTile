using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public abstract class MagicBase : MonoBehaviour
    {
        [SerializeField] public Vector3 dirVec;
        [SerializeField] int offset = 10;
        private int damage;
        private float moveSpeed;
        private float coolTime;

        public int Damage { get => damage; set => damage = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float CoolTime { get => coolTime; set => coolTime = value; }

        public virtual void InitMagic(int _damage, int _moveSpeed, float _coolTime)
        {
            damage = _damage;
            moveSpeed = _moveSpeed;
            coolTime = _coolTime;
        }

        public virtual void Shoot(Vector2 _dir)
        {
            dirVec = _dir.normalized;
            StartCoroutine(DestoryDelay());
        }

        public void FixedUpdate()
        {
            if (dirVec != null)
            {
                transform.position += dirVec * moveSpeed * Time.deltaTime;
            }

            if (this.gameObject.tag == "Bullet")
            {
                ViewportOutCheck();
            }
        }

        void ViewportOutCheck()
        {
            Vector2 viewPortPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPortPos.x < 0 ||
                viewPortPos.x > 1 ||
                viewPortPos.y < 0 ||
                viewPortPos.y > 1)
            {

                Destroy(this.gameObject);
            }
        }


        IEnumerator DestoryDelay()
        {
            yield return new WaitForSeconds(3.0f);
            Destroy(this.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Obstacle")
                Destroy(this.gameObject);
        }
    }
}