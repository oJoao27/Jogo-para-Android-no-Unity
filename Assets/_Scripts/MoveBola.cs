using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class MoveBola : MonoBehaviour
{
    Rigidbody rb;
    public float velocidade, forcaPulo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        #if UNITY_EDITOR
            AplicarTorque
                (new Vector3(
                    0,
                    0,
                    Input.GetAxis("Horizontal") * -velocidade
                    )
                );

            
        #endif
        #if UNITY_ANDROID
            AplicarTorque
                (new Vector3(
                    Input.acceleration.y * velocidade,
                    0,
                    Input.acceleration.x * -velocidade
                    )
                );
        #endif
    }

    void AplicarTorque(Vector3 direcao) {
        rb.AddTorque(direcao);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(new Vector3(0,1,0) * forcaPulo,ForceMode.Impulse);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PontoFinal"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}