using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using Modelo;
using System.Collections;
using System.Data;
using excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using MahApps.Metro.Controls.Dialogs;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmReporteGastos.xaml
    /// </summary>
    public partial class frmReporteGastos : MetroWindow
    {

        CrystalReport1 rptDoc = new CrystalReport1();
        public frmReporteGastos()
        {
            InitializeComponent();
            dtdFechaInicio.DisplayDateEnd = DateTime.Now;
            dtdFechaFin.DisplayDateEnd = DateTime.Now;
        }

        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        public static System.Data.DataTable DataGridtoDataTable(DataGrid dg)
        {


            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);

            System.Data.DataTable dt = new System.Data.DataTable();
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {

                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;

        }






        private void WriteToExcel(System.Data.DataTable dt, string location)
        {
            //instantiate excel objects (application, workbook, worksheets)
            excel.Application XlObj = new excel.Application();
            XlObj.Visible = false;
            excel._Workbook WbObj = (excel.Workbook)(XlObj.Workbooks.Add(""));
            excel._Worksheet WsObj = (excel.Worksheet)WbObj.ActiveSheet;



            //run through datatable and assign cells to values of datatable

            int row = 1; int col = 1;
            foreach (DataColumn column in dt.Columns)
            {
                //adding columns
                WsObj.Cells[row, col] = column.ColumnName;
                col++;
            }
            //reset column and row variables
            col = 1;
            row++;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //adding data
                foreach (var cell in dt.Rows[i].ItemArray)
                {
                    WsObj.Cells[row, col] = cell;
                    col++;
                }
                col = 1;
                row++;
            }


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "EXCEL.EXE";
            startInfo.Arguments = @"C:\Users\Caty\Desktop\Teste.xlsm";

            if (!System.IO.File.Exists(location))
            {

                WbObj.SaveAs(location, excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled,
                    null, null, false, false, excel.XlSaveAsAccessMode.xlNoChange,
                        false, false, null, null, null);
                WbObj.Close();


                Process.Start(startInfo);
            }
            else
            {
                //System.IO.File.Delete(location);
                WbObj.SaveAs(location);
                WbObj.Close();
                Process.Start(startInfo);
            }
        }

        private void dtdFechaFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dtdFechaInicio.SelectedDate.Equals(null))
            {
                if (dtdFechaInicio.SelectedDate <= dtdFechaFin.SelectedDate)
                {
                    rptDoc.Load("C:\\Users\\Caty\\Documents\\GitHub\\CoffeeLand.Metro\\Proyecto\\CoffeeLand\\CrystalReport1.rpt");

                    rptDoc.SetDataSource(MreporteGastos.GetInstance().funcionreportegastos(DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()), DateTime.Parse(dtdFechaFin.SelectedDate.ToString())) as IEnumerable);

                    crystalReportsViewer1.ViewerCore.ReportSource = rptDoc;
                
                }
                else
                {
                    mensajeError("La fecha inicial, no puede ser mayor que la fecha final.");
                    dtdFechaFin.SelectedDate = null;
                }
            }
            else
            {
                mensajeError("Por favor seleccione la fecha inicial.");
                dtdFechaFin.SelectedDate = null;
            }
        }

  

        private void button_Click(object sender, RoutedEventArgs e)
        {

            //CrystalReport1 cryRpt = new CrystalReport1();
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\Caty\\Desktop\\InformeGastos.pdf";
                CrExportOptions = rptDoc.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                rptDoc.Export();
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString().Equals("-2147215870"))
                {
                    mensajeError("Por favor seleccione las fechas para generar el informe.");
                }
              
            }


        }
    }

}
