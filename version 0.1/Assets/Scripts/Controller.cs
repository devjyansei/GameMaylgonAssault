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
        float pitch = pitchDueToPosition + pitchDueToControl;// y ekseninde pozisyonumuz her 1 arttýgýnda,x Rotasyonumuzda positionPitchFactor katý kadar artacak.
                                                        // Yani sadece yukarý aþaðý yön tuþlarýyla hem rotasyonu hemde pozisyonu deðiþtiriyoruz.
                                                        // oyun penceresinde sürekli rotasyon ve poziyon ile oynayarak kaç katsayý vereceðimizi bulabiliriz.
                                                        //+ yDir * controlPitchFactor ise ekstra yalpalama etkisi katýyor.

        float yaw = transform.localPosition.x * positionYawFactor; // x ekseninde pozisyonumuza göre rotasyonumuzu deðiþtiriyoruz.

        float roll = xDir * controllRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
      //  transform.localRotation = Quaternion.Euler(pitch, yaw, roll);  Nesnemizi yerelrotasyonda hangi rotasyona dönmesini istediðimizi yazýyoruz.
    }
    private void PositionMovement()
    {
        xDir = Input.GetAxis("Horizontal");
        yDir = Input.GetAxis("Vertical");

        float xOffset = xDir * Time.deltaTime * speed;  // Giriþi hýz ve zaman ile stabilleþtirdik.
        float yOffset = yDir * Time.deltaTime * speed;

        float rawXPos = transform.localPosition.x + xOffset;    // stabil giriþi yeni pozisyonumuz haline getirdik.
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);  // yeni pozisyonumuzu kýsýtladýk.
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
