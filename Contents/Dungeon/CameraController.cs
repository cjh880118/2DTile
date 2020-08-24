using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.UI.Event;
using System;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 originPos;
    private bool isShake;

    [SerializeField] private float smoothSpeed;
    [SerializeField] private float minX, maxX, minY, maxY;
    //Max Y 6 MInY -10  minX -8 MaxX 26
    // Start is called before the first frame update
    private void Start()
    {
        AddMessage();
    }

    void AddMessage()
    {
        Message.AddListener<CameraLimitMsg>(CameraLimit);
    }

    private void CameraLimit(CameraLimitMsg msg)
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        minX = msg.cameraLimit.Camera_Min_X;
        maxX = msg.cameraLimit.Camera_Max_X;
        minY = msg.cameraLimit.Camera_Min_Y;
        maxY = msg.cameraLimit.Camera_Max_Y;
    }

    public void CameraShake()
    {
        StartCoroutine(Shake(0.1f, 0.1f));
    }

    //private void CameraShake(CameraShakeMsg msg)
    //{
    //    StartCoroutine(Shake(0.1f, 0.1f));
    //}

    public IEnumerator Shake(float _amount, float _duration)
    {
        originPos = transform.position;
        float timer = 0;

        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * _amount + originPos;
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z), smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
            );
    }
}
