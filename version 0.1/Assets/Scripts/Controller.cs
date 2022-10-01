using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float xDir , yDir;

    [Header("General Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 8f;
   
    [Header("Ekran pozisyonu tabanli ayarlar")]
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Kullanici input tabanli ayarlar")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controllRollFactor = -5f;

    [Header("LaserGun Array")]
    [SerializeField] GameObject[] lasers;


    void Start()
    {
        
    }

    void Update()
    {
        PositionMovement();
        RotationMovement();
        Fire();
        
    }

    

    void RotationMovement()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yDir * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;// y ekseninde pozisyonumuz her 1 artt�g�nda,x Rotasyonumuzda positionPitchFactor kat� kadar artacak.
                                                        // Yani sadece yukar� a�a�� y�n tu�lar�yla hem rotasyonu hemde pozisyonu de�i�tiriyoruz.
                                                        // oyun penceresinde s�rekli rotasyon ve poziyon ile oynayarak ka� katsay� verece�imizi bulabiliriz.
                                                        //+ yDir * controlPitchFactor ise ekstra yalpalama etkisi kat�yor.

        float yaw = transform.localPosition.x * positionYawFactor; // x ekseninde pozisyonumuza g�re rotasyonumuzu de�i�tiriyoruz.

        float roll = xDir * controllRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
      //  transform.localRotation = Quaternion.Euler(pitch, yaw, roll);  Nesnemizi yerelrotasyonda hangi rotasyona d�nmesini istedi�imizi yaz�yoruz.
    }
    private void PositionMovement()
    {
        xDir = Input.GetAxis("Horizontal");
        yDir = Input.GetAxis("Vertical");

        float xOffset = xDir * Time.deltaTime * speed;  // Giri�i h�z ve zaman ile stabille�tirdik.
        float yOffset = yDir * Time.deltaTime * speed;

        float rawXPos = transform.localPosition.x + xOffset;    // stabil giri�i yeni pozisyonumuz haline getirdik.
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);  // yeni pozisyonumuzu k�s�tlad�k.
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z); // hareket
    }
    void Fire()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateLasers();            
        }
        else
        {
            DeactivateLasers();
        }            
    }


    void ActivateLasers()
    {
        EmissionIsActive(true);
    }
    void DeactivateLasers()
    {
        EmissionIsActive(false);
    }


    void EmissionIsActive(bool isActive)
    {
        foreach (var item in lasers)
        {
            var emissionModule = item.GetComponent<ParticleSystem>().emission;

            emissionModule.enabled = isActive;
        }
    }
}
