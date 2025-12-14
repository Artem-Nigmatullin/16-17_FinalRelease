using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour, ISwitcher
{
    [SerializeField] private ParticleSystem _effect;
    private float minTime = 2.7f;
    private float maxTime = 3.4f;
    private Coroutine _randomEffectCoroutine;

    public ParticleSystem Effect { get => _effect; private set => _effect = value; }

    public void Off() => gameObject.SetActive(false);

    public void On() => gameObject.SetActive(true);

    public void PlayEffect()
    {
      
        if (_randomEffectCoroutine != null)
        {
            StopCoroutine(StartRandomToggleEffect()); // ← останавливаем ТУ, что работает
        }
        _randomEffectCoroutine = StartCoroutine(StartRandomToggleEffect());
     
    }
    public void StopEffect()
    {
        _effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);


        if (_randomEffectCoroutine == null)
            DevLog.Log("coroutine null");
        //Destroy(gameObject);
    }
    private IEnumerator StartRandomToggleEffect()
    {
        while (true)
        {
            float randomTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(randomTime);
           _effect.Stop();
            yield return new WaitForSeconds(randomTime);
            _effect.Play();
        }

    }
}
