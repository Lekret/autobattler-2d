using Infrastructure.States;
using Logic;
using Logic.Characters;
using Logic.GameStates;
using Services.BattleSetup;
using Services.CharacterStorage;
using Services.NextAction;
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
            Container.BindInterfacesTo<BattleSetupService>().AsSingle();
            Container.BindInterfacesTo<CharacterStorage>().AsSingle();
            Container.BindInterfacesTo<CharacterActionService>().AsSingle();
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