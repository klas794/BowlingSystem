using AccountabilityInterfacesLib;
using AccountabilityLib;
using AccountInterfacesLib;
using BowlingDbLib;
using BowlingInterfacesLib;
using BowlingLib;
using FactoryEnumsLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace BowlingUnitTestLib
{
    public class BowlingSystemTests
    {
        private ServiceProvider _serviceProvider;

        public BowlingSystemTests()
        {

            _serviceProvider = new ServiceCollection()
                .AddTransient<BowlingSystem>()
                .AddTransient<IPartyRepository, FakePartyRepository>()
                .AddTransient<IAccountabilityRepository, FakeAccountabilityRepository>()
                .AddTransient<IAccountRepository, FakeAccountRepository>()
                .BuildServiceProvider();
        }

        [Fact]
        public void WinnerOfYear()
        {
            var sut = _serviceProvider.GetService<BowlingSystem>();

            var player1 = sut.CreatePlayer("Kalle Kallesson", "710101-1111");
            var player2 = sut.CreatePlayer("Olle Ollesson", "810606-2222");

            var game = sut.PlayGame(player1, player2, true);
            Assert.Equal(game.Winner, player1);

            var year = game.TimePoint.Year;
            Assert.Equal(sut.WinnerOfYear(year), player1);
        }

        [Fact]
        public void LaneFactoryWorks()
        {
            var sut = _serviceProvider.GetService<BowlingSystem>();

            var player1 = sut.CreatePlayer("Kalle Kallesson", "710101-1111");
            var player2 = sut.CreatePlayer("Olle Ollesson", "810606-2222");

            var lane = sut.GetDefaultLane();

            var game = sut.PlayGame(player1, player2, true, lane);

            Assert.NotEqual(game.Lane, null);
        }

        [Fact]
        public void PlayersMustBeRegistered()
        {
            var sut = _serviceProvider.GetService<BowlingSystem>();

            PlayerParty player1 = null;
            PlayerParty player2 = null;

            Assert.Throws<InvalidOperationException>(() => sut.PlayGame(player1, player2, true));

        }

        //[Fact]
        //public void PlayCompetition()
        //{
        //    var sut = _serviceProvider.GetService<IBowlingSystem>();

        //    var competitionId = sut.RegisterCompetition("Holiday special", new TimePeriod {
        //        StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1)
        //    });

        //    var player1 = sut.CreatePlayer("Kalle Kallesson", "710101-1111");
        //    var player2 = sut.CreatePlayer("Olle Ollesson", "810606-2222");

        //    sut.RegisterCompetitionPlayer(competitionId, player1.PlayerPartyId);
        //    sut.RegisterCompetitionPlayer(competitionId, player2.PlayerPartyId);

        //    sut.RunCompetition(competitionId);

        //    var competitions = sut.ListCompetitions();

        //    var competition = competitions.Find(x => x.CompetitionId == competitionId);

        //    Assert.Equal(competition?.Games.Count, 10);
        //}
    }
}
