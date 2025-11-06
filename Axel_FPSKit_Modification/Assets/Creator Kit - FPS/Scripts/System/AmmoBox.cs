using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Collider))] 
[RequireComponent(typeof(Rigidbody))]
public class AmmoBox : MonoBehaviour
{
    [Header("Pickup Settings")]
    [AmmoType]
    public int ammoType;
    public int amount;

    [Header("Physics Settings")]
    public float dropForce = 2.0f;
    private Rigidbody rb;
    private bool hasLanded;

    void Start()
    {
        rb = GetComponent<RigidBody>();
        rb.AddForce(Vector3.up * dropForce, ForceMode.Impulse);
    }
    void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerCollisionOnly");
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Controller c = other.GetComponent<Controller>();

        if (c != null)
        {
            c.ChangeAmmo(ammoType, amount);
            Destroy(gameObject);
        }
    }
}
