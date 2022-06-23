using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    CharacterController characterController;


    [Header("Character Move Settings")]
    [Tooltip("Karakterin hýz deðeridir.")]    public float speed;
    [Tooltip("Klavye kontrolleri için dönme hýzý deðeridir.")]    public float rotationSpeed;
    [Tooltip("Mobil cihazlar için dönme hýzý deðeridir.")]    public float touchSpeed;
    private Vector3 movement;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        
        if (GameManager.gameManager.gameActive)
        {
            //CharacterMovementActionsWithKeyboard();
            CharacterMove();
            CharacterMovementActionsForMobile();
        }
    }



    void CharacterMove()
    {
        //Karakterin durmadan yürümesi için bu metoddaki iki fonskyion da kullanýlabilir. Karakterin collider algýlamasý için CharacterController kullandým. Diðer seçenekte Rigidbody kullanmam gerekecekti.

        //transform.position += transform.forward * Time.deltaTime * speed;

        characterController.Move(transform.forward * Time.deltaTime * speed);
    }

    void CharacterMovementActionsWithKeyboard()
    {
        //Klavye ile yönlendirme için bu metodu yazdým. Input ayarlarýndaki Horizontal ve Vertical ayarlarýna göre hareket eder.

        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        movement = transform.forward * Z;
        transform.Rotate(Vector3.up * X, (rotationSpeed * Time.deltaTime)); //dönme aksiyonu normal insan karakterleri gibi iþliyor. Birden arkasýný dönmüyor, rotationSpeed deðerine göre yön deðiþtiriyor
        characterController.Move(movement * speed * Time.deltaTime);
    }

    void CharacterMovementActionsForMobile()
    {
        // Mobil cihazlardaki dokunma aksiyonuna göre çalýþmasý için bu metodu yazdým. touchCount, ekrana dokunulduðu sürece her ihtimalde 0'dan büyüktür. 
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) //TouchPhase.Moved parmak hareketini alýr. 
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition; // Parmaðýn ekrandaki yer deðerini alýr
            transform.Rotate(0, touchDeltaPosition.x * touchSpeed, 0); // Parmaðýn ekrandaki yerine göre karakteri döndürür.

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Diamond"))
        {
            GameManager.gameManager.score += GameManager.gameManager.scoreHitPoint;
            GameManager.gameManager.scroreText.text = "Score : " + GameManager.gameManager.score.ToString();
            Destroy(collision.gameObject);
        }
    }
}
