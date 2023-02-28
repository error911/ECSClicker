using Leopotam.EcsLite;

namespace Game.ECS
{
    public sealed class PlayerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsPool<PlayerComponent> _playerPool;
        private PlayerStatsWidget _widget;

        // DI -> вывод UI
        public PlayerSystem(PlayerStatsWidget widget)
        {
            _widget = widget;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerPool = _world.GetPool<PlayerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            PayDay();
        }

        // Собираем деньги со всех бизнесов
        private void PayDay()
        {
            ref var player = ref _playerPool.Get(0);
            UpdatePlayerUI(player.money);
        }

        private void UpdatePlayerUI(int money)
        {
            if (!_widget) return;
            _widget.SetMoney(money);
        }
    }
}
