using Leopotam.EcsLite;
using UnityEngine;

namespace Game.ECS
{
    public sealed class BuisenessSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private BuisenessUI _buisenessUI;
        
        private EcsFilter _filter;
        private EcsPool<BuisenessComponent> _buisenessPool;
        private EcsPool<PlayerComponent> _playerPool;
        
        private int buisenessLevelUpId = -1;
        private int buisenessImprovementId1 = -1;
        private int buisenessImprovementId2 = -1;

        // DI -> вывод UI
        public BuisenessSystem(BuisenessUI buisenessUI)
        {
            _buisenessUI = buisenessUI;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<BuisenessComponent>()
                .End();

            _buisenessPool = _world.GetPool<BuisenessComponent>();
            _playerPool = _world.GetPool<PlayerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref BuisenessComponent buiseness = ref _buisenessPool.Get(entity);
                if (buiseness.widget == null)
                    CreateUI(ref buiseness);

                WorkProcess(ref buiseness);

                UpdateUI(ref buiseness);
            }
        }

        private void CreateUI(ref BuisenessComponent buiseness)
        {
            var _buisenessPanel = _buisenessUI.Add();

            _buisenessPanel.Initialize(
                buiseness.config,
                buiseness.config.BaseCost);

            buiseness.widget = _buisenessPanel;
            int buisenessId = buiseness.id;
            buiseness.widget.OnClickButtonLvlUp(() => OnClickLevelUp(buisenessId));

            buiseness.widget.OnClickButtonImprovement1(() => OnClickImprovement1(buisenessId));
            buiseness.widget.OnClickButtonImprovement2(() => OnClickImprovement2(buisenessId));
        }

        private void WorkProcess(ref BuisenessComponent buiseness)
        {
            if (buisenessLevelUpId != -1 && buiseness.id == buisenessLevelUpId)
                IncreaseLvl(ref buiseness);

            if (buisenessImprovementId1 != -1 && buiseness.id == buisenessImprovementId1)
                ImprovementBuy(1, ref buiseness);

            if (buisenessImprovementId2 != -1 && buiseness.id == buisenessImprovementId2)
                ImprovementBuy(2, ref buiseness);

            if (buiseness.level == 0) return;

            var delay = buiseness.config.IncomeDelayTime;
            var pr = (Time.deltaTime / delay) * 100f;

            buiseness.progress += pr;

            if (buiseness.progress >= 100f)
                WorkComplete(ref buiseness);
        }

        private void WorkComplete(ref BuisenessComponent buiseness)
        {
            buiseness.progress = 0;

            RecalcIncome(ref buiseness);

            ChangePlayerMoney(buiseness.income);
        }

        private void UpdateUI(ref BuisenessComponent buiseness)
        {
            ref var player = ref _playerPool.Get(0);
            var playerMoney = player.money;

            buiseness.nexLevelPrice = (buiseness.level + 1) * buiseness.config.BaseCost;

            if (playerMoney >= buiseness.nexLevelPrice)
                buiseness.widget.ButtonLvlUpInteractable(true);
            else
                buiseness.widget.ButtonLvlUpInteractable(false);

            buiseness.widget.SetLevel(buiseness.level);
            buiseness.widget.SetProgress(buiseness.progress);
            buiseness.widget.SetLvlUpPrice(buiseness.nexLevelPrice);

            if (buiseness.level > 0)
            {
                if (buiseness.improvementMod1 <= 0)
                {
                    if (playerMoney >= buiseness.config.Improvement1.Price)
                        buiseness.widget.ButtonImprovementInteractable1(true);
                    else
                        buiseness.widget.ButtonImprovementInteractable1(false);
                }

                if (buiseness.improvementMod2 <= 0)
                {
                    if (playerMoney >= buiseness.config.Improvement2.Price)
                        buiseness.widget.ButtonImprovementInteractable2(true);
                    else
                        buiseness.widget.ButtonImprovementInteractable2(false);
                }
            }
        }

        private void IncreaseLvl(ref BuisenessComponent buiseness)
        {
            buisenessLevelUpId = -1;
            buiseness.level++;
            buiseness.progress = 0;
            
            ChangePlayerMoney(-buiseness.nexLevelPrice);

            buiseness.nexLevelPrice = (buiseness.level + 1) * buiseness.config.BaseCost;

            RecalcIncome(ref buiseness);

            buiseness.widget.SetIncome(buiseness.income);

            buiseness.widget.ButtonLvlUpInteractable(false);
        }

        private void RecalcIncome(ref BuisenessComponent buiseness)
        {
            var income = buiseness.level * buiseness.config.BaseIncome * (1 + (buiseness.improvementMod1 / 100f) + (buiseness.improvementMod2 / 100f));
            buiseness.income = (int)income;
            buiseness.widget.SetIncome((int)income);
        }

        private void ImprovementBuy(int num, ref BuisenessComponent buiseness)
        {
            if (num == 1) {
                buiseness.improvementMod1 = buiseness.config.Improvement1.PercentModIncome;
                ChangePlayerMoney(-buiseness.config.Improvement1.Price);
                RecalcIncome(ref buiseness);
                buisenessImprovementId1 = -1;
                buiseness.widget.ButtonImprovementInteractable1(false);
            }
            else if (num == 2) {
                buiseness.improvementMod2 = buiseness.config.Improvement2.PercentModIncome;
                ChangePlayerMoney(-buiseness.config.Improvement2.Price);
                RecalcIncome(ref buiseness);
                buisenessImprovementId2 = -1;
                buiseness.widget.ButtonImprovementInteractable2(false);
            }
        }


        private void OnClickLevelUp(int buisenessId)
        {
            buisenessLevelUpId = buisenessId;
        }

        private void OnClickImprovement1(int buisenessId)
        {
            buisenessImprovementId1 = buisenessId;
        }


        private void OnClickImprovement2(int buisenessId)
        {
            buisenessImprovementId2 = buisenessId;
        }

        private void ChangePlayerMoney(int money)
        {
            ref var player = ref _playerPool.Get(0);
            player.money += money;
        }
    }
}
