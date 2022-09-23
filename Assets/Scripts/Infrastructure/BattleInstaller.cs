using Infrastructure.States;
using Logic;
using Logic.GameStates;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BattleInstaller : MonoInstaller
    {
        public SpawnPoints SpawnPoints;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesTo<SetupState>().AsSingle();
            Container.BindInterfacesTo<BattleState>().AsSingle();
            Container.Bind<SpawnPoints>().FromInstance(SpawnPoints).AsSingle();
        }
    }
}