using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

namespace DatabaseControlLib
{
    public class CourseAdapter : CommonAdapter
    {
        //构造函数
        public CourseAdapter(DBase db)
            : base(db, "Course")
        {

        }

        public override DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(tableName);
            
            dataTable.Columns.Add("Cnum", Type.GetType("System.String"));
            dataTable.Columns.Add("Cname", Type.GetType("System.String"));
            dataTable.Columns.Add("Ccredit", Type.GetType("System.Double"));
            dataTable.Columns.Add("Chours", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Anum", Type.GetType("System.Int32"));
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
                "INSERT INTO " + tableName + "(Cnum,Cname,Ccredit,Chours,Anum) " +
                "VALUES(?,?,?,?,?)", conn);
            InsertCommand.Parameters.Add(
                "@Cnum", OleDbType.VarChar, 20, "Cnum");
            InsertCommand.Parameters.Add(
                "@Cname", OleDbType.VarWChar, 50, "Cname");
            InsertCommand.Parameters.Add(
                "@Ccredit", OleDbType.Double, 0, "Ccredit");
            InsertCommand.Parameters.Add(
                "@Chours", OleDbType.Integer, 0, "Chours");
            InsertCommand.Parameters.Add(
                "@Anum", OleDbType.Integer, 0, "Anum");
            odda.InsertCommand = InsertCommand;

            //创建UpdateCommand
            OleDbCommand UpdateCommand;
            UpdateCommand = new OleDbCommand(
                "UPDATE " + tableName + " SET Cname=?,Ccredit=?,Chours=?,Anum=? " +
                "WHERE Cnum=?",conn);
            UpdateCommand.Parameters.Add(
                "@Cname", OleDbType.VarWChar, 50, "Cname");
            UpdateCommand.Parameters.Add(
                "@Ccredit", OleDbType.Double, 0, "Ccredit");
            UpdateCommand.Parameters.Add(
                "@Chours", OleDbType.Integer, 0, "Chours");
            UpdateCommand.Parameters.Add(
                "@Anum", OleDbType.Integer, 0, "Anum");
            UpdateCommand.Parameters.Add(
                "@Cnum", OleDbType.VarChar, 20, "Cnum");
            odda.UpdateCommand = UpdateCommand;

            //创建DeleteCommand
            OleDbCommand DeleteCommand;
            DeleteCommand = new OleDbCommand(
                "DELETE FROM " + tableName + " WHERE Cnum=?", conn);
            DeleteCommand.Parameters.Add(
                "@Cnum", OleDbType.VarChar, 20, "Cnum");
            odda.DeleteCommand = DeleteCommand;
        }

        public DataSet GetCourseByCourseNum(string CourseNum)
        {
            return SelectData("*", string.Format("Cnum = '{0}'", CourseNum));
        }

        public DataSet GetCourseByAcademy(int AcademyNum)
        {
            return SelectData("*", string.Format("Anum = {0}", AcademyNum));
        }

        public void AddOneCourse(Course course)
        {
            DataSet dataSet = CreateEmptyDataSet();
            try
            {
                db.openConnection();
                dataSet.Tables[tableName].Rows.Add(
                    new object[] { course.Cnum, course.Cname, course.Ccredit, course.Chours, course.Anum });
                odda.Update(dataSet, tableName);

            }
            catch (Exception ex)
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
