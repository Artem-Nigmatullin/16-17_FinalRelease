using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private UI uI;
    [SerializeField] private CoinCollector _coinCollector;
    private Coroutine _loadingCoroutine;
    private Coroutine _updateCoroutine;

    private void Awake()
    {

        if (_loadingCoroutine != null)
            StopCoroutine(_loadingCoroutine);
        _loadingCoroutine = StartCoroutine(StartProcess());

    }
    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading...");



        _enemy.Initialize();
       // _spawner.DeleteCloneEnemy();


        yield return new WaitForSeconds(1);

        _loadingScreen.Hide();

    }
    private IEnumerator UpdateProcess()
    {
        
        yield return null;
    }

}
