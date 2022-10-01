using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelReloadDelay;
    [SerializeField] ParticleSystem explosionParticle;
    void Start()
    {
       
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerEnter(Collider other)
    {

        StartGameOverSequence();
        Debug.Log("ontriggerenter");
    }

    private void StartGameOverSequence()
    {
        gameObject.GetComponent<Controller>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        explosionParticle.Play();
        Invoke("ReloadLevel", levelReloadDelay);
    }





    void ReloadLevel() 
    {
        int activescene = SceneManager.GetActiveScene().buildIndex;       
        SceneManager.LoadScene(activescene);
    }

}
