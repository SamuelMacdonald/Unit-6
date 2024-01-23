using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class part : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Partical();
    }
    void ShootAtPlayer()
    {
        

        
    }
    private IEnumerator Partical()
    {
        ParticleSystem part = GetComponent<ParticleSystem>();
        part.Play();
        yield return new WaitForSeconds(2);
    }

}
