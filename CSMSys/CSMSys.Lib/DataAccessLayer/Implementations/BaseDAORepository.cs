using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Linq.Expressions;
using CSMSys.Lib.DataAccessLayer.Interfaces;

namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    /// <summary>
    /// Abstruct class for BaseDAO which contains CRUD functionalities like Add,Edit,Delete etc. so that we need not
    /// to write the same method for all corresponding DAO. Two generic parameter is taken,one is 'GenericEntityType' the 
    /// Entity class belongs to the DB table and second generic parameter is 'GenericContextType' wich is DataContext.Genererally 
    /// Every DB would be a DataContext. It gives the flexibility to connect with multiple DB.
    /// 
    /// </summary>
    /// <typeparam name="GenericEntityType"></typeparam>
    /// <typeparam name="GenericContextType"></typeparam>
    abstract public class BaseDAORepository<GenericEntityType, GenericContextType> : IRepository<GenericEntityType>, IDisposable
        where GenericEntityType : class
        where GenericContextType : DataContext, new()
    {
        #region Private Members
        protected DataContext _DataContext = null;

        ///Manages SubmitChanges() method
        private bool _Flag;

        ///Logger
        protected static log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //protected static log4net.ILog _SmtpLogger = log4net.LogManager.GetLogger("AMMS.Web.Smtp");

        #endregion

        #region Public Members
        public DataContext DataContext
        {
            get { return _DataContext; }
            set
            {
                _DataContext = value;
                _Flag = false;
            }
        }
        #endregion

        #region Constructor
        public BaseDAORepository()
        {
            _DataContext = CreateDataContext();

            this._DataContext.DeferredLoadingEnabled = false;

            _Flag = true;
        }
        #endregion

        #region Abstruct Methods
        /// <summary>
        ///selection delegate expression, used for speeding up Load(int ID)
        /// needs to be implemented by Repository descendants as follows:
        ///
        /// protected override Expression<Func<SpecializedTEntityType, bool>> GetIDSelector(int ID)
        ///{
        ///    return (item) => item.ID == ID;
        ///}
        /// 
        /// SpecializedTEntityType represents the given entity's type
        /// and item.ID represents the entity's unique ID property
        /// 
        /// </summary>

        protected abstract Expression<Func<GenericEntityType, bool>> GetIDSelector(int id);

        #endregion

        #region IRepository<GenericEntityType> Members

        /// <summary>
        /// Return all instances of type T.
        /// </summary>
        /// <returns></returns>
        public IList<GenericEntityType> All()
        {
            try
            {
                this.DataContext.DeferredLoadingEnabled = false;
                return GetTable.ToList();
            }
            catch (ArgumentNullException e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " object list not PICKED -- " + e);
                return null;
            }
        }

        /// <summary>
        /// Return all instances of type T that match the expression exp.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IList<GenericEntityType> FindAll(Func<GenericEntityType, bool> exp)
        {
            try
            {
                return GetTable.Where<GenericEntityType>(exp).ToList();
            }
            catch (ArgumentNullException e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " object list could not be PICKED -- " + e);
                return null;
            }
        }

        public IQueryable<GenericEntityType> Find()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pick Object by oid
        /// </summary>
        /// <param name="entity">DB entity</param>
        /// <returns>bool</returns>
        public GenericEntityType PickByID(int id)
        {
            if (id > 0)
            {
                try
                {
                    // this.DataContext.DeferredLoadingEnabled = false;
                    return GetTable.SingleOrDefault(GetIDSelector(id));
                }
                catch (ArgumentNullException e)
                {
                    _Logger.Error(typeof(GenericEntityType).Name + " object could not be PICKED -- " + e);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    _Logger.Error(typeof(GenericEntityType).Name + " object could not be PICKED -- " + e);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Add the entity to DB
        /// </summary>
        /// <param name="entity">DB entity</param>
        /// <returns>bool</returns>
        public bool Add(GenericEntityType entity)
        {
            if (entity == null)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " is null to ADD!");
                return false;
            }
            try
            {
                GetTable.InsertOnSubmit(entity);
                if (_Flag)
                {
                    _DataContext.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " could not be ADDED -- " + e);
                return false;
            }
        }

        /// <summary>
        /// Add the entity to DB
        /// </summary>
        /// <param name="entity">DB entity</param>
        /// <returns>bool</returns>
        public bool Add(IList<GenericEntityType> entityList)
        {
            if (entityList == null)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " list is null to ADD!");
                return false;
            }

            try
            {

                GetTable.InsertAllOnSubmit(entityList);
                if (_Flag)
                {
                    _DataContext.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " list could not be ADDED -- " + e);
                return false;
            }
        }

        public bool Exists(GenericEntityType entity)
        {
            ITable<GenericEntityType> table = _DataContext.GetTable<GenericEntityType>();
            return table.Contains(entity);
        }

        public bool Edit(GenericEntityType entity)
        {
            try
            {
                if (entity == null)
                {
                    _Logger.Error(typeof(GenericEntityType).Name + " is null to EDIT!");
                    return false;
                }

                _DataContext.GetTable<GenericEntityType>().Attach(entity);
                // The foll stmt causes a refresh of the identity cache

                _DataContext.Refresh(RefreshMode.KeepCurrentValues, entity);


                // This stmt does an update on only the changed fields

                if (_Flag)
                {
                    _DataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                }

                return true;
            }
            catch (ChangeConflictException e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " could not be EDITED...Confict occured -- " + e);
                return false;
            }
        }

        public bool Edit(IList<GenericEntityType> entitylist, bool isInContext)
        {
            try
            {
                if (entitylist == null)
                {
                    _Logger.Error(typeof(GenericEntityType).Name + " list is null to EDIT!");
                    return false;
                }
                if (!isInContext)
                {
                    _DataContext.GetTable<GenericEntityType>().AttachAll(entitylist);
                }
                // The foll stmt causes a refresh of the identity cache
                _DataContext.Refresh(RefreshMode.KeepCurrentValues, entitylist);
                // This stmt does an update on only the changed fields
                if (_Flag)
                {
                    _DataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                }

                return true;
            }
            catch (ChangeConflictException e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " list could not be EDITED...Confict occured -- " + e);
                return false;
            }
        }

        /// <summary>
        /// Method to edit generic entity.
        /// In case the Entity is already attached to DataContext, use 'true' as flag param. 
        /// Otherwise, use 'false'
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool Edit(GenericEntityType entity, bool flag)
        {
            try
            {
                if (entity == null)
                {
                    _Logger.Error(typeof(GenericEntityType).Name + " is null to EDIT!");
                    return false;
                }

                // This stmt does an update on only the changed fields
                if (!flag)
                {
                    _DataContext.GetTable<GenericEntityType>().Attach(entity);

                    // refreshing the identity cache
                    _DataContext.Refresh(RefreshMode.KeepCurrentValues, entity);
                }

                if (_Flag)
                {
                    _DataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                }

                return true;
            }
            catch (ChangeConflictException e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " could not be EDITED...Confict occured -- " + e);
                return false;
            }
        }

        public bool Delete(GenericEntityType entity)
        {

            try
            {
                _DataContext.GetTable<GenericEntityType>().Attach(entity);
                GetTable.DeleteOnSubmit(entity);
                if (_Flag)
                {
                    _DataContext.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " could not be DELETED -- " + e);
                return false;
            }
        }

        //public bool Delete(GenericEntityType entity, bool isInContext)
        //{

        //    try
        //    {
        //        //_DataContext.GetTable<GenericEntityType>().Attach(entity);
        //        GetTable.DeleteOnSubmit(entity);
        //        if (_Flag)
        //        {
        //            _DataContext.SubmitChanges();
        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        _Logger.Error(typeof(GenericEntityType).Name + " could not be DELETED -- " + e);
        //        return false;
        //    }
        //}


        public bool Delete(IList<GenericEntityType> entityList, bool isInContext)
        {
            if (entityList == null)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " list is null to DELETE!");
                return false;
            }
            try
            {

                if (!isInContext)
                {
                    _DataContext.GetTable<GenericEntityType>().AttachAll(entityList);
                }
                GetTable.DeleteAllOnSubmit(entityList);
                if (_Flag)
                {
                    _DataContext.SubmitChanges();
                }
                return true;

            }
            catch (Exception e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " list could not be DELETED -- " + e);
                return false;
            }
        }

        public bool Delete(GenericEntityType entity, bool isInContext)
        {
            try
            {
                if (!isInContext)
                {
                    _DataContext.GetTable<GenericEntityType>().Attach(entity);
                }

                GetTable.DeleteOnSubmit(entity);

                if (_Flag)
                {
                    _DataContext.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(typeof(GenericEntityType).Name + " could not be DELETED -- " + e);
                return false;
            }
        }

        public void Refresh(GenericEntityType entity)
        {
            _DataContext.Refresh(RefreshMode.KeepChanges, entity);
        }

        public void Refresh(IEnumerable<GenericEntityType> items)
        {
            _DataContext.Refresh(RefreshMode.KeepChanges, items);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Create a DataContext object
        /// </summary>
        /// <returns></returns>
        private DataContext CreateDataContext()
        {
            return new GenericContextType();
        }

        /// <summary>
        /// Get the Table for corresponding Entity
        /// </summary>
        protected Table<GenericEntityType> GetTable
        {
            get { return _DataContext.GetTable<GenericEntityType>(); }
        }

        /// <summary>
        /// Get the ITable by specified Type
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected ITable GetEntityTable(Type entity)
        {
            return _DataContext.GetTable(entity);
        }
        #endregion

        #region Public Methods
        public void Dispose()
        {
            _DataContext.Dispose();
        }
        #endregion
    }
}
