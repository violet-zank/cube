using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerMove inst = null;
    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.inst.transform.Translate(new Vector3(0,0,100*Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.inst.transform.Translate(new Vector3(0,0,-100*Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.inst.transform.Translate(new Vector3(-150*Time.deltaTime,0,0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.inst.transform.Translate(new Vector3(150*Time.deltaTime,0,0));
        }
    }
}
