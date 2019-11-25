using System.Data.Common;

using CollectionManager.DataBase.Tables;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.DataBase {
	public class CollectionManagerDbContext : DbContext {
		private readonly DbConnection _dbConnection;

		public DbSet<ItemSet> ItemSets {
			get;
			set;
		}

		public DbSet<Item> Items {
			get;
			set;
		}

		public DbSet<ItemSetAuthor> ItemSetAuthors {
			get;
			set;
		}

		public CollectionManagerDbContext(DbConnection dbConnection) {
			this._dbConnection = dbConnection;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			// Primary Keys
			modelBuilder.Entity<ItemSet>().HasKey(x => x.ItemSetId);
			modelBuilder.Entity<Item>().HasKey(x => new { x.ItemSetId, x.OrdinalNumber, x.OrdinalAlternative });
			modelBuilder.Entity<ItemSetAuthor>().HasKey(x => new { x.ItemSetId, x.Name });

			// Relation
			modelBuilder.Entity<Item>()
				.HasOne(x => x.ItemSet)
				.WithMany(x => x.Items)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ItemSetAuthor>()
				.HasOne(x => x.ItemSet)
				.WithMany(x => x.Authors)
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			switch (this._dbConnection) {
				case SqliteConnection conn:
					optionsBuilder.UseSqlite(conn);
					break;
			}
		}
	}
}
