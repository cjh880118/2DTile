using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    Rigidbody2D rid;
    Animator ani;
    SpriteRenderer sprite;
    PhotonView pv;
    float moveH, moveV, MoveSpeed;
    public delegate void DieHandler();
    public DieHandler EventDie;

    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        pv = GetComponent<PhotonView>();
        string name = pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;
        GetComponentInChildren<TextMesh>().text = name;
        GetComponentInChildren<TextMesh>().color = pv.IsMine ? Color.blue : Color.black;
        gameObject.name = name;
        MoveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            moveH = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            moveV = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
            this.gameObject.transform.Translate(new Vector3(moveH, moveV));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Mace")
        {
            if (pv.IsMine)
            {
                EventDie?.Invoke();
            }

            GameObjectDestory();
            //this.gameObject.SetActive(false);
        }
    }

    public void GameObjectDestory()
    {
        if (pv.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
