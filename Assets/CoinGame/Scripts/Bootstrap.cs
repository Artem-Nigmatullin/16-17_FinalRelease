using System.Collections;
using UnityEngine;

public class Bootstrap : SettingsMonoBehavior
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LoadingScreen _loadingScreen;
    private Coroutine _loadingCoroutine;
    private Coroutine _updateCoroutine;


    private void StartGameSettings()
    {
    
        ScalableBufferManager.ResizeBuffers(0.85f, 0.85f);
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

    }
    private void Awake()
    {
        StartGameSettings();

        if (_loadingCoroutine != null)
            StopCoroutine(_loadingCoroutine);
        _loadingCoroutine = StartCoroutine(StartProcess());
    }
    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading...");

        _projectInstaller?.Initialize();

        yield return new WaitForSeconds(1);

        _loadingScreen.Hide();

    }
    private IEnumerator UpdateProcess()
    {
        yield return null;
    }

}
