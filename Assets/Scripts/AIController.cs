using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public float speed;
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
        targetDir = targetDir / targetDir.sqrMagnitude;
        Vector3 enemyDir = enemy.transform.position - myrb.position;
        enemyDir = enemyDir / enemyDir.sqrMagnitude;
        Vector3 movement = (targetDir - enemyDir).normalized;
        movement -= v;
        myrb.AddForce(movement * speed);

	
	}
}
