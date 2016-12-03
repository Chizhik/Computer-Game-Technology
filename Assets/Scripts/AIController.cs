using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public float speed;
    public float coef;
    public GameObject target; //pursue
    public GameObject enemy; //flee
	Transform obj;

    private Rigidbody myrb;

	// Use this for initialization
	void Start () {
		obj = GetComponent<Transform>();
        myrb = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 v = myrb.velocity;
        Vector3 targetDir = target.transform.position - myrb.position;
        targetDir = coef * targetDir / targetDir.sqrMagnitude;
        Vector3 enemyDir = enemy.transform.position - myrb.position;
        enemyDir = coef * enemyDir / enemyDir.sqrMagnitude;
        Vector3 movement = targetDir - enemyDir - v;
        myrb.AddForce(movement * speed);

	
	}

	void OnCollisionEnter (Collision col)
	{
		float force = 500;
		if(col.gameObject.name == "Bot Green") {
			//red collide with green
			Vector3 dir = col.contacts[0].point - transform.position;
			dir = -dir.normalized;
			myrb.AddForce(dir*force);
		} else if(col.gameObject.name == "Bot Red") {
			//green collide with red
			Vector3 dir = col.contacts[0].point - transform.position;
			dir = -dir.normalized;
			myrb.AddForce(dir*force);
		}
	}
}
