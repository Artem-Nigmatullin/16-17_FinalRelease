using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private UI uI;
    [SerializeField] private GameInitializer _gameInitializer;
    private Coroutine _loadingCoroutine;


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
        _spawner.Initialize();

        uI.Initialize();
        _gameInitializer.Initialize();

        yield return new WaitForSeconds(1);

        _loadingScreen.Hide();

    }

}
