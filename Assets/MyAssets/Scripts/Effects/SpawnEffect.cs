using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpawnTypeEffect
{
    blue=1,
    red=2
}
public class SpawnEffect : MonoBehaviour, ISwitcher
{
    [SerializeField] private ParticleSystem _blueEffect;
    [SerializeField] private ParticleSystem _redEffect;
    private Coroutine _spawnBlueEffectCoroutine;
    private Coroutine _spawnRedEffectCoroutine;

    public ParticleSystem BlueEffect { get => _blueEffect;private set => _blueEffect = value; }
    public ParticleSystem RedEffect { get => _redEffect;private set => _redEffect = value; }

    public void Off() => gameObject.SetActive(false);

    public void On() => gameObject.SetActive(true);

    public void StopBlueEffect()
    {
        _blueEffect.Stop();
        if (_spawnBlueEffectCoroutine != null)
        {
            StopCoroutine(StartBlueSpawnEffect());
        }
  
      
    }
    public void StopRedEffect()
    {
        _redEffect.Stop();
        if (_spawnRedEffectCoroutine != null)
        {
            StopCoroutine(StartRedSpawnEffect());
        }
   

    }
    public void PlayBlueEffect()
    {
        // _effect.Play();
        _spawnBlueEffectCoroutine = StartCoroutine(StartBlueSpawnEffect());


    }
    public void PlayRedEffect()
    {
        // _effect.Play();
        _spawnRedEffectCoroutine = StartCoroutine(StartRedSpawnEffect());


    }

    private IEnumerator StartBlueSpawnEffect()
    {
        _blueEffect.Play();
        yield break;
    }
    private IEnumerator StartRedSpawnEffect()
    {
        _redEffect.Play();
        yield break;
    }
}
