using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public float speed;
    public float coef;
    public GameObject target; //pursue
    public GameObject enemy; //flee

    private Rigidbody myrb;

	// Use this for initialization
	void Start () {

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
}
