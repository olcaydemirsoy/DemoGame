using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    CharacterController characterController;


    [Header("Character Move Settings")]
    [Tooltip("Karakterin h�z de�eridir.")]    public float speed;
    [Tooltip("Klavye kontrolleri i�in d�nme h�z� de�eridir.")]    public float rotationSpeed;
    [Tooltip("Mobil cihazlar i�in d�nme h�z� de�eridir.")]    public float touchSpeed;
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
        //Karakterin durmadan y�r�mesi i�in bu metoddaki iki fonskyion da kullan�labilir. Karakterin collider alg�lamas� i�in CharacterController kulland�m. Di�er se�enekte Rigidbody kullanmam gerekecekti.

        //transform.position += transform.forward * Time.deltaTime * speed;

        characterController.Move(transform.forward * Time.deltaTime * speed);
    }

    void CharacterMovementActionsWithKeyboard()
    {
        //Klavye ile y�nlendirme i�in bu metodu yazd�m. Input ayarlar�ndaki Horizontal ve Vertical ayarlar�na g�re hareket eder.

        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        movement = transform.forward * Z;
        transform.Rotate(Vector3.up * X, (rotationSpeed * Time.deltaTime)); //d�nme aksiyonu normal insan karakterleri gibi i�liyor. Birden arkas�n� d�nm�yor, rotationSpeed de�erine g�re y�n de�i�tiriyor
        characterController.Move(movement * speed * Time.deltaTime);
    }

    void CharacterMovementActionsForMobile()
    {
        // Mobil cihazlardaki dokunma aksiyonuna g�re �al��mas� i�in bu metodu yazd�m. touchCount, ekrana dokunuldu�u s�rece her ihtimalde 0'dan b�y�kt�r. 
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) //TouchPhase.Moved parmak hareketini al�r. 
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition; // Parma��n ekrandaki yer de�erini al�r
            transform.Rotate(0, touchDeltaPosition.x * touchSpeed, 0); // Parma��n ekrandaki yerine g�re karakteri d�nd�r�r.

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
