using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[RequireComponent(typeof(Rigidbody))]
public class ShootingBehavior : MonoBehaviour {

	public float fireRate = 0;
	public float damage = 10;
	public LayerMask whatToHit;
	public Transform firePoint;
	public GameObject BulletTrailPrefab;
	public float effectSpawnRate = 10;
	public Animator animator;
	public KeyCode fireKey = KeyCode.Space;
	/*public GameObject bullet;
	public GameObject shooter;
	public GameObject bulletPrefab;
*/
	private float timeToFire = 0;

	private float timeToSpawnEffect = 0;


	void Awake(){
		//firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("no firepoint");
		}
	}
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate () {
		if (fireRate == 0) {//single shot
			if (Input.GetKeyDown (fireKey)) {
				animator.SetTrigger ("PlayerShootTrigger");
				Shoot ();
			}
		} else {
			if(Input.GetKeyDown (fireKey) && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;//define fire rate as delays to next time to fire
				Shoot ();
			}
		} 

	/*	if (Input.GetKey(fireKey)) {
			Shot newBullet = GameObject.Instantiate (Shot);
			newBullet.transform.parent = shooter.transform;
		}*/
	}

	void Shoot(){
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firePointDirection = new Vector2 (firePoint.position.x+10, firePoint.position.y);//forward
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, firePointDirection, 100, whatToHit);
		if (Time.time > timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}
		Debug.DrawLine (firePointPosition, firePointDirection, Color.green);
		if (hit.collider != null) {
			Debug.Log ("we hit " + hit.collider.name + " and did " + damage + " damage");
		}
	}

	void Effect (){
		GameObject bullet = Instantiate (BulletTrailPrefab, (firePoint.position + (firePoint.forward * 2)), firePoint.rotation) as GameObject;
		bullet.layer = LayerMask.NameToLayer ("Characters");
	}
}
