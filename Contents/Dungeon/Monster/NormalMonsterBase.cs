using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JHchoi.Contents
{
    public abstract class NormalMonsterBase : MonsterBase
    {
        protected List<Node> NodeList = new List<Node>();
        int index = 0;
        private void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            if (NodeList == null || NodeList.Count <= index)
                return;

            Vector2 vec = new Vector2(NodeList[index].x, NodeList[index].y);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, vec, Time.deltaTime * monsterObject.data.MoveSpeed);
            Debug.Log(Vector2.Distance(gameObject.transform.position, new Vector2(NodeList[0].x, NodeList[0].y)));

            if (Vector2.Distance(gameObject.transform.position, new Vector2(NodeList[index].x, NodeList[index].y)) < 0.1f)
            {
                index++;
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Vector2Int startVec = new Vector2Int(grid.WorldToCell(transform.position).x, grid.WorldToCell(transform.position).y);
                Vector2Int targetVec = new Vector2Int(grid.WorldToCell(target.transform.position).x, grid.WorldToCell(target.transform.position).y);
                NodeList = astar2D.PathFinding(startVec, targetVec);
                index = 0;
                isTargetOn = true;
                anim.SetBool("isTargetOn", isTargetOn);

                //test = StartCoroutine(ReTarget());
            }
        }
        Coroutine test;
        IEnumerator ReTarget()
        {
            while (isTargetOn)
            {
                yield return new WaitForSeconds(1.5f);
                Vector2Int startVec = new Vector2Int(grid.WorldToCell(transform.position).x, grid.WorldToCell(transform.position).y);
                Vector2Int targetVec = new Vector2Int(grid.WorldToCell(target.transform.position).x, grid.WorldToCell(target.transform.position).y);
                NodeList = astar2D.PathFinding(startVec, targetVec);
                index = 0;
                isTargetOn = true;
                anim.SetBool("isTargetOn", isTargetOn);
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                //StopCoroutine(test);
                isTargetOn = false;
                anim.SetBool("isTargetOn", isTargetOn);
            }
        }
    }
}