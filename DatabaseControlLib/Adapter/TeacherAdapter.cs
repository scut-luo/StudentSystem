using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

namespace DatabaseControlLib
{
    //教师表Teacher操作接口
    public class TeacherAdapter : CommonAdapter
    {
        //构造函数
        public TeacherAdapter(DBase db)
            : base(db, "Teacher")
        {

        }

        public override DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(tableName);
            
            dataTable.Columns.Add("Tnum", Type.GetType("System.String"));
            dataTable.Columns.Add("Tname", Type.GetType("System.String"));
            dataTable.Columns.Add("Tphone", Type.GetType("System.String"));
            dataTable.Columns.Add("Tphoto", Type.GetType("System.Byte[]"));
            dataTable.Columns.Add("Anum", Type.GetType("System.Int32"));
            dataTable.Columns.Add("id", Type.GetType("System.Int32"));
            dataSet.Tables.Add(dataTable);

            return dataSet;
        }

        public override void InitAdapter()
        {
            base.InitAdapter();

            OleDbConnection conn = db.getConnection();

            OleDbCommand UpdateCommand;
            UpdateCommand = new OleDbCommand(
                "UPDATE " + tableName + " SET Tnum=?,Tname=?,Tphone=?,Tphoto=?,Anum=? " +
                "WHERE id = ?",conn);
            UpdateCommand.Parameters.Add("@Tnum", OleDbType.VarChar, 20, "Tnum");
            UpdateCommand.Parameters.Add("@Tname", OleDbType.VarWChar, 50, "Tname");
            UpdateCommand.Parameters.Add("@Tphone", OleDbType.VarChar, 50, "Tphone");
            UpdateCommand.Parameters.Add("@Tphoto", OleDbType.VarBinary, 0, "Tphoto");
            UpdateCommand.Parameters.Add("@Anum", OleDbType.Integer, 0, "Anum");
            UpdateCommand.Parameters.Add("@id", OleDbType.Integer, 0, "id");
            odda.UpdateCommand = UpdateCommand;

            OleDbCommand InsertCommand;
            InsertCommand = new OleDbCommand(
                "INSERT INTO " + tableName + "(Tnum,Tname,Tphone,Tphoto,Anum) " +
                "VALUES(?,?,?,?,?)", conn);
            InsertCommand.Parameters.Add("@Tnum", OleDbType.VarChar, 20, "Tnum");
            InsertCommand.Parameters.Add("@Tname", OleDbType.VarWChar, 50, "Tname");
            InsertCommand.Parameters.Add("@Tphone", OleDbType.VarChar, 50, "Tphone");
            InsertCommand.Parameters.Add("@Tphoto", OleDbType.VarBinary, 0, "Tphoto");
            InsertCommand.Parameters.Add("@Anum", OleDbType.Integer, 0, "Anum");
            odda.InsertCommand = InsertCommand;

            OleDbCommand DeleteCommand;
            DeleteCommand = new OleDbCommand(
                "DELETE FROM " + tableName + " WHERE Tnum=?", conn);
            DeleteCommand.Parameters.Add("@Tnum", OleDbType.VarChar, 20, "Tnum");
            odda.DeleteCommand = DeleteCommand;
        }

        public DataSet GetTeacherByAcademy(int AcademyNum)
        {
            return SelectData("*", string.Format("Anum = {0}", AcademyNum));
        }

        public DataSet GetTeacherByTeacherNum(string TeacherNum)
        {
            return SelectData("*", string.Format("Tnum = '{0}'",TeacherNum));
        }
    }
}
