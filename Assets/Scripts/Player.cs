using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed = 1;
    public Transform mesh;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveTowards();
    }

    private void LateUpdate()
    {
        AdjustMeshHeight();
    }

    void AdjustMeshHeight()
    {
        Vector3 pos = mesh.position;
        pos.y = 1;
        mesh.position = pos;
    }

    private void MoveTowards()
    {
        Vector3 positionChange = new Vector3(InputManager.Instance.horizontal * speed * Time.fixedDeltaTime, 0, InputManager.Instance.vertical * speed * Time.fixedDeltaTime);
        rb.position += positionChange; //Change position
        if (positionChange == Vector3.zero) return;
        transform.forward = positionChange; //Change rotation
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Collectible col = other.gameObject.GetComponentInParent<Collectible>();
            if (!col) return;
            
            col.Die();
            LevelManager.Instance.AddScore(col.collectibleScore);
            Taptic.Light();


        };
    }
}
