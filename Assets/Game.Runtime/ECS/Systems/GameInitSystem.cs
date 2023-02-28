using Leopotam.EcsLite;
using UnityEngine;

namespace Game.ECS
{
    public sealed class GameInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private GameConfig _gameConfig;

        // DI -> конфигурация
        public GameInitSystem(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            CreatePlayer();
            CreateBuisenesses();
        }

        private void CreatePlayer()
        {
            var player = _world.NewEntity();
            var _poolPlayer = _world.GetPool<PlayerComponent>();

            ref PlayerComponent playerComponent = ref _poolPlayer.Add(player);

            playerComponent.money = _gameConfig.StartMoney;
        }

        private void CreateBuisenesses()
        {
            if (_gameConfig.Buisinesses.Length == 0)
            {
                Debug.LogWarning("Бизнесы не сконфигурированы");
                return;
            }

            int id = 0;
            var _poolBuiseness = _world.GetPool<BuisenessComponent>();
            foreach (var config in _gameConfig.Buisinesses)
            {
                var buiseness = _world.NewEntity();

                ref BuisenessComponent buisenessComponent = ref _poolBuiseness.Add(buiseness);

                if (config == _gameConfig.Buisinesses[0])
                {
                    buisenessComponent.level = 1;
                }
                else
                {
                    buisenessComponent.level = 0;
                }

                buisenessComponent.id = id;
                buisenessComponent.config = config;
                buisenessComponent.income = buisenessComponent.level * buisenessComponent.config.BaseIncome;
                buisenessComponent.improvementMod1 = 0;
                buisenessComponent.improvementMod2 = 0;

                id++;
            }
        }
    }
}
