using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
	Transform obj;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
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
            count = count + 1;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 11)
        {
            winText.text = "You Win!";
        }
    }

	void OnCollisionEnter (Collision col)
	{
		float force = 500;
		if(col.gameObject.name == "Bot Red")
		{
			count += 1;
			Vector3 dir = col.contacts[0].point - transform.position;
			dir = -dir.normalized;
			rb.AddForce(dir*force);
		} else if(col.gameObject.name == "Bot Green")
		{
			count -= 1;
			Vector3 dir = col.contacts[0].point - transform.position;
			dir = -dir.normalized;
			rb.AddForce(dir*force);
		}
		setCountText ();
	}
}