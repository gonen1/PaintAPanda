using UnityEngine;
using System.Collections;

public class MoveBulletTrail : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public int bulletSpeed=200;
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * bulletSpeed);
		Destroy (this.gameObject, 1);
	}
}
