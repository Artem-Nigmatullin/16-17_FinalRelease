using System.Collections;
using UnityEngine;


public class SpawnEffect : MonoBehaviour, ISwitcher
{
    [SerializeField] private ParticleSystem _blueEffect;
    [SerializeField] private ParticleSystem _redEffect;
    private Coroutine _BlueEffectCoroutine;
    private Coroutine _RedEffectCoroutine;

    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);

    public void StopBlueEffect()
    {
        _blueEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
   
    }
    public void StopRedEffect()
    {
        _redEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);      
    }
    public void PlayBlueEffect()
    {
        // _effect.Play();
        if (_BlueEffectCoroutine != null)
        {
            StopCoroutine(StartBlueSpawnEffect()); // ← останавливаем ТУ, что работает
        }
        _BlueEffectCoroutine = StartCoroutine(StartBlueSpawnEffect());

    }
    public void PlayRedEffect()
    {
        // _effect.Play();
        if (_RedEffectCoroutine != null)
        {
            StopCoroutine(StartRedSpawnEffect()); // ← останавливаем ТУ, что работает
        }
        _RedEffectCoroutine = StartCoroutine(StartRedSpawnEffect());
    }

    private IEnumerator StartBlueSpawnEffect()
    {
        _blueEffect.Play();
        yield return null;
    }
    private IEnumerator StartRedSpawnEffect()
    {
        _redEffect.Play();
        yield return null;
    }
}
