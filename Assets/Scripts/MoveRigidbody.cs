using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveRigidbody : MonoBehaviour
{
    [SerializeField] private float power;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var force = new Vector3(horizontal, vertical, 0f);
        _rb.AddForce(force * power);
    }
}