using System.Data;

namespace HonestBobs.Persistence.Infrastructure
{
	/// <summary>
	///     Extension methods for the data objects.
	/// </summary>
	public static class DataExtensions
	{
		/// <summary>
		///     Add the parameter to the command instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command.</param>
		/// <param name="name">The name.</param>
		/// <param name="dbType">The db type.</param>
		/// <param name="value">The value.</param>
		/// <returns>
		///     The <see cref="IDbDataParameter" />.
		/// </returns>
		public static IDbDataParameter AddParameter<T>(this IDbCommand command, string name, DbType dbType, T value)
		{
			return command.AddParameter(name).SetType(dbType).SetValue(value);
		}

		/// <summary>
		///     Add parameter to the command instance.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="name">The name.</param>
		/// <returns>
		///     The <see cref="IDbDataParameter" />.
		/// </returns>
		public static IDbDataParameter AddParameter(this IDbCommand command, string name)
		{
			IDbDataParameter parameter = command.CreateParameter();
			parameter.ParameterName = name;
			command.Parameters.Add(parameter);

			return parameter;
		}

		/// <summary>
		///     Set the type of the parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="dbType">The db type.</param>
		/// <returns>
		///     The <see cref="IDbDataParameter" />.
		/// </returns>
		public static IDbDataParameter SetType(this IDbDataParameter parameter, DbType dbType)
		{
			parameter.DbType = dbType;
			return parameter;
		}

		/// <summary>
		///     Set the value of the parameter value.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		/// <returns>
		///     The <see cref="IDbDataParameter" />.
		/// </returns>
		public static IDbDataParameter SetValue<T>(this IDbDataParameter parameter, T value)
		{
			parameter.Value = value;
			return parameter;
		}
	}
}