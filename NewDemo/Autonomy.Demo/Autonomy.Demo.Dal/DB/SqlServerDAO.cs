using System.Data;
using System.Data.SqlClient;

using Autonomy.Demo.Util;

namespace Autonomy.Demo.Dal
{
    public class SqlServerDAO : DAO
    {
        public override IDbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        #region parameter type
        protected override IDbDataParameter String
        {
            get { return new SqlParameter(string.Empty, SqlDbType.VarChar); }
        }
        protected override IDbDataParameter TinyInt
        {
            get { return new SqlParameter(string.Empty, SqlDbType.TinyInt); }
        }
        protected override IDbDataParameter SmallInt
        {
            get { return new SqlParameter(string.Empty, SqlDbType.SmallInt); }
        }
        protected override IDbDataParameter Int
        {
            get { return new SqlParameter(string.Empty, SqlDbType.Int); }
        }
        protected override IDbDataParameter BigInt
        {
            get { return new SqlParameter(string.Empty, SqlDbType.BigInt); }
        }
        protected override IDbDataParameter Float
        {
            get { return new SqlParameter(string.Empty, SqlDbType.Float); }
        }
        protected override IDbDataParameter DateTime
        {
            get { return new SqlParameter(string.Empty, SqlDbType.DateTime); }
        }
        protected override IDbDataParameter Bool
        {
            get { return new SqlParameter(string.Empty, SqlDbType.Bit); }
        }
        protected override IDbDataParameter Text
        {
            get { return new SqlParameter(string.Empty, SqlDbType.Text); }
        }
        protected override IDbDataParameter Image
        {
            get { return new SqlParameter(string.Empty, SqlDbType.Image); }
        }
        protected override IDbDataParameter GUID
        {
            get { return new SqlParameter(string.Empty, SqlDbType.UniqueIdentifier); }
        }
        #endregion 

    }
}
                                <summary>
            Supports type-safe iteration over a <see cref="T:log4net.Appender.AppenderCollection"/>.
            </summary>
            <exclude/>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.IAppenderCollectionEnumerator.MoveNext">
            <summary>
            Advances the enumerator to the next element in the collection.
            </summary>
            <returns>
            <c>true</c> if the enumerator was successfully advanced to the next element; 
            <c>false</c> if the enumerator has passed the end of the collection.
            </returns>
            <exception cref="T:System.InvalidOperationException">
            The collection was modified after the enumerator was created.
            </exception>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.IAppenderCollectionEnumerator.Reset">
            <summary>
            Sets the enumerator to its initial position, before the first element in the collection.
            </summary>
        </member>
        <member name="P:log4net.Appender.AppenderCollection.IAppenderCollectionEnumerator.Current">
            <summary>
            Gets the current element in the collection.
            </summary>
        </member>
        <member name="T:log4net.Appender.AppenderCollection.Tag">
            <summary>
            Type visible only to our subclasses
            Used to access protected constructor
            </summary>
            <exclude/>
        </member>
        <member name="F:log4net.Appender.AppenderCollection.Tag.Default">
            <summary>
            A value
            </summary>
        </member>
        <member name="T:log4net.Appender.AppenderCollection.Enumerator">
            <summary>
            Supports simple iteration over a <see cref="T:log4net.Appender.AppenderCollection"/>.
            </summary>
            <exclude/>
        </member>
        <member name="M:log4net.Appender.AppenderCollection.Enu