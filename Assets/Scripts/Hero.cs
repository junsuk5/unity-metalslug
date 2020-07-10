using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public GameObject Bullet;

    bool jump = false, attack = false;

	// Use this for initialization
	void Start () {

        Screen.SetResolution(960, 640, true);

        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);
	}

    void MoveLeft()
    {
        transform.Translate(Vector3.left * 6.0f * Time.deltaTime);
        gameObject.GetComponent<tk2dSprite>().FlipX = true;
        if (!attack) gameObject.GetComponent<tk2dSpriteAnimator>().Play("Walk");
    }

    void MoveRight()
    {
        transform.Translate(Vector3.right * 6.0f * Time.deltaTime);
        gameObject.GetComponent<tk2dSprite>().FlipX = false;
        if (!attack) gameObject.GetComponent<tk2dSpriteAnimator>().Play("Walk");
    }

    // Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.LeftArrow)) MoveLeft();
        else if (Input.GetKey(KeyCode.RightArrow)) MoveRight();
        else
        {
            if (!attack) gameObject.GetComponent<tk2dSpriteAnimator>().Play("Idle");
        }

        if (!jump && Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000f);
            jump = true;
        }

        if (!attack && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AudioSource>().Play();
            gameObject.GetComponent<tk2dSpriteAnimator>().Play("Attack");
            GameObject temp = Instantiate(Bullet, new Vector3(transform.position.x + (!gameObject.GetComponent<tk2dSprite>().FlipX ? 0.3f : -0.3f), transform.position.y+1.4f, transform.position.z+1), Quaternion.identity) as GameObject;
            temp.GetComponent<Bullet>().TargetName = "Enemy";
            temp.GetComponent<tk2dSprite>().FlipX = gameObject.GetComponent<tk2dSprite>().FlipX;
            attack = true;
        }

        if (attack)
        {
            if (!gameObject.GetComponent<tk2dSpriteAnimator>().IsPlaying("Attack")) attack = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Ground")
        {
            jump = false;
        }
    }
}
