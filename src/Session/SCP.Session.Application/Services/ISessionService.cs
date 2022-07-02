using SCP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP.Session.Application.Services
{
    /// <summary>
    /// Service used for handling transactions.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Start new session
        /// </summary>
        /// <param name="wsdata">Workstation data needed to create new session</param>
        /// <returns>Session object</returns>
        Task<SessionModel?> StartSession(WorkstationDataModel wsModel);

        /// <summary>
        /// Finish already opened session
        /// </summary>
        /// <param name="id">Id of session to finish</param>
        /// <returns>Session object</returns>
        Task<SessionModel?> FinishSession(Guid id);

        /// <summary>
        /// Get specific session
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <returns>Session object</returns>
        Task<SessionModel?> GetSession(Guid id);
    }
}
