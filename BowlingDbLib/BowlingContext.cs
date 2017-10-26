using AccountabilityLib;
using AccountLib;
using FactoryInterfacesLib;
using FactoryLib;
using MeasurementLib;
using Microsoft.EntityFrameworkCore;
using System;

namespace BowlingDbLib
{
    public class BowlingContext : DbContext
    {
        public BowlingContext(DbContextOptions<BowlingContext> options)
            :base(options)
        {

        }

        public DbSet<PlayerParty> Parties { get; set; }
        public DbSet<GameAccountability> Accountabilities { get; set; }
        public DbSet<GameAccountabilityType> AccountabilityTypes { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<PhenomenonType> PhenomenonTypes { get; set; }
        public DbSet<Quantity> Quantities { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundsScoreAccount> ScoreAccounts { get; set; }
        public DbSet<RoundsScoreEntry> ScoreEntries { get; set; }
        public DbSet<WinningAccount> WinningAccounts { get; set; }
        public DbSet<WinningEntry> WinningEntries { get; set; }
        public DbSet<WinningTransaction> WinningTransactions { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<OakLane> OakLanes { get; set; }
    }
}




