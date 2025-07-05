using RepoDb;

namespace Application.Interfaces.IRepository.Common
{
    public interface IGenericRepository
    {
        /// <summary>
        /// Ejecuta un procedimiento almacenado con los parámetros especificados.
        /// </summary>
        /// <typeparam name="T">El tipo de datos que se espera recibir.</typeparam>
        /// <param name="procedureName">Nombre del procedimiento almacenado.</param>
        /// <param name="parameters">Parámetros del procedimiento.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene los datos obtenidos.</returns>
        Task<IEnumerable<T>> GetProcedureAsync<T>(string procedureName, object parameters, int? commandTimeout = null) where T : class;

        /// <summary>
        /// Ejecuta un procedimiento almacenado con los parámetros especificados.
        /// </summary>
        /// <typeparam name="T">El tipo de datos que se espera recibir.</typeparam>
        /// <param name="procedureName">Nombre del procedimiento almacenado.</param>
        /// <param name="parameters">Parámetros del procedimiento.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene los datos obtenidos.</returns>
        Task<T?> GetProcedureSingleAsync<T>(string procedureName, object parameters, int? commandTimeout = null) where T : class;

        /// <summary>
        /// Ejecuta un procedimiento almacenado con los parámetros especificados.
        /// </summary>
        /// <typeparam name="T">El tipo de datos que se espera recibir.</typeparam>
        /// <param name="procedureName">Nombre del procedimiento almacenado.</param>
        /// <param name="parameters">Parámetros del procedimiento.</param>
        Task ExecuteProcedureAsync(string procedureName, object parameters, int? commandTimeout = null);
    }
}
