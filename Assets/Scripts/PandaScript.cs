using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PandaScript : MonoBehaviour {

	public int lane = 2;
	public bool moving = false;
	public Animator animator;
	// Use this for initialization

	public float speed = 3.5f;
	void Start(){
		animator = this.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
//		if (Input.GetKey(KeyCode.LeftArrow))
//		{
//			transform.position += Vector3.left * speed * Time.deltaTime;
//		}
//		if (Input.GetKey(KeyCode.RightArrow))
//		{
//			transform.position += Vector3.right * speed * Time.deltaTime;
//		}
		if (Input.GetKey(KeyCode.UpArrow) && lane > 1 && !moving)
		{
			moving = true;
			lane -= 1;
			animator.SetTrigger ("PlayerJumpTrigger");
			DOTween.To (() => transform.position, (y) => transform.position = y, new Vector3 (transform.position.x, transform.position.y + 2, 0), 1f)
				.OnComplete(() => 
					{
						moving = false;
					});
		}
		if (Input.GetKey(KeyCode.DownArrow) && lane < 3 && !moving)
		{
			moving = true;
			lane += 1;
			animator.SetTrigger ("PlayerJumpTrigger");
			DOTween.To (() => transform.position, (y) => transform.position = y, new Vector3 (transform.position.x, transform.position.y - 2, 0), 1f)
				.OnComplete(() => 
				{
					moving = false;
				});
		}
	}
}
