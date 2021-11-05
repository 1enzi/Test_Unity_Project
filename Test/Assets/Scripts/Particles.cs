using System.Collections;
using System;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public static ParticleSystem particle;

    private void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
        particle.Pause();
    }

    public static void TurnOn()
    {
        particle.Play();
    }

    public static IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(3.0f);

        particle.Stop();
    }
}
