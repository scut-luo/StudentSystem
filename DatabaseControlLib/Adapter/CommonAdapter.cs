using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.OleDb;
using System.Data;

namespace DatabaseControlLib
{
    public abstract class CommonAdapter
    {
        protected OleDbDataAdapter odda;
        protected string tableName;     // 操作的数据表名
        protected DBase db;             //数据库连接

        public CommonAdapter(DBase datab, string tb_Name)
        {
            db = datab;
            tableName = tb_Name;

            odda = new OleDbDataAdapter();

            InitAdapter();
        }

        public virtual void InitAdapter()
        {

        }

        public virtual DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = null;
            return dataSet;
        }

        public DataSet SelectData(string FieldList = "*", string RowFilter = "1=1")
        {
            string cmdText = string.Format("SELECT {0} FROM {1} WHERE ({2});", FieldList, tableName, RowFilter);
            OleDbCommand cmd = null;
            DataSet dataSet = new DataSet();

            try
            {
                cmd = new OleDbCommand(cmdText, db.getConnection());
                odda.SelectCommand = cmd;       //设置SELECT
                db.openConnection();            //打开数据库连接

                odda.Fill(dataSet, tableName);
                //dataSet.AcceptChanges();

            }
            catch
            {
                dataSet = null;
            }
            finally
            {
                db.closeConnection();
            }

            return dataSet;
        }

        /*
        public virtual bool UpdateData(DataSet dataSet)
        {
            bool reValue = false;
            if (dataSet != null)
            {
                try
                {
                    db.openConnection();
                    odda.Update(dataSet, tableName);
                    reValue = true;
                }
                catch
                {
                    reValue = false;
                }
                finally
                {
                    db.closeConnection();
                }
            }
            return reValue;
        }
        */
        public void UpdateData(DataSet dataSet)
        {
            if (dataSet != null)
            {
                try
                {
                    db.openConnection();
                    odda.Update(dataSet, tableName);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    db.closeConnection();
                }
            }
        }
    }
}
