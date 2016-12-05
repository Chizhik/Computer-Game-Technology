using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text blueCountText;
	public Text redCountText;
	public Text greenCountText;
    public Text winText;

    private Rigidbody rb;
	private static int blueCount;
	private static int redCount;
	private static int greenCount;
	Transform obj;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blueCount = 0;
		redCount = 0;
		greenCount = 0;
        setCountText();
        winText.text = "";
		blueCountText.text = "";
		obj = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up")) 
        {
            other.gameObject.SetActive(false);
            blueCount = blueCount + 1;
            setCountText();
        }
    }

    public void setCountText()
    {
        blueCountText.text = "Blue Count: " + blueCount.ToString();
		redCountText.text = "Red Count: " + redCount.ToString();
		greenCountText.text = "Green Count: " + greenCount.ToString();
        if (blueCount >= 11)
        {
            winText.text = "You Win!";
        }
    }

	void OnCollisionEnter (Collision col)
	{
		float force = 500;
		if(col.gameObject.name == "Bot Red")
		{
			blueCount += 1;
			redCount -= 1;
			bounceAndUpdateScore (col);
		} else if(col.gameObject.name == "Bot Green")
		{
			blueCount -= 1;
			greenCount += 1;
			bounceAndUpdateScore (col);
		}
	}

	void bounceAndUpdateScore(Collision col)
	{
		float force = 500;
		Vector3 dir = col.contacts[0].point - transform.position;
		dir = -dir.normalized;
		rb.AddForce(dir*force);
		setCountText ();
	}

	public static void setRedCounts(int r) 
	{
		redCount = r;
	}

	public static void setGreenCounts(int g) 
	{
		greenCount = g;
	}

	public static void setBlueCounts(int b) 
	{
		blueCount = b;
	}

	public static int getRedCounts() 
	{
		return redCount;
	}

	public static int getGreenCounts() 
	{
		return greenCount;
	}

	public static int getBlueCounts() 
	{
		return blueCount;
	}
}