using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using System.Data.OleDb;
using System.Data;

namespace DatabaseControlLib
{
    public class StudentAdapter : CommonAdapter
    {        
        
        public StudentAdapter(DBase datab)
            : base(datab,"Student")
        {
        }

        public override DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(tableName);

            //dataTable.Columns.Add("id", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Snum", Type.GetType("System.String"));
            dataTable.Columns.Add("Sname", Type.GetType("System.String"));
            dataTable.Columns.Add("Ssex", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Sphoto", Type.GetType("System.Byte[]"));
            dataTable.Columns.Add("Anum", Type.GetType("System.Int32"));
            dataSet.Tables.Add(dataTable);

            return dataSet;
        }

        public override void InitAdapter()
        {
            OleDbConnection conn = db.getConnection();
            string commandText;
            OleDbCommand command;

            commandText = "UPDATE Student SET " +
                "Snum = ?," +
                "Sname = ?," +
                "Ssex = ?," +
                "Sphoto = ?," +
                "Anum = ? " +
                "WHERE (id = ?) ;";
            command = new OleDbCommand(commandText, conn);
            command.CommandType = System.Data.CommandType.Text;
            
            command.Parameters.Add("@Snum", OleDbType.VarChar, 20,"Snum");
            command.Parameters.Add("@Sname", OleDbType.VarWChar, 50,"Sname");
            command.Parameters.Add("@Ssex", OleDbType.Integer,0,"Ssex");
            command.Parameters.Add("@Sphoto", OleDbType.VarBinary,0,"Sphoto");
            command.Parameters.Add("@Anum", OleDbType.Integer, 0, "Anum");
            command.Parameters.Add("@id", OleDbType.Integer,0,"id");

            odda.UpdateCommand = command;

            //创建InsertCommand
            OleDbCommand InsertCommand;
            InsertCommand = new OleDbCommand(
                "INSERT INTO " + tableName + "(Snum,Sname,Ssex,Sphoto,Anum)" +
                "VALUES(?,?,?,?,?)", conn);
            InsertCommand.Parameters.Add("@Snum", OleDbType.VarChar, 20, "Snum");
            InsertCommand.Parameters.Add("@Sname", OleDbType.VarWChar, 50, "Sname");
            InsertCommand.Parameters.Add("@Ssex", OleDbType.Integer, 0, "Ssex");
            InsertCommand.Parameters.Add("@Sphoto", OleDbType.VarBinary, 0, "Sphoto");
            InsertCommand.Parameters.Add("@Anum", OleDbType.Integer, 0, "Anum");
            odda.InsertCommand = InsertCommand;

            //创建DeleteCommand
            OleDbCommand DeleteCommand;
            DeleteCommand = new OleDbCommand(
                "DELETE FROM " + tableName + " WHERE Snum=?", conn);
            DeleteCommand.Parameters.Add("@Snum", OleDbType.VarChar, 20, "Snum");
            odda.DeleteCommand = DeleteCommand;

        }

        //获取指定学生的信息
        public DataSet GetStudentInfo(string StudentNum)
        {
            return SelectData("*",
                string.Format("Snum = '{0}'", StudentNum));
        }

        //根据学院号获得学生列表
        public DataSet GetStudentInfoByAcademyNum(string AcademyNum)
        {
            return SelectData("*",
                string.Format("Anum = '{0}'", AcademyNum));
        }
    }
}
