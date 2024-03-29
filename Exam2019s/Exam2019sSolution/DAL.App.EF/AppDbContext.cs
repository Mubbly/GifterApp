﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base;
using com.mubbly.gifterapp.Contracts.Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private readonly Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> _entityTracker =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();

        private readonly IUserNameProvider _userNameProvider;

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider) : base(options)
        {
            _userNameProvider = userNameProvider;
        }

        // Example:
        // public DbSet<ActionType> ActionTypes { get; set; } = default!;
        
        public DbSet<Example> Examples { get; set; } = default!;
        
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Answer> Answers { get; set; } = default!;
        public DbSet<QuizResponse> QuizResponses { get; set; } = default!;
        public DbSet<QuizType> QuizTypes { get; set; } = default!;

        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Disable cascade delete
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            
            // Make sure that only one answer can be 'true' per one question (for Polls)
            modelBuilder.Entity<Answer>()
                .HasIndex(e => new { e.QuestionId, e.IsCorrect })
                .IsUnique().HasFilter($"[{nameof(Answer.IsCorrect)}] = 1");

            
            
            
            // Example:
            // modelBuilder.Entity<Wishlist>()
            //     .HasMany(w => w.Gifts)
            //     .WithOne(g => g.Wishlist!);

            // EXAMPLE on how to enable cascade delete for some entities:
            // // enable cascade delete on GpsSession->GpsLocation
            // modelBuilder.Entity<GpsSession>()
            //     .HasMany(s => s.GpsLocations)
            //     .WithOne(l => l.GpsSession!)
            //     .OnDelete(DeleteBehavior.Cascade);
            //
            // // enable cascade delete on LangStr->LangStrTranslations
            // modelBuilder.Entity<LangStr>()
            //     .HasMany(s => s.Translations)
            //     .WithOne(l => l.LangStr!)
            //     .OnDelete(DeleteBehavior.Cascade);

            // // indexes
            // builder.Entity<GpsSession>().HasIndex(i => i.CreatedAt);
            // builder.Entity<GpsSession>().HasIndex(i => i.RecordedAt);
            // builder.Entity<GpsLocation>().HasIndex(i => i.CreatedAt);
            // builder.Entity<GpsLocation>().HasIndex(i => i.RecordedAt);

            // builder.Entity<LangStrTranslation>().HasIndex(i => new {i.Culture, i.LangStrId}).IsUnique();


            // This code is commented out because it only applies when using MSSQL and still had errors

            // Following code is to turn off Cascade Delete for relationships that have multiple FK references to the SAME table
            // to avoid cycles and multiple cascade paths
            // TODO: Remove repetitions, create general logic to target them at once

            /*
                // Turn off cascade delete for every foreign key relationship
                foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            */

            /*modelBuilder.Entity<Friendship>()
                .HasOne(f => f.AppUser1)
                .WithMany(t => t.ConfirmedFriendships)
                .HasForeignKey(t => t.AppUser1Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.AppUser2)
                .WithMany(t => t.PendingFriendships)
                .HasForeignKey(t => t.AppUser2Id)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ReceivedPrivateMessages)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserSender)
                .WithMany(t => t.SentPrivateMessages)
                .HasForeignKey(t => t.UserSenderId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ReceivedPrivateMessages)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserSender)
                .WithMany(t => t.SentPrivateMessages)
                .HasForeignKey(t => t.UserSenderId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<ReservedGift>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ReservedGiftsForUser)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ReservedGift>()
                .HasOne(f => f.UserGiver)
                .WithMany(t => t.ReservedGiftsByUser)
                .HasForeignKey(t => t.UserGiverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ArchivedGift>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ArchivedGiftsForUser)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ArchivedGift>()
                .HasOne(f => f.UserGiver)
                .WithMany(t => t.ArchivedGiftsByUser)
                .HasForeignKey(t => t.UserGiverId)
                .OnDelete(DeleteBehavior.NoAction);*/
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }

        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();

            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.EditedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.EditedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.EditedAt = DateTime.Now;
                entityWithMetaData.EditedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker) value.Id = key.Id;
        }
    }
}