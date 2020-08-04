using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    // Update is called once per frame
    Vector3 dirVec;
    public void CreateArrow(int _lastDir)
    {
        dirVec = IntToDirectVector(_lastDir);
    }

    private void FixedUpdate()
    {

        if(dirVec != null)
            this.gameObject.transform.Translate(dirVec * 5 * Time.deltaTime);
    }

    //public void IntToDirectVector(int _lastDir)
    //{


    //    vec2 = vec * 10;
    //    Debug.Log(vec2);
    //    Debug.Log("Nomal + " + vec2.normalized);
    //}

    public Vector3 IntToDirectVector(int _lastDir)
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
}
