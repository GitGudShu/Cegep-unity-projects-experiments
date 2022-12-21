using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float RunSpeed = 10f;
    public float JumpForce = 10f;
    public int life = 3;

    private bool m_FacingRight = true;
    private Animator m_Anim;
    private Rigidbody2D m_RB;
    private bool m_UserJump;
    private bool m_Grounded;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private Transform FeetPosition;
    [SerializeField] private LayerMask GroundLayer;

    private Coroutine m_FlashCR;

    // Awake is called before Start (You want to define your private variable or reference here)
    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_RB = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_UserJump && m_Grounded)
            m_UserJump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        Vector2 t_FeetPos = new Vector2(FeetPosition.position.x, FeetPosition.position.y);
        m_Grounded = Physics2D.OverlapCircle(t_FeetPos, 0.2f, GroundLayer.value);
        m_Anim.SetBool("Grounded", m_Grounded);

        float movement = Input.GetAxis("Horizontal");
        bool shift = Input.GetButton("Shift");

        m_Anim.SetFloat("Speed", Mathf.Abs(shift ? movement * 2 : movement));
        m_RB.velocity = new Vector2(movement * (shift ? RunSpeed:WalkSpeed), m_RB.velocity.y);

        // Orientation
        if (movement < 0 && m_FacingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            m_FacingRight = false;
        }
        else if (movement > 0 && !m_FacingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            m_FacingRight = true;
        }

        // Jump
        if(m_UserJump && m_Grounded)
        {
            m_UserJump = false;
            m_RB.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            m_Anim.SetTrigger("Jump");
        }
    }

    public void TakeDamage(int a_Damage)
    {
        // todo reduire la vie
        life -= a_Damage;
        if(life <= 0)
            Die();
        // Visual flash
        if(m_FlashCR != null)
        {
            StopCoroutine(m_FlashCR);
        }

        m_FlashCR = StartCoroutine(CR_Flash());
    }

    IEnumerator CR_Flash()
    {
        for(int i = 0; i < 4; i++)
        {
            m_SpriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            m_SpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void Die() 
    {
        Debug.Log("Robot dead!");
        m_Anim.SetTrigger("Dead");
    }
}
