using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform m_transform;
    public int m_life = 5;

    CharacterController m_ch;

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

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if (m_life <= 0)
        {
            return;
        }
        Control();
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
