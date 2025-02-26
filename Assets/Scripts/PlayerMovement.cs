/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Player m_groundSensor;
    private bool m_grounded = false;
    private bool facingRight = true;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Player>();
    }

    void Update()
    {
        if (m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (inputX < 0 && facingRight)
        {
            FlipCharacter();
        }

            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        if (Input.GetKeyDown("space") && m_groundSensor.State())
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }


    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f); 
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Player m_groundSensor;
    private bool m_grounded = false;
    private bool facingRight = true;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Player>();
    }

    void Update()
    {
        // Проверка состояния сенсора земли
        m_grounded = m_groundSensor.State();
        m_animator.SetBool("Grounded", m_grounded);

        // Получение входного значения по оси X
        float inputX = Input.GetAxis("Horizontal");

        // Переворачивание персонажа в зависимости от направления движения
        if (inputX > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (inputX < 0 && facingRight)
        {
            FlipCharacter();
        }

        // Установка скорости персонажа
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // Установка параметров для анимаций
        m_animator.SetFloat("Speed", Mathf.Abs(inputX)); // Устанавливаем скорость движения для анимации

        // Установка скорости в воздухе для анимации
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // Обработка прыжка
        if (Input.GetKeyDown(KeyCode.Space) && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.z *= -1; // Инвертируем масштаб по оси X
        transform.localScale = scale;
        Debug.Log("Character flipped. Facing right: " + facingRight);
    }
}
