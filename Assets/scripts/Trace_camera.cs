using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_camera : MonoBehaviour
{
	public GameObject gameObject;
	float x1;
	float x2;
	float x3;
	float x4;
	void Start () {
	}


	void Update () {
		//空格键抬升高度
		if (Input.GetKey (KeyCode.Space))
		{
			transform.position =  new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z);
		}	 
 
		if (Input.GetKey (KeyCode.C))
		{
			transform.position =  new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z);
		}	


		//w键前进
		if(Input.GetKey(KeyCode.W))
		{
			this.gameObject.transform.Translate(new Vector3(0,(float)1.732*50*Time.deltaTime,50*Time.deltaTime));
		}
		//s键后退
		if(Input.GetKey(KeyCode.S))
		{
			this.gameObject.transform.Translate(new Vector3(0,(float)-1.732*50*Time.deltaTime,-50*Time.deltaTime));
		}
		//a键后退
		if(Input.GetKey(KeyCode.A))
		{
			this.gameObject.transform.Translate(new Vector3(-150*Time.deltaTime,0,0));
		}
		//d键后退
		if(Input.GetKey(KeyCode.D))
		{
			this.gameObject.transform.Translate(new Vector3(150*Time.deltaTime,0,0));
		}
	}
}
