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

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blueCount = 0;
		redCount = 0;
		greenCount = 0;
        blueCountText.text = "Blue Count: " + blueCount.ToString();
        greenCountText.text = "Green Count: " + greenCount.ToString();
        redCountText.text = "Red Count: " + redCount.ToString();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

        rb.AddForce(movement.normalized * speed);
        
    }

    public void setWinText()
    {
        if (blueCount >= 11)
        {
            winText.text = "You Win!";
        }
    }

	void OnCollisionEnter (Collision col)
	{
        if (col.gameObject.name == "Bot Red")
        {
            blueCount += 1;
            blueCountText.text = "Blue Count: " + blueCount.ToString();
        }
        if (col.gameObject.name == "Bot Red" || col.gameObject.name == "Bot Green")
        {
            float force = 500;
            Vector3 dir = transform.position - col.transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force);
            setWinText();
        }
        
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