using System.Collections.Generic;
using System.Data;
using HonestBobs.Data;
using HonestBobs.Domain;
using HonestBobs.Persistence.Infrastructure;

namespace HonestBobs.Persistence
{
	public class BookRepository : BaseRepository, IBookRepository
	{
		private const string SelectSql = 
@"SELECT P.Id, P.Name, P.Description, P.UnitPrice, B.Author, B.ISBN, B.Pages
FROM Products as P INNER JOIN Books as B ON P.Id = B.ProductId";

		public BookRepository(IDataAccessProvider dataAccessProvider) : base(dataAccessProvider)
		{
		}

		public IEnumerable<Book> FetchAll()
		{
			using (var command = this.DataAccessProvider.CreateCommand(SelectSql))
			{
				return this.DataAccessProvider.ReadEnumerable(command, CreateCategoriesFromReader);
			}
		}

		public Book FetchById(int id)
		{
			var sql = string.Concat(SelectSql, " WHERE P.Id = @id;");
			using (var command = this.DataAccessProvider.CreateCommand(sql))
			{
				command.AddParameter("@id", DbType.Int32, id);
				return this.DataAccessProvider.ReadSingle(command, CreateCategoriesFromReader);
			}
		}

		public IList<Book> GetProductsByCategory(Category category)
		{
			var sql = string.Concat(SelectSql, " WHERE P.CategoryId = @id;");
			using (var command = this.DataAccessProvider.CreateCommand(sql))
			{
				command.AddParameter("@id", DbType.Int32, category.Id);
				return this.DataAccessProvider.ReadEnumerable(command, CreateCategoriesFromReader);
			}
		}

		private static Book CreateCategoriesFromReader(IDataReader reader)
		{
			var item = new Book
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1),
				Description = reader.GetString(2),
				UnitPrice = reader.GetDecimal(3),
				Author = reader.GetString(4),
				Isbn = reader.GetString(5),
				Pages = reader.GetInt32(6)
			};

			return item;
		}
	}
}