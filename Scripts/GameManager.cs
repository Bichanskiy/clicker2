using Click_Button;
using Configs.LevelConfig;
using End_Level_Window;
using Enemy;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Health_bar;
using SceneManagement;
using SkillsSystem;
using UnityEngine;





public class GameManager : EntryPoint
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    [SerializeField] private LevelsConfig _levelsConfig;
    
    [SerializeField] private Timer.Timer _timer;
    [SerializeField] private SkillsConfig _skillsConfig; 
    
    private SkillSystem _skillSystem;
    
    private GameEnterParams _gameEnterParams;
    private SaveSystem _saveSystem;


    private const string SCENE_LOADER_TAG = "SceneLoader";

    public override void Run(SceneEnterParams enterParams)
    {
        _saveSystem = FindFirstObjectByType<SaveSystem>();
        if (enterParams is not GameEnterParams gameEnterParams)
        {
            Debug.LogError("troubles with enter params");
            return;
        }
        
        _gameEnterParams = gameEnterParams;
        
        _clickButtonManager.Initialize();
        _enemyManager.Initialize(_healthBar, _timer);
        
        _endLevelWindow.Initialize();
        var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
        _skillSystem = new(openedSkills, _skillsConfig, _enemyManager);
        
        _clickButtonManager.OnClicked += () =>
        {
            _enemyManager.DamageCurrentEnemy(1.0f);
            _skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
        };
        _endLevelWindow.OnRestartClicked += RestartLevel;
        _enemyManager.OnLevelPassed += LevelPassed;
        
        StartLevel();
    }

    private void LevelPassed(bool isPassed)
    {
        if (isPassed)
        {
            TrySaveProgress();

            _endLevelWindow.ShowWinWindow();
        }
        else
        {
            _endLevelWindow.ShowLoseWindow();
        }
        
    }

    private void TrySaveProgress()
    {
        var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
        if (_gameEnterParams.Location == progress.CurrentLocation && _gameEnterParams.Level == progress.CurrentLevel)
        {
            var maxLevel = _levelsConfig.GetMaxLevelOnLocation(progress.CurrentLocation);
            if (progress.CurrentLevel >= maxLevel)
            {
                progress.CurrentLevel = 1;
                progress.CurrentLocation++;
            }
            else
            {
                progress.CurrentLevel++;
            }
            _saveSystem.SaveData(SavableObjectType.Progress);
        }
    }

    private void StartLevel()
    {
        var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);
        
        _enemyManager.StartLevel(levelData);
    }

    private void RestartLevel()
    {
        var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        sceneLoader.LoadGameplayScene(_gameEnterParams);
    }


}

