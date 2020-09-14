using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D[] hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Debug.Log("마우스 입력 포지션 : " + Input.mousePosition.x + "  ,   " + Input.mousePosition.y);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            Debug.Log("월드 포지션 : " + worldPos.x + "  ,   " + worldPos.y);
            Vector3 viewPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            Debug.Log("뷰 포지션 : " + viewPos.x + "  ,   " + viewPos.y);


            RaycastHit2D hit = Physics2D.Raycast(worldPos, transform.forward, 15);
            if (hit)
            {
                Debug.Log(hit.collider.name);
            }

        }
    }
}
