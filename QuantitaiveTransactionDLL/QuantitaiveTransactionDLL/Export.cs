using System;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace QuantitaiveTransactionDLL
{
    class Export
    {
        /// <summary> 
        /// data set to excel
        /// </summary> 
        /// <param name="dataSet">input data set</param> 
        /// <param name="isShowExcle">show the excel</param> 
        /// <returns></returns> 
        public string DataSetToExcel(DataSet dataSet, bool isShowExcle)
        {
            System.Data.DataTable dataTable = dataSet.Tables[0];
            int rowNumber = dataTable.Rows.Count; 
            int columnNumber = dataTable.Columns.Count;
            int colIndex = 0;
            if (rowNumber == 0) return "empty dataset";
            

            
            Application excel = new Application();
           
            Workbook workbook = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
            excel.Visible = isShowExcle;
            
            Range range;
            
            foreach (DataColumn col in dataTable.Columns)
            {
                excel.Cells[1, colIndex++] = col.ColumnName;
            }

            object[,] objData = new object[rowNumber, columnNumber];

            for (int r = 0; r < rowNumber; r++)
            {
                for (int c = 0; c < columnNumber; c++)
                {
                    objData[r, c] = dataTable.Rows[r][c];
                } 
            }

            range = worksheet.get_Range(excel.Cells[2, 1], excel.Cells[rowNumber + 1, columnNumber]);
            
            range.Value2 = objData;
            worksheet.get_Range(excel.Cells[2, 1], excel.Cells[rowNumber + 1, 1]).NumberFormat = "yyyy-m-d h:mm";

            return "success";
        }

        /// <summary> 
        /// data set to excel
        /// </summary> 
        /// <param name="dataSet">input data set</param> 
        /// <param name="fileName">the target filename</param> 
        /// <param name="isShowExcle">show the excel</param> 
        /// <returns></returns> 
        public string DataSetToExcel(DataSet dataSet, string fileName, bool isShowExcle)
        {
            System.Data.DataTable dataTable = dataSet.Tables[0];
            int rowNumber = dataTable.Rows.Count;//不包括字段名 
            int columnNumber = dataTable.Columns.Count;
            int colIndex = 0;

            if (rowNumber == 0) return "empty dataset";

            //建立Excel对象 
            Application excel = new Application();
            //excel.Application.Workbooks.Add(true); 
            Workbook workbook = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
            excel.Visible = false;
            //Worksheet worksheet = (Worksheet)excel.Worksheets[1]; 
            Range range;

            //生成字段名称 
            foreach (DataColumn col in dataTable.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            object[,] objData = new object[rowNumber, columnNumber];

            for (int r = 0; r < rowNumber; r++)
            {
                for (int c = 0; c < columnNumber; c++)
                {
                    objData[r, c] = dataTable.Rows[r][c];
                }
                //Application.DoEvents(); 
            }

            // 写入Excel 
            range = worksheet.get_Range(excel.Cells[2, 1], excel.Cells[rowNumber + 1, columnNumber]);
            //range.NumberFormat = "@";//设置单元格为文本格式 
            range.Value2 = objData;
            worksheet.get_Range(excel.Cells[2, 1], excel.Cells[rowNumber + 1, 1]).NumberFormat = "yyyy-m-d h:mm";

            //string fileName = path + "\\" + DateTime.Now.ToString().Replace(':', '_') + ".xls"; 
            workbook.SaveAs(fileName,XlSaveAsAccessMode.xlNoChange);

            try
            {
                workbook.Saved = true;
                excel.UserControl = false;
 
            }
            catch (Exception exception)
            {
              return exception.Message.ToString();
            }
            finally
            {
                workbook.Close(XlSaveAction.xlSaveChanges);
                excel.Quit();
            }

            if (isShowExcle)
            {
                System.Diagnostics.Process.Start(fileName);
            }
            return "success";
        }
    }
}

