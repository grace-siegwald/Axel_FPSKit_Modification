using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

[RequireComponent(typeof(Collider))] 
[RequireComponent(typeof(Rigidbody))]
public class AmmoBox : MonoBehaviour
{
    [Header("Pickup Settings")]
    [AmmoType]
    public int ammoType;
    public int amount;

    [Header("Physics Settings")]
    private Rigidbody rb;
    private bool hasLanded;
    private GameObject collisionTrigger;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Creates a child object to handle the trigger collider
        collisionTrigger = new GameObject("TriggerCollider");
        collisionTrigger.transform.SetParent(transform);
        collisionTrigger.transform.localPosition = Vector3.zero;
        collisionTrigger.transform.localRotation = Quaternion.identity;
        collisionTrigger.layer = LayerMask.NameToLayer("PlayerCollisionOnly");
        BoxCollider triggerCollider = collisionTrigger.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector3(2.0f, 2.0f, 2.0f);

    }

    void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    // Detects when the ammo hit's the ground!
    void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
        {
            hasLanded = true;
            rb.angularVelocity = Vector3.zero;
        }
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
