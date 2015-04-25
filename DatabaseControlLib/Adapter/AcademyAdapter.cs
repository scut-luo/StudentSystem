using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Data;

namespace DatabaseControlLib
{
    public class AcademyAdapter : CommonAdapter
    {
        //构造函数
        public AcademyAdapter(DBase db)
            : base(db, "Academy")
        {

        }

        public override void InitAdapter()
        {
            base.InitAdapter();

            OleDbConnection conn = db.getConnection();

            //创建InsertCommand
            OleDbCommand InsertCommand;
            InsertCommand = new OleDbCommand(
                "INSERT INTO " + tableName + "(Anum,Aname) " +
                "VALUES(?,?);", conn);
            InsertCommand.Parameters.Add(
                "@Anum", OleDbType.Integer, 0, "Anum");
            InsertCommand.Parameters.Add(
                "@Aname", OleDbType.VarWChar, 50, "Aname");
            odda.InsertCommand = InsertCommand;
        }
        public override DataSet CreateEmptyDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(tableName);

            dataTable.Columns.Add("Anum", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Aname", Type.GetType("System.String"));
            dataSet.Tables.Add(dataTable);
            return dataSet;
        }

        public void AddOneAcademy(Academy academy)
        {
            DataSet dataSet = CreateEmptyDataSet();
            try
            {
                db.openConnection();
                dataSet.Tables[tableName].Rows.Add(
                    new object[] { academy.Anum, academy.Aname });
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

        public ICollection<Academy> GetAllAcademyC()
        {
            ObservableCollection<Academy> AcademyList = 
                    new ObservableCollection<Academy>();
            try
            {                
                DataSet dataSet = SelectData();
                foreach (DataRow oneRow in dataSet.Tables[tableName].Rows)
                {
                    AcademyList.Add(new Academy(Convert.ToInt32(oneRow["Anum"].ToString()),
                                                oneRow["Aname"].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return AcademyList;
        }
    }
}
