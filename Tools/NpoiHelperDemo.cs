using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Tools
{
    class NpoiHelperDemo
    {
        //
        //NpoiHelper类的使用方法
        //

        public void open()
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() != true)
            //{
            //    return;
            //}

            var Npoi = new NpoiHelper();
            try
            {
                DataTable ExcelSheet = Npoi.GetSheetTable("filename", 0, true);//openFileDialog.FileName
                if (ExcelSheet == null)
                {
                    //没有成功加载;
                    return;
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void save()
        {
            var Npoi = new NpoiHelper();
            DataSet myDataset = new DataSet();
            DataTable myDataTable = new DataTable();
            myDataset.Tables.Add(myDataTable);
            Npoi.SaveFile(myDataset, "d://myNewFile.xls");
            Npoi.SaveFile(myDataset, "d://myNewFile.xlsx");
            //一定要有这一行
            myDataset.Tables.Remove(myDataTable);
        }
    }
}
