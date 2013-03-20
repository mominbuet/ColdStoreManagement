using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Interfaced Repository Pattern for Data Access Objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    where T : class
    {
        /// <summary>
        /// Gets a queryable enumeration of all the items in the repository
        /// </summary>
        IQueryable<T> Find();

        DataContext DataContext { get; set; }

        /// <summary>
        /// Gets a List of all the items in the repository
        /// </summary>
        IList<T> All();

        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to add</param>
        bool Add(T item);

        /// <summary>
        /// Adds a new List of entity to the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="itemList">The entity to add</param>
        bool Add(IList<T> itemList);

        bool Exists(T item);

        /// <summary>
        /// Edit a new entity to the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to edit</param>
        bool Edit(T item);

        bool Edit(T item, bool flag);
        bool Edit(IList<T> item, bool isInContext);

        /// <summary>
        /// Deletes the specified entity from the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to delete</param>
        bool Delete(T item);

        bool Delete(T item, bool isInContext);

        /// <summary>
        /// Deletes the specified entity List from the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to delete</param>
        bool Delete(IList<T> itemList, bool isInContext);

        void Refresh(T item);
        T PickByID(int id);
        void Dispose();
    }
}
