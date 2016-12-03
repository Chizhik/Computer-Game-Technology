using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public float speed;
    public GameObject target; //pursue
    public GameObject enemy; //flee

    private Rigidbody myrb;
    private Vector3 northWall;
    private Vector3 southWall;
    private Vector3 westWall;
    private Vector3 eastWall;

	// Use this for initialization
	void Start () {

        myrb = GetComponent<Rigidbody>();
        northWall = new Vector3(0, 0, 10);
        southWall = new Vector3(0, 0, -10);
        westWall = new Vector3(-10, 0, 0);
        eastWall = new Vector3(10, 0, 0);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 v = myrb.velocity;
        Vector3 seekVel = target.transform.position - myrb.position;
        seekVel = seekVel / seekVel.sqrMagnitude;
        Vector3 fleeVel = myrb.position - enemy.transform.position;
        fleeVel = fleeVel / fleeVel.sqrMagnitude;

        Vector3 northWallDir = new Vector3(0, 0, myrb.position.z - northWall.z);
        Vector3 southWallDir = new Vector3(0, 0, myrb.position.z - southWall.z);
        Vector3 westWallDir = new Vector3(myrb.position.x - westWall.x, 0, 0);
        Vector3 eastWallDir = new Vector3(myrb.position.x - eastWall.x, 0, 0);
        northWallDir = northWallDir / (northWallDir.sqrMagnitude * northWallDir.magnitude);
        southWallDir = southWallDir / (southWallDir.sqrMagnitude * southWallDir.magnitude);
        westWallDir = westWallDir / (westWallDir.sqrMagnitude * westWallDir.magnitude);
        eastWallDir = eastWallDir / (eastWallDir.sqrMagnitude * eastWallDir.magnitude);
        Vector3 desiredVel = (seekVel + fleeVel + northWallDir + southWallDir + westWallDir + eastWallDir).normalized * speed;
        Vector3 steering = desiredVel - v;
        //targetDir = targetDir / targetDir.sqrMagnitude;
        //Vector3 enemyDir = enemy.transform.position - myrb.position;
        //enemyDir = enemyDir / enemyDir.sqrMagnitude;
        //Vector3 movement = (targetDir - enemyDir).normalized;
        myrb.AddForce(myrb.mass * steering / Time.fixedDeltaTime);

	
	}
}
