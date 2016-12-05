using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIController : MonoBehaviour {

    public float speed;
    public float coef;
    public GameObject target; //pursue
    public GameObject enemy; //flee
	public Text redCountText;
	public Text greenCountText;
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
		if(col.gameObject.name == "Bot Green") {
			//red collide with green

			int redCount = PlayerController.getRedCounts() + 1;
			int greenCount = PlayerController.getGreenCounts () - 1;
			PlayerController.setRedCounts(redCount);
			PlayerController.setRedCounts(greenCount);
			redCountText.text = "Red Count: " + redCount.ToString();
			greenCountText.text = "Green Count: " + greenCount.ToString();

			float force = 500;
			Vector3 dir = col.contacts[0].point - transform.position;
			dir = -dir.normalized;
			myrb.AddForce(dir*force);
		}

	}
}
