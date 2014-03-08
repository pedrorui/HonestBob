using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HonestBobs.Persistence.SqLite
{
	/// <summary>
	/// The SqLite data Access Provider class
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
			return new SQLiteCommand(commandText);
		}

		/// <summary>
		/// Returns the result of a command excution as an enumerable.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command.</param>
		/// <param name="dataLoader">The data loader.</param>
		/// <returns></returns>
		public IList<T> ReadEnumerable<T>(IDbCommand command, Func<IDataReader, T> dataLoader)
		{
			IList<T> items = new List<T>();
			this.Read(
				command,
				reader =>
				{
					while (reader.Read())
					{
						items.Add(dataLoader(reader));
					}
				});
			return items;
		}

		/// <summary>
		/// Returns a single item from the command result.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command.</param>
		/// <param name="dataLoader">The data loader.</param>
		/// <returns></returns>
		public T ReadSingle<T>(IDbCommand command, Func<IDataReader, T> dataLoader) where T : class, new()
		{
			T item = null;
			this.Read(
				command,
				reader =>
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
		protected void Read(IDbCommand command, Action<IDataReader> dataReader)
		{
			using (var connection = new SQLiteConnection(this.dataAccessInitializer.ConnectionString))
			{
				using (command)
				{
					command.Connection = connection;
					connection.Open();
					using (IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
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
