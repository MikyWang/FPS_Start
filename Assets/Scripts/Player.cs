using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Transform m_transform;

    public int m_life = 5;
    public LayerMask m_layer;
    public Transform m_fx;
    public AudioClip m_audio;
    float m_shootTimer = 0;

    CharacterController m_ch;
    Transform m_muzzlePoint;


    float m_movSpeed = 3f;
    float m_gravity = 2.0f;
    Transform m_camTransform;
    Vector3 m_camRot;
    float m_camHeight = 1f;

    private void Start()
    {
        m_transform = transform;
        m_ch = GetComponent<CharacterController>();

        m_camTransform = Camera.main.transform;
        m_camTransform.position = m_transform.TransformPoint(0, m_camHeight, 0);

        m_camTransform.rotation = m_transform.rotation;
        m_camRot = m_camTransform.eulerAngles;

        m_muzzlePoint = m_camTransform.Find("M16/weapon/muzzlepoint").transform;

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if (m_life <= 0)
        {
            return;
        }
        Control();

        m_shootTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && m_shootTimer <= 0)
        {
            m_shootTimer = 0.1f;
            GetComponent<AudioSource>().PlayOneShot(m_audio);
            GameManager.Instance.setAmmo(1);

            RaycastHit info;
            bool hit = Physics.Raycast(m_muzzlePoint.position, m_camTransform.TransformDirection(Vector3.forward), out info, 100, m_layer);
            if (hit)
            {
                if (info.transform.tag.CompareTo("Enemy") == 0)
                {
                    Enemy enemy = info.transform.GetComponent<Enemy>();
                    enemy.OnDamage(1);
                }

                Instantiate(m_fx, info.point, info.transform.rotation);
            }
        }

    }

    public void OnDamage(int damage)
    {
        m_life -= damage;
        GameManager.Instance.SetLife(m_life);
        if (m_life <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Control()
    {
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        m_camRot.x -= rv;
        m_camRot.y += rh;
        m_camTransform.eulerAngles = m_camRot;

        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0;
        camrot.z = 0;
        m_transform.eulerAngles = camrot;

        Vector3 motion = Vector3.zero;

        motion.x = Input.GetAxis("Horizontal") * m_movSpeed * Time.deltaTime;
        motion.z = Input.GetAxis("Vertical") * m_movSpeed * Time.deltaTime;

        motion.y -= m_gravity * Time.deltaTime;
        m_ch.Move(m_transform.TransformDirection(motion));

        m_camTransform.position = m_transform.TransformPoint(0, m_camHeight, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Spawn.tif");
    }
}
