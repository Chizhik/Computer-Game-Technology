using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class AIController : MonoBehaviour {

    public float speed;
    public GameObject target; //pursue
    public GameObject enemy; //flee
    public GameObject obstacles;

    private Rigidbody myrb;
    private Rigidbody tgRigid;
    private Rigidbody enRigid;
    private Vector3[,] potentialBoard;
    private int gridSize = 400;

	// Use this for initialization
	void Start () {

        myrb = GetComponent<Rigidbody>();
        tgRigid = target.GetComponent<Rigidbody>();
        enRigid = enemy.GetComponent<Rigidbody>();

        potentialBoard = new Vector3[gridSize+1, gridSize+1];
        computePotentialForBoard(ref potentialBoard);

        //printPotentialBoard();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 v = myrb.velocity;
        Vector3 seekVel = pursuit(tgRigid.position, tgRigid.velocity, myrb.position);
        Vector3 fleeVel = evade(enRigid.position, enRigid.velocity, myrb.position);

        //Vector3 potDir = newPotential(myrb.position); //compute dynamically
        Vector3 potDir = potentialFromBoard(myrb.position);

        Vector3 desiredVel = (seekVel + fleeVel + 0.09f * potDir).normalized * speed;
        Vector3 steering = desiredVel - v;
        Vector3 final = myrb.mass * steering / Time.fixedDeltaTime;
        //final.y = 0f;
        myrb.AddForce(final);
	}

    private Vector3 newPotential(Vector3 pos)
    {
        Vector3 res = Vector3.zero;
        Vector3 closestPoint = Vector3.zero;
        Vector3 dir = Vector3.zero;
        foreach (BoxCollider obs in obstacles.GetComponentsInChildren<BoxCollider>())
        {
            closestPoint = obs.ClosestPointOnBounds(pos);
            dir = pos - closestPoint;
            res += dir / dir.sqrMagnitude;
        }
        return res;
    }

    private void computePotentialForBoard(ref Vector3[,] board)
    {
        for (int x = 0; x <= gridSize; ++x)
        {
            for (int z = 0; z <= gridSize; ++z)
            {
                board[x, z] = newPotential(new Vector3((x - gridSize / 2) * 20f / gridSize, 0.5f, (z - gridSize / 2) * 20f / gridSize)); // if point is inside of obstacle??
            }
        }
    }

    private Vector3 potentialFromBoard(Vector3 position)
    {
        int x = Convert.ToInt32(position.x * gridSize / 20f) + gridSize / 2;
        int z = Convert.ToInt32(position.z * gridSize / 20f) + gridSize / 2;
        return potentialBoard[x,z];
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

    private void printPotentialBoard()
    {
        using (var sw = new StreamWriter("output.txt"))
        {
            for (int x = 0; x <= 400; ++x)
            {
                for (int z = 0; z <= 400; ++z)
                {
                    sw.Write(Convert.ToString(potentialBoard[x, z]) + " ");
                }
                sw.Write("\n");
            }

            sw.Flush();
            sw.Close();
        }
    }

}
