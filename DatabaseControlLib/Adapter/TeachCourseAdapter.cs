using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

namespace DatabaseControlLib
{
    public class TeachCourseAdapter : CommonAdapter
    {
        //构造函数
        public TeachCourseAdapter(DBase db)
            : base(db, "TeachCourse")
        {

        }

        public override DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(tableName);

            dataTable.Columns.Add("Cnum", Type.GetType("System.String"));
            dataTable.Columns.Add("Tnum", Type.GetType("System.String"));
            dataTable.Columns.Add("TCplace", Type.GetType("System.String"));
            dataSet.Tables.Add(dataTable);

            return dataSet;
        }

        public override void InitAdapter()
        {
            base.InitAdapter();

            OleDbConnection conn = db.getConnection();

            //创建InsertCommand
            OleDbCommand InsertCommand;
            InsertCommand = new OleDbCommand(
                "INSERT INTO " + tableName + "(Cnum,Tnum,TCplace) " +
                "VALUES(?,?,?);", conn);
            InsertCommand.Parameters.Add(
                "@Cnum", OleDbType.VarChar, 20, "Cnum");
            InsertCommand.Parameters.Add(
                "@Tnum", OleDbType.VarChar, 20, "Tnum");
            InsertCommand.Parameters.Add(
                "@TCplace", OleDbType.VarWChar, 100, "TCplace");
            odda.InsertCommand = InsertCommand;

            //创建UpdateCommand
            OleDbCommand UpdateCommand;
            UpdateCommand = new OleDbCommand(
                "UPDATE " + tableName + " SET TCplace=? WHERE (Cnum=? AND Tnum=?)", conn);
            UpdateCommand.Parameters.Add(
                "@TCplace", OleDbType.VarWChar, 100, "TCplace");
            UpdateCommand.Parameters.Add(
                "@Cnum", OleDbType.VarChar, 20, "Cnum");
            UpdateCommand.Parameters.Add(
                "@Tnum", OleDbType.VarChar, 20, "Tnum");
            odda.UpdateCommand = UpdateCommand;
        }

        public DataSet GetCourseByTeacherNum(string TeacherNum)
        {
            return SelectData("*", string.Format("Tnum = '{0}'", TeacherNum));
        }
    }
}
