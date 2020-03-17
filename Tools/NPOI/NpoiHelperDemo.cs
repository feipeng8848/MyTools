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
		
        /*导入excel
		private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            string ExcleFilePath = string.Empty;
            if (ofd.ShowDialog() == true)
            {
                ExcleFilePath = ofd.FileName;
            }
            else
            {
                return;
            }

            try
            {
                NpoiHelper npoiHelper = new NpoiHelper();
                var excelTable = npoiHelper.GetSheetTable(ExcleFilePath,0,true);

                List<AttendancePerson> People = new List<AttendancePerson>();
                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        continue;//跳过首行标题
                    }
                    AttendancePerson p = new AttendancePerson()
                    {
                        Name = excelTable.Rows[i][1] == null ? "" : excelTable.Rows[i][1].ToString(),
                        IDNum = excelTable.Rows[i][2] == null ? "" : excelTable.Rows[i][2].ToString(),
                        PhoneNum = excelTable.Rows[i][4] == null ? "" : excelTable.Rows[i][4].ToString(),
                        Org = excelTable.Rows[i][3] == null ? "" : excelTable.Rows[i][3].ToString(),
                    };
                    People.Add(p);
                }
                CustomerConferenceDB.GetInstance().AddPerson(People);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.TargetSite.ToString());
            }

        }
        */
    }
}
