using System.Collections.Generic;
using System.Data;
using HonestBobs.Data;
using HonestBobs.Domain;
using HonestBobs.Persistence.Infrastructure;

namespace HonestBobs.Persistence
{
	public class CategoryRepository : BaseRepository, ICategoryRepository
	{
		/// <summary>
		/// The select sql.
		/// </summary>
		private const string SelectSql = "SELECT Id, Name FROM Categories";

		public CategoryRepository(IDataAccessProvider dataAccessProvider) : base(dataAccessProvider)
		{
		}

		public IEnumerable<Category> FetchAll()
		{
			using (var command = this.DataAccessProvider.CreateCommand(SelectSql))
			{
				return this.DataAccessProvider.ReadEnumerable(command, CreateCategoriesFromReader);
			}
		}

		public Category FetchById(int id)
		{
			var sql = string.Concat(SelectSql, " WHERE Id = @id");
			using (var command = this.DataAccessProvider.CreateCommand(sql))
			{
				command.AddParameter("@id", DbType.Int32, id);
				return this.DataAccessProvider.ReadSingle(command, CreateCategoriesFromReader);
			}
		}

		/// <summary>
		/// Creates the categories from data reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>A new category instance from the data reader.</returns>
		private static Category CreateCategoriesFromReader(IDataReader reader)
		{
			var item = new Category
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1)
			};

			return item;
		}
	}
}