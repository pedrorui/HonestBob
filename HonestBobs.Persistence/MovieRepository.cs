using System.Collections.Generic;
using System.Data;
using HonestBobs.Data;
using HonestBobs.Domain;
using HonestBobs.Persistence.Infrastructure;

namespace HonestBobs.Persistence
{
	public class MovieRepository : BaseRepository, IMovieRepository
	{
		private const string SelectSql =
@"SELECT P.Id, P.Name, P.Description, P.UnitPrice, M.Format, M.PackageItems, M.Studio
FROM Products as P INNER JOIN Movies as M ON P.Id = M.ProductId";

		public MovieRepository(IDataAccessProvider dataAccessProvider) : base(dataAccessProvider)
		{
		}

		public IEnumerable<Movie> FetchAll()
		{
			using (var command = this.DataAccessProvider.CreateCommand(SelectSql))
			{
				return this.DataAccessProvider.ReadEnumerable(command, CreateCategoriesFromReader);
			}
		}

		public Movie FetchById(int id)
		{
			var sql = string.Concat(SelectSql, " WHERE P.Id = @id;");
			using (var command = this.DataAccessProvider.CreateCommand(sql))
			{
				command.AddParameter("@id", DbType.Int32, id);
				return this.DataAccessProvider.ReadSingle(command, CreateCategoriesFromReader);
			}
		}

		public IList<Movie> GetProductsByCategory(Category category)
		{
			var sql = string.Concat(SelectSql, " WHERE P.CategoryId = @id;");
			using (var command = this.DataAccessProvider.CreateCommand(sql))
			{
				command.AddParameter("@id", DbType.Int32, category.Id);
				return this.DataAccessProvider.ReadEnumerable(command, CreateCategoriesFromReader);
			}
		}

		private static Movie CreateCategoriesFromReader(IDataReader reader)
		{
			var item = new Movie
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1),
				Description = reader.GetString(2),
				UnitPrice = reader.GetDecimal(3),
				Format = reader.GetString(4),
				PackageUnits = reader.GetInt32(5),
				Studio = reader.GetString(6)
			};

			return item;
		}
	}
}