﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Autonomy.Demo.Dal
{
    public interface IDAO<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T FindById(long id);
        List<T> Find(string strWhere, SqlParameter[] parameters);
        DataSet GetDataSet(string strWhere, SqlParameter[] parameters);
    }

    public class SqlDAO<T> : IDAO<T>
    {
        #region IDAO<T> Members

        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T FindById(long id)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> Find(string strWhere, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual DataSet GetDataSet(string strWhere, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }
        public virtual object FindByWhere(string StrWhere)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
                       </summary>
            <param name="c">The <c>AppenderCollection</c> whose elements are copied to the new collection.</param>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.#ctor(log4net.Appender.IAppender[])">
            <summary>
            Initializes a new instance of the <c>AppenderCollection</c> class
            that contains elements copied from the specified <see cref="T:log4net.Appender.IAppender"/> array.
            </summary>
            <param name="a">The <see cref="T:log4net.Appender.IAppender"/> array whose elements are copied to the new list.</param>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.#ctor(System.Collections.ICollection)">
            <summary>
            Initializes a new instance of the <c>AppenderCollection</c> class
            that contains elements copied from the specified <see cref="T:log4net.Appender.IAppender"/> collection.
            </summary>
            <param name="col">The <see cref="T:log4net.Appender.IAppender"/> collection whose elements are copied to the new list.</param>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.#ctor(log4net.Appender.AppenderCollection.Tag)">
            <summary>
            Allow subclasses to avoid our default constructors
            </summary>
            <param name="tag"></param>
            <exclude/>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.CopyTo(log4net.Appender.IAppender[])">
            <summary>
            Copies the entire <c>AppenderCollection</c> to a one-dimensional
            <see cref="T:log4net.Appender.IAppender"/> array.
            </summary>
            <param name="array">The one-dimensional <see cref="T:log4net.Appender.IAppender"/> array to copy to.</param>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.CopyTo(log4net.Appender.IAppender[],System.Int32)">
            <summary>
            Copies the entire <c>AppenderCollection</c> to a one-dimensional
            <see cref="T:log4net.Appender.IAppender"/> array, starting at the specified index of the target array.
            </summary>
            <param name="array">The one-dimensional <see cref="T:log4net.Appender.IAppender"/> array to copy to.</param>
            <param name="start">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.Add(log4ne