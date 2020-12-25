using System.Collections;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    /*private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }*/

    private void Update()
    {
        // destroy not-playing particle every 15 frame
        if (Time.frameCount % 15 == 0 /*&& !particle.isPlaying*/)
        {

            StartCoroutine(DestroyParticleSystem());

            //Destroy(gameObject);
        }
    }
    
    private IEnumerator DestroyParticleSystem()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
    
}

