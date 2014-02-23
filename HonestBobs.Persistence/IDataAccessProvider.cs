using System;
using System.Collections.Generic;
using System.Data;

namespace HonestBobs.Persistence
{
	/// <summary>
	/// The DataAccess interface.
	/// </summary>
	public interface IDataAccessProvider
	{
		/// <summary>
		/// Creates a command for the implemented provider.
		/// </summary>
		/// <param name="commandText">The command Text.</param>
		/// <returns>
		/// The <see cref="IDbCommand" /> instance.
		/// </returns>
		IDbCommand CreateCommand(string commandText);

		/// <summary>
		/// Retrive a list of objects from a query.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command.</param>
		/// <param name="dataLoader">The data loader.</param>
		/// <returns>
		/// The list of results from the command execution.
		/// </returns>
		IList<T> ReadEnumerable<T>(IDbCommand command, Func<IDataReader, T> dataLoader);

		/// <summary>
		/// Retrieve a single object from a query.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command.</param>
		/// <param name="dataLoader">The data loader.</param>
		/// <returns>
		/// The item from the command execution.
		/// </returns>
		T ReadSingle<T>(IDbCommand command, Func<IDataReader, T> dataLoader) where T : class, new();
	}
}