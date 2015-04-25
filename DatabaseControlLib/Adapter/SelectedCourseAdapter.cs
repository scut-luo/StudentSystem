using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

namespace DatabaseControlLib
{
    public class SelectedCourseAdapter : CommonAdapter
    {
        //构造函数
        public SelectedCourseAdapter(DBase db)
            : base(db, "SelectedCourse")
        {

        }

        public override DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(tableName);

            dataTable.Columns.Add("Snum", Type.GetType("System.String"));
            dataTable.Columns.Add("Cnum", Type.GetType("System.String"));
            dataTable.Columns.Add("Score", Type.GetType("System.Double"));
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
                "INSERT INTO " + tableName + "(Snum,Cnum,Score) " +
                "VALUES(?,?,?)", conn);
            InsertCommand.Parameters.Add(
                "@Snum", OleDbType.VarChar, 20, "Snum");
            InsertCommand.Parameters.Add(
                "@Cnum", OleDbType.VarChar, 20, "Cnum");
            InsertCommand.Parameters.Add(
                "@Score", OleDbType.Double, 0, "Score");
            odda.InsertCommand = InsertCommand;

            //创建UpdateCommand
            OleDbCommand UpdateCommand;
            UpdateCommand = new OleDbCommand(
                "UPDATE " + tableName + " SET Score = ? WHERE Snum = ? AND Cnum = ?", conn);            
            UpdateCommand.Parameters.Add(
                "@Score", OleDbType.Double, 0, "Score");
            UpdateCommand.Parameters.Add(
                "@Snum", OleDbType.VarChar, 20, "Snum");
            UpdateCommand.Parameters.Add(
                "@Cnum", OleDbType.VarChar, 20, "Cnum");
            odda.UpdateCommand = UpdateCommand;

            //创建DeleteCommand
            OleDbCommand DeleteCommand;
            DeleteCommand = new OleDbCommand(
                "DELETE FROM " + tableName + " WHERE Snum=?", conn);
            DeleteCommand.Parameters.Add(
                "@Snum", OleDbType.VarChar, 20, "Snum");
            odda.DeleteCommand = DeleteCommand;
        }

        public DataSet GetSelectedCourseByStudentNum(string StudentNum)
        {
            return SelectData("*", string.Format("Snum = '{0}'", StudentNum));
        }
    }
}
