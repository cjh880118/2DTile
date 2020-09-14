using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("월드 포지션 : " + transform.position.x + "  ,   " + transform.position.y);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y));
        Debug.Log("스크린 포지션 : " + screenPos.x + "  ,   " + screenPos.y);
        Vector3 viewPos = Camera.main.WorldToViewportPoint(new Vector3(transform.position.x, transform.position.y));
        Debug.Log("뷰 포지션 : " + viewPos.x + "  ,   " + viewPos.y);
    }

    // Update is called once per frame
    void Update()
    {
     

    }
}
