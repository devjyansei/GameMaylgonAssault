using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyHitParticle;
    [SerializeField] GameObject enemyDeathParticle;
    GameObject parentObj;
    ScoreBoard scoreBoard;
    [SerializeField] int scoreValue;

    [SerializeField] float healthPoint;

    void Start()
    {
        AddRigidbody();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentObj = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void OnParticleCollision(GameObject other)
    {
        OnHit();

        if (healthPoint < 1)
        {
            OnEnemyKilled();
        }
    }



    private void OnEnemyKilled()
    {


        GameObject killVfx = Instantiate(enemyDeathParticle, transform.position, transform.rotation);
        killVfx.transform.parent = parentObj.transform;
        Destroy(gameObject);
    }

    private void OnHit()
    {
        healthPoint--;
        scoreBoard.UpdateScore(scoreValue);

        GameObject hitVfx = Instantiate(enemyHitParticle, transform.position, Quaternion.identity);
        hitVfx.transform.parent = parentObj.transform;
    }

    private void AddRigidbody()
    {
        Rigidbody enemyRb = gameObject.AddComponent<Rigidbody>();
        enemyRb.useGravity = false;
        enemyRb.mass = 5;
    }
    void SelfDestruct()
    {
        Destroy(gameObject, 1f);
    }
}
