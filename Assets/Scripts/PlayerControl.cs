using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject spartic;
    private Rigidbody2D rb;
    private Animator anim;
    public LayerMask groundMask;
    public float speed;
    private float jumpForce = 8f;

    private float lDir;

    private bool pressed;

    private bool grounded;
    public bool walljump;
    private bool walljumpr;
    private bool walljumpl;
    private float wjDir;

    private List <GameObject> bodies = new List<GameObject>();
    public int lives = 3;
    public GameObject dead;
    public Vector2 spawnPos;

    //Inputs
    private float mHorizontal;
    private float mSpeed;
    private float mDir;

    //Sound Manager
    public AudioClip[] pSFX = new AudioClip[3];
    private AudioSource aS;

    public bool pCON = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aS = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        InputControl();
    }
    void FixedUpdate()
    {
        PlayerPhysics();
    }
    public void InputControl()
    {
        if(pCON)
        {
            mHorizontal = Input.GetAxis("Horizontal");
            mSpeed = Mathf.Clamp(mHorizontal * speed, -speed, speed);
            
            mDir = Mathf.Clamp(rb.velocity.x, -1, 1);


            PlayerCollisionDetection();

            if (mDir >= 0.99f || mDir <= -0.99f)
            {
                anim.SetFloat("Dir", mDir);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f && !pressed)
            {
                pressed = true;
                JumpMechanic();
            }
            if (Input.GetAxisRaw("Vertical") < 0.5f)
            {
                pressed = false;
            }


        }
    }
    private void PlayerPhysics()
    {
        if (grounded)
        {
            rb.velocity = new Vector2(mSpeed, rb.velocity.y);
            anim.SetFloat("mSpeed", Mathf.Abs(mSpeed));
            if(Mathf.Abs(rb.velocity.x) > 0.5f)
            {
                if (aS.clip != pSFX[0])
                {
                    aS.Stop();
                    aS.clip = pSFX[0];
                }
                if (!aS.isPlaying)
                {
                    aS.pitch = Random.Range(0.95f, 1.05f);
                    aS.Play();
                }
            }
            
        }
        if (!grounded)
        {
            rb.AddForce(new Vector2(mHorizontal * 4f, 0));
        }
    }
    private void JumpMechanic()
    {

        if (pressed && grounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if ((walljumpl || walljumpr) && pressed)
        {
            walljump = true;

            aS.Stop();
            aS.clip = pSFX[1];
            aS.pitch = Random.Range(0.95f, 1.05f);
            aS.Play();
            //anim.SetBool("WallCling", true);
            //Debug.Log(walljump);
        }
        if (walljump)
        {
            rb.velocity = new Vector2(speed * wjDir, jumpForce * 0.8f);
            walljump = false;
            //anim.SetBool("WallCling", false);

        }

    }

    public IEnumerator PlayerDeath()
    {
        lives -= 1;
        anim.SetBool("Death", true);
        pCON = false;
        //rb.bodyType = RigidbodyType2D.Static;
        aS.Stop();
        aS.clip = pSFX[2];
        aS.Play();
        yield return new WaitForSeconds(1f);
        if(lives <= 0)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            pCON = true;
            anim.SetBool("Death", false);
            gameObject.transform.position = spawnPos;
            lives = 3;
            ResetBodies();
            pCON = true;
        }
        else
        {
            //rb.bodyType = RigidbodyType2D.Dynamic;
            pCON = true;
            anim.SetBool("Death", false);
            GameObject body = Instantiate(dead) as GameObject;
            body.transform.position = rb.position;
            gameObject.transform.position = spawnPos;
            GameObject sP = Instantiate(spartic) as GameObject;
            sP.transform.position = transform.position;
            bodies.Add(body);
            yield return new WaitForSeconds(1f);
            Destroy(sP);
            
        }
        yield return null;
    }
    public void ResetBodies()
    {
        for(int i = 0; i < bodies.Count; i++)
        {
            Destroy(bodies[i]);
        }
        bodies.Clear();
    }

    

    private void PlayerCollisionDetection()
    {
        grounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.7f, 0.1f), 0f, groundMask);
        walljumpl = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.4f, gameObject.transform.position.y), new Vector2(0.2f, 0.8f), 0f, groundMask);
        walljumpr = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + 0.4f, gameObject.transform.position.y), new Vector2(0.2f, 0.8f), 0f, groundMask);

        if(walljumpl)
        {
            wjDir = 1f;
        }
        else if (walljumpr)
        {
            wjDir = -1f;
        }

        anim.SetBool("Grounded", grounded);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.7f, 0.1f));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x - 0.4f, gameObject.transform.position.y), new Vector2(0.2f, 0.8f));
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x + 0.4f, gameObject.transform.position.y), new Vector2(0.2f, 0.8f));
    }
}
