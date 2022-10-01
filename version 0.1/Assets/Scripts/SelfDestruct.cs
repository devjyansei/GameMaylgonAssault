using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float particleDestroyDelay = 2f;
    void Start()
    {
        SelfDestroy();
    }
    public void SelfDestroy()
    {
        Destroy(gameObject, particleDestroyDelay);

    }
}
