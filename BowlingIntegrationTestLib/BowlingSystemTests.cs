using AccountabilityInterfacesLib;
using AccountabilityLib;
using BowlingDbLib;
using AccountInterfacesLib;
using BowlingInterfacesLib;
using BowlingLib;
using MeasurementLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace BowlingIntegrationTestLib
{
    public class BowlingSystemTests
    {
        private IServiceProvider _serviceProvider;
        private BowlingContext _context;

        public BowlingSystemTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BowlingContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BengansBowling;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new BowlingContext(optionsBuilder.Options);

            _serviceProvider = new ServiceCollection()
                .AddTransient<BowlingSystem>()
                .AddTransient<IPartyRepository, SqlPartyRepository>()
                .AddTransient<IAccountabilityRepository, SqlAccountabilityRepository>()
                .AddTransient<IAccountRepository, SqlAccountRepository>()
                .AddSingleton<BowlingContext>(context)
                .BuildServiceProvider();

            _context = new BowlingContext(optionsBuilder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void WinnerOfYear()
        {
            var sut = _serviceProvider.GetService<BowlingSystem>();

            var player1 = sut.CreatePlayer("Kalle Kallesson", "710101-1111");
            var player2 = sut.CreatePlayer("Olle Ollesson", "810606-2222");

            var game = sut.PlayGame(player1, player2, true);
            Assert.Equal(game.Winner, player1);

            var year = DateTime.Now.Year;
            Assert.Equal(sut.WinnerOfYear(year), player1);

        }

        [Fact]
        public void PlayCompetition()
        {
            var sut = _serviceProvider.GetService<BowlingSystem>();

            var competitionGuid = sut.RegisterCompetition("Holiday special", new TimePeriod
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            });

            var player1 = sut.CreatePlayer("Kalle Kallesson", "710101-1111");
            var player2 = sut.CreatePlayer("Olle Ollesson", "810606-2222");

            sut.RegisterCompetitionPlayer(competitionGuid, player1.PlayerPartyId);
            sut.RegisterCompetitionPlayer(competitionGuid, player2.PlayerPartyId);

            sut.RunCompetition(competitionGuid);

            var competitions = sut.ListCompetitions();

            var competition = competitions.Find(x => x.CompetitionGuid == competitionGuid);

            Assert.Equal(competition?.Games.Count, 10);
            Assert.Equal(competition?.Name, "Holiday special");
            Assert.Equal(competition?.PlayerOne, player1);
            Assert.Equal(competition?.PlayerTwo, player2);
        }
    }
}
