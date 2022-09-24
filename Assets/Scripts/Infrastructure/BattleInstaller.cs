using Infrastructure.States;
using Logic;
using Logic.Characters;
using Logic.GameStates;
using Services.BattleSetupService;
using Zenject;

namespace Infrastructure
{
    public class BattleInstaller : MonoInstaller
    {
        public SpawnPoints SpawnPoints;

        public override void InstallBindings()
        {
            BindGameStates();
            Container.Bind<SpawnPoints>().FromInstance(SpawnPoints).AsSingle();
            Container.BindInterfacesTo<CharacterFactory>().AsSingle();
            Container.BindInterfacesTo<AliveCharacters>().AsSingle();
            Container.BindInterfacesTo<BattleSetupService>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.BindInterfacesTo<GameStateMachine>().AsSingle()
                .OnInstantiated<GameStateMachine>((_, gsm) =>
                {
                    gsm.Enter<SetupState>();
                });
            Container.BindInterfacesAndSelfTo<SetupState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResultState>().AsSingle();
        }
    }
}