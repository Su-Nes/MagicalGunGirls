using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAWARUDO : MonoBehaviour
{
    public KeyCode freezeKey = KeyCode.Space; // The key to toggle freezing
        public GameObject player; // Assign the player in the inspector
    
        private Dictionary<Rigidbody, (Vector3 velocity, Vector3 angularVelocity)> storedVelocities = new Dictionary<Rigidbody, (Vector3, Vector3)>();
        private bool isFrozen = false;
    
        void Update()
        {
            if (Input.GetKeyDown(freezeKey))
            {
                ToggleFreeze();
            }
        }
    
        void ToggleFreeze()
        {
            isFrozen = !isFrozen;
    
            Rigidbody[] allRigidbodies = FindObjectsOfType<Rigidbody>();
    
            foreach (Rigidbody rb in allRigidbodies)
            {
                if (rb.gameObject == player) continue; // Skip freezing the player
    
                if (isFrozen)
                {
                    // Store velocity & angular velocity, then freeze
                    storedVelocities[rb] = (rb.velocity, rb.angularVelocity);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }
                else
                {
                    // Restore velocity & angular velocity, then unfreeze
                    if (storedVelocities.ContainsKey(rb))
                    {
                        (Vector3 velocity, Vector3 angularVelocity) = storedVelocities[rb];
                        rb.isKinematic = false;
                        rb.velocity = velocity;
                        rb.angularVelocity = angularVelocity;
                    }
                }
            }
    
            if (!isFrozen)
            {
                storedVelocities.Clear(); // Clear stored velocities when unfreezing
            }
        }
    }
