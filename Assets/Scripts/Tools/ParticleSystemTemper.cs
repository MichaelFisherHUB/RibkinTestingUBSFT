using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemTemper : MonoBehaviour {

    [SerializeField] private ParticleSystem _particleSystem;
    private ParticleSystem PartSystem
    {
        get
        {
            return _particleSystem ?? (_particleSystem = GetComponent<ParticleSystem>());
        }
    }

    public void Play()
    {
        StartCoroutine(PlayAndDestroy());
    }

    private IEnumerator PlayAndDestroy()
    {
        PartSystem.Play();
        yield return new WaitForSeconds(PartSystem.main.duration);
        Destroy(gameObject);
    }
}
