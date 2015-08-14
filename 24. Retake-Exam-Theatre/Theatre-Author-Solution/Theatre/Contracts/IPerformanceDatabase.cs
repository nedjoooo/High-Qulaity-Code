namespace Theatre.Contracts
{
    using System;
    using System.Collections.Generic;

    using Theatre.Exceptions;

    /// <summary>
    /// Defines a database (repository) holding theatres and performances and
    /// add / list operations for theatres and performances in theаtres. 
    /// </summary>
    public interface IPerformanceDatabase
    {
        /// <summary>
        /// Adds a new theatre to the database.
        /// </summary>
        /// <param name="theatreName">The name of the theatre</param>
        /// <exception cref="TheatreNotFoundException">Thrown in case of duplicate theatre. 
        /// Holds a "Duplicate theatre" as error message.</exception>
        void AddTheatre(string theatreName);

        /// <summary>
        /// Lists all theatres ordered by name.
        /// </summary>
        /// <returns>A sequence of theatre names</returns>
        IEnumerable<string> ListTheatres();

        /// <summary>
        /// Adds a new performance to an existing theatre. The performance cannot overlap
        /// with another performance in the same theatre.
        /// </summary>
        /// <param name="theatreName">The name of the theatre</param>
        /// <param name="performanceTitle">The title of the performance</param>
        /// <param name="startDateTime">The performance start date and time (e.g. 12-Jan-2015 11:30)</param>
        /// <param name="duration">The performance duration (e.g. 1:30 hours)</param>
        /// <param name="price">The price of the performance</param>
        /// <exception cref="TheatreNotFoundException">Thrown with message "Theatre does not exist"
        /// in case of non-existing theatre.</exception>
        /// <exception cref="TimeDurationOverlapException">Thrown with message "Time/duration overlap"
        /// in case of the time interval overlaps with another performance time interval
        /// in the same theatre.</exception>
        void AddPerformance(string theatreName, string performanceTitle, DateTime startDateTime, TimeSpan duration, decimal price);

        /// <summary>
        /// Lists all performances from all theatres ordered by theatre name (as first criteria)
        /// and then by performance date and time (as second criteria).
        /// </summary>
        /// <returns>A sequence of all performances</returns>
        IEnumerable<Performance> ListAllPerformances();

        /// <summary>
        /// Lists all performances from specified theatre ordered by performance date and time.
        /// </summary>
        /// <param name="theatreName">The name of the theatre</param>
        /// <returns>A sequence of all performances for the specified theatre</returns>
        /// <exception cref="TheatreNotFoundException">Thrown with message "Theatre does not exist"
        /// in case of non-existing theatre.</exception>
        IEnumerable<Performance> ListPerformances(string theatreName);
    }
}
