using Game.ECS;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PlayerStatsWidget _playerStatsWidget;
        [SerializeField] private BuisenessUI _buisenessWidget;

        //public static UIController UI => _instance._ui;
        //public static BuisenessListUI UI => _instance._buisenessListUI;

        //private UIController _ui;
        //private BuisenessListUI _ui;
        private EcsWorld _world;
        private EcsSystems _systems;
//        private static EntryPoint _instance;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            //_instance = this;
        }

        private void Start()
        {
            if (_gameConfig == null)
            {
                Debug.LogWarning($"Игра не сконфигурирована в {nameof(EntryPoint)}");
                return;
            }

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            AddSystems();
        }

        private void AddSystems()
        {
            _systems
                .Add(new GameInitSystem(_gameConfig))
                .Add(new BuisenessSystem(_buisenessWidget))
                .Add(new PlayerSystem(_playerStatsWidget));

            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _world?.Destroy();
        }



    }
}
