using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.OleDb;
using System.Data;

namespace DatabaseControlLib
{
    public class SystemUserAdapter : UserAdapter
    {
        public SystemUserAdapter(DBase db)
            : base(db, "SystemUser")
        {
           
        }

        public override void InitAdapter()
        {
            OleDbConnection conn = db.getConnection(); 

            //创建InsertCommand
            OleDbCommand InsertCommand;                             
            InsertCommand = new OleDbCommand(
                "INSERT INTO SystemUser(UID,Username,Password,Canlogin) " +
                "VALUES(?,?,?,?);",conn);
            InsertCommand.Parameters.Add(
                "@UID", OleDbType.Integer,0,"UID");
            InsertCommand.Parameters.Add(
                "@Username", OleDbType.VarChar, 100,"Username");
            InsertCommand.Parameters.Add(
                "@Password", OleDbType.VarWChar, 255,"Password");
            InsertCommand.Parameters.Add(
                "@Canlogin", OleDbType.Boolean,0,"Canlogin");
            odda.InsertCommand = InsertCommand;

            //创建DeleteCommand
            OleDbCommand DeleteCommand;
            DeleteCommand = new OleDbCommand(
                "DELETE FROM SystemUser WHERE UID = ?", conn);
            DeleteCommand.Parameters.Add(
                "@UID", OleDbType.Integer, 0, "UID");
            odda.DeleteCommand = DeleteCommand;

        }

        public override DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(tableName);

            dataTable.Columns.Add("UID", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Username", Type.GetType("System.String"));
            dataTable.Columns.Add("Password", Type.GetType("System.String"));
            dataTable.Columns.Add("Canlogin", Type.GetType("System.Boolean"));
            dataSet.Tables.Add(dataTable);
            return dataSet;
        }

        public bool AddOneSystemUser(SystemUser sysUser)
        {
            bool reValue = true;
            DataSet dataSet = CreateEmptyDataSet();

            try
            {
                db.openConnection();
                dataSet.Tables[tableName].Rows.Add(
                    new object[] {sysUser.UID, sysUser.Username, sysUser.Password, sysUser.CanLogin});
                odda.Update(dataSet,tableName);               
            }
            catch
            {
                reValue = false;
            }
            finally
            {
                db.closeConnection();
            }
            
            return reValue;           
        }

        //获取在1-500已经使用的UID的最大值
        public int GetMaxUIDOfAdmin()
        {
            int uid = 0;
            DataSet dataSet = null;

            dataSet = SelectData("MAX(UID) as MaxUID", "UID<=500");
            if (dataSet != null &&
                dataSet.Tables[tableName].Rows.Count == 1)
            {
                DataRow dataRow = dataSet.Tables[tableName].Rows[0];
                object value = dataRow["MaxUID"];
                if (!DBNull.Value.Equals(value))
                    uid = (int)value;
            }
            else
                uid = -1;
            return uid;
        }

        //获取501-无穷已经使用的UID的最大值
        public int GetMaxUIDOfUser()
        {
            int uid = 500;
            DataSet dataSet = null;

            dataSet = SelectData("MAX(UID) as MaxUID", "UID>500");
            int count = dataSet.Tables[tableName].Rows.Count;
            if (dataSet != null &&
                dataSet.Tables[tableName].Rows.Count == 1)
            {
                DataRow dataRow = dataSet.Tables[tableName].Rows[0];
                object value = dataRow["MaxUID"];
                if (!DBNull.Value.Equals(value))
                    uid = (int)value;
            }
            else
                uid = -1;      
            return uid;
        }
    }
}
