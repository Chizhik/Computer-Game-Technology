using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public float speed;
    public GameObject target; //pursue
    public GameObject enemy; //flee

    private Rigidbody myrb;
    private Rigidbody tgRigid;
    private Rigidbody enRigid;
    private Vector3 northWall;
    private Vector3 southWall;
    private Vector3 westWall;
    private Vector3 eastWall;

	// Use this for initialization
	void Start () {

        myrb = GetComponent<Rigidbody>();
        tgRigid = target.GetComponent<Rigidbody>();
        enRigid = enemy.GetComponent<Rigidbody>();
        northWall = new Vector3(0, 0, 10);
        southWall = new Vector3(0, 0, -10);
        westWall = new Vector3(-10, 0, 0);
        eastWall = new Vector3(10, 0, 0);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 v = myrb.velocity;
        Vector3 seekVel = pursuit(tgRigid.position, tgRigid.velocity, myrb.position);
        Vector3 fleeVel = evade(enRigid.position, enRigid.velocity, myrb.position);

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

    private Vector3 pursuit(Vector3 tgPos, Vector3 tgVel,  Vector3 mypos)
    {
        float dist = (tgPos - mypos).magnitude;
        //float T = dist / speed;
        float T = Time.fixedDeltaTime * dist / speed;
        Vector3 futurePosition = tgPos + tgVel * T;
        Vector3 seekVel = futurePosition - mypos;
        //Vector3 seekVel = tgPos - mypos;
        seekVel = seekVel / seekVel.sqrMagnitude;
        return seekVel;
    }

    private Vector3 evade(Vector3 enPos, Vector3 enVel, Vector3 mypos)
    {
        float dist = (enPos - mypos).magnitude;
        //float T = dist / speed;
        float T = Time.fixedDeltaTime * dist / speed;
        Vector3 futurePosition = enPos + enVel * T;
        Vector3 fleeVel = mypos - futurePosition;
        //Vector3 fleeVel = mypos - enPos;
        fleeVel = fleeVel / fleeVel.sqrMagnitude;
        return fleeVel;
    }

}
