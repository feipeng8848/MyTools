using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace Tools
{
    public class NpoiHelper
    {
        #region private

        /// <summary>
        /// 根据文件名得到WorkBook
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private IWorkbook GetWkBk(string fileName)
        {
            IWorkbook WkBk = null;
            //HSSF使用于2007之前的xls版本，XSSF适用于2007及其之后的xlsx版本
            using (FileStream fs = File.OpenRead(@fileName))   //打开myxls.xls文件
            {
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    WkBk = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    WkBk = new HSSFWorkbook(fs);
            }

            return WkBk;
        }

        #endregion


        #region public方法
        /// <summary>
        /// 读取指定excel文件sheet数据到dataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetNum"></param>
        /// <param name="isSkipNullValue">设置为True就会跳过空单元格或者空行，可能与原数据表有出入，false的时候如果有null值3就会报错</param>
        /// <returns></returns>
        public DataTable GetSheetTable(string fileName, int sheetNum, bool isSkipNullValue = false)
        {

            IWorkbook WkBk = GetWkBk(fileName);
            if (WkBk == null)
            {
                return null;
            }

            if (sheetNum >= WkBk.NumberOfSheets || sheetNum < 0)
            {
                //指定的sheet大于文件所包含的sheet数目
                return null;
            }
            ISheet ExcelSheet = WkBk.GetSheetAt(sheetNum);

            //创建DataTable列
            DataTable Table = new DataTable();
            IRow FirstExcelRow = ExcelSheet.GetRow(0);
            if (FirstExcelRow == null)
            {
                return null;
            }
            int ColumnCount = FirstExcelRow.LastCellNum;
            for (int i = 0; i < ColumnCount; i++)
            {
                var column = new DataColumn();
                Table.Columns.Add(column);
            }

            //获取数据
            for (int i = 0; i <= ExcelSheet.LastRowNum; i++)
            {
                var row = Table.NewRow();
                IRow ExcelRow = ExcelSheet.GetRow(i);
                if (ExcelRow == null && isSkipNullValue == false)
                {
                    throw new Exception("读取第" + (i + 1) + "行失败！");
                }
                else if (ExcelRow == null && isSkipNullValue == true)
                {
                    Table.Rows.Add(Table.NewRow());
                    continue;
                }
                for (int j = 0; j < ColumnCount; j++)
                {
                    ICell ExcelCell = ExcelRow.GetCell(j);
                    if (ExcelCell == null && isSkipNullValue == false)
                    {
                        throw new Exception("读取第" + (i+1) + "行第" + (j+1) + "列失败！");
                    }
                    else if (ExcelCell == null && isSkipNullValue == true)
                    {
                        row[j] = string.Empty;
                        continue;
                    }
                    row[j] = ExcelCell;
                }
                Table.Rows.Add(row);
            }

            return Table;
        }


        /// <summary>
        /// 保存Dataset成Excel文件，每个DataTabel作为一个Sheet
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="fileName"></param>
        public void SaveFile(DataSet dataSet, string fileName)
        {

            IWorkbook WkBk = null;

            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                WkBk = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                WkBk = new HSSFWorkbook();

            if (WkBk == null)
            {
                throw new Exception("保存Excel失败！");
            }

            foreach (DataTable table in dataSet.Tables)
            {
                ISheet ExcelSheet =  WkBk.CreateSheet();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    IRow ExcelRow = ExcelSheet.CreateRow(i);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        ICell ExcelCell = ExcelRow.CreateCell(j);
                        ExcelCell.SetCellValue(table.Rows[i][j].ToString());
                    }
                }
            }

            //打开一个xls文件，如果没有则自行创建
            using (FileStream fs = File.OpenWrite(@fileName)) 
            {
                WkBk.Write(fs);
            }
        }

        #endregion
    }
}
