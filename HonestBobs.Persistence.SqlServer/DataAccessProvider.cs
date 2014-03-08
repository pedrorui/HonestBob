using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HonestBobs.Persistence.SqlServer
{
	/// <summary>
	/// An implementation of the <see cref="IDataAccessProvider"/> for Microsoft SQL Server.
	/// </summary>
	public class DataAccessProvider : IDataAccessProvider
	{
		/// <summary>
		/// The connection string
		/// </summary>
		private readonly IDataAccessInitializer dataAccessInitializer;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAccessProvider" /> class.
		/// </summary>
		/// <param name="dataAccessInitializer">The data access initializer.</param>
		public DataAccessProvider(IDataAccessInitializer dataAccessInitializer)
		{
			this.dataAccessInitializer = dataAccessInitializer;
		}

		/// <summary>
		/// Creates a command for the implemented provider.
		/// </summary>
		/// <param name="commandText">The command Text.</param>
		/// <returns>
		/// The <see cref="IDbCommand" /> instance.
		/// </returns>
		public IDbCommand CreateCommand(string commandText)
		{
			return new SqlCommand(commandText);
		}

		/// <summary>
		/// Retrive a list of objects from a query.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command.</param>
		/// <param name="dataLoader">The data loader.</param>
		/// <returns>
		/// The list of results from the command execution.
		/// </returns>
		public IList<T> ReadEnumerable<T>(IDbCommand command, Func<IDataReader, T> dataLoader)
		{
			IList<T> items = new List<T>();
			this.Read(command, reader =>
			{
				while (reader.Read())
				{
					items.Add(dataLoader(reader));
				}
			});
			return items;
		}

		/// <summary>
		/// Retrieve a single object from a query.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command.</param>
		/// <param name="dataLoader">The data loader.</param>
		/// <returns>
		/// The item from the command execution.
		/// </returns>
		public T ReadSingle<T>(IDbCommand command, Func<IDataReader, T> dataLoader) where T : class, new()
		{
			var item = new T();
			this.Read(command, reader =>
			{
				reader.Read();
				item = dataLoader(reader);
			});

			return item;
		}

		/// <summary>
		/// Reads the specified command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="dataReader">The data reader.</param>
		private void Read(IDbCommand command, Action<IDataReader> dataReader)
		{
			using (var connection = new SqlConnection(this.dataAccessInitializer.ConnectionString))
			{
				using (command)
				{
					command.Connection = connection;
					connection.Open();
					using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
					{
						dataReader(reader);
						reader.Close();
					}
				}

				connection.Close();
			}
		}
	}
}