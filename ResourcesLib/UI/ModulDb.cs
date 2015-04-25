using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections.ObjectModel;
using DatabaseControlLib;

namespace ResourcesLib.UI
{
    public class ModulDb
    {
        //获取所有Modul
        public ICollection<Modul> GetAllModuls()
        {
            DataSet ds = UIDataset.ReadDataSet();

            ObservableCollection<Modul> moduls = new ObservableCollection<Modul>();
            foreach (DataRow modulRow in ds.Tables["Modul"].Rows)
            {
                moduls.Add(new Modul((int)modulRow["ModulNum"],
                    (string)modulRow["ModulName"]));
            }
            return moduls;
        }

        //获取指定UserType的所有Modul
        public ICollection<Modul> GetModulsFromUserType(string UserType)
        {
            DataTable dt;
            DataSet ds = UIDataset.ReadDataSet();
            DataSetHelper dsHelper = new DataSetHelper(ref ds);
            ObservableCollection<Modul> moduls = new ObservableCollection<Modul>();

            //添加关系
            ds.Relations.Add("ModulAndUserType", ds.Tables["Modul"].Columns["ModulNum"],
                ds.Tables["UserTypeModul"].Columns["ModulNum"]);
            //多表查询
            dt = dsHelper.SelectJoinInto("UserTypeModuls", ds.Tables["UserTypeModul"],
                "ModulNum,ModulAndUserType.ModulName", string.Format("UserType='{0}'", UserType), "ModulNum");

            DataTable distinct_table = dt.DefaultView.ToTable(true,new string[]{"ModulNum","ModulName"});
            foreach (DataRow modulRow in distinct_table.Rows)
            {
                moduls.Add(new Modul((int)modulRow["ModulNum"],
                    (string)modulRow["ModulName"]));
            }
            return moduls;
        }

        
        //获取指定UserType的Modul的子Modul
        public ICollection<ModulDetail> GetModulDetailsFromUserType(string userType, int modulNum)
        {
            if (userType == null)
                throw new Exception("用户类型未指定！");
            DataSet ds = UIDataset.ReadDataSet();
            DataTable modulDetail_Table = ds.Tables["ModulDetail"],
               userTypeModul_Table = ds.Tables["UserTypeModul"];
            ObservableCollection<ModulDetail> modulDetails = new ObservableCollection<ModulDetail>();

            //根据用户类型和模块(Modul)编号获取子Modul的编号            
            DataRow[] userTypeModul_Rows = userTypeModul_Table.Select(
                string.Format("UserType='{0}' AND ModulNum={1}", userType, modulNum),
                "DetailNum");

            if (userTypeModul_Rows.Length != 0)
            {
                DataRow[] modulDetail_Rows;
                DataRow modulDetail_Row;
                int detailNum = 0;
                foreach (DataRow foundRow in userTypeModul_Rows)
                {
                    detailNum = (int)foundRow["DetailNum"];    //获取子Modul的编号
                    modulDetail_Rows = modulDetail_Table.Select(string.Format("DetailNum={0} AND ModulNum={1}", detailNum,modulNum));

                    //根据子Modul编号招不到该子Modul的所有信息
                    if (modulDetail_Rows.Length == 0)       
                        throw new Exception(string.Format("找不到子模块编号为：{0}的信息", detailNum));
                    
                    modulDetail_Row = modulDetail_Rows[0];
                    modulDetails.Add(new ModulDetail((int)modulDetail_Row["DetailNum"], (int)modulDetail_Row["ModulNum"],
                        (string)modulDetail_Row["ShowImage"], (string)modulDetail_Row["DetailName"],
                        (string)modulDetail_Row["UriPath"]));
                }
            }            
            return modulDetails;
        }
    }
}
