using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using LibReports;

namespace LibReports.Codebeat.Production_CB
{
    public partial class Production : DevExpress.XtraReports.UI.XtraReport
    {
        
        private PointF myPoint;
        private float currentWidth;
        private List<string> productIds;
        Filters myFilters;
        Database_ ProductionDataCB;
    
        //Filter Variables
        //private string batchName = "testing";
        //private string recipeName = "testing";
        //private string userName = "";
        //private string startDate = "";
        //private string endDate = "";
        //private string filterString;

        public Production()
        {
            InitializeComponent();

            this.myPoint = new PointF(0, 0);
            this.currentWidth = this.RootReport.PageWidth - this.Margins.Left - this.Margins.Right;
            this.productIds = new List<string>();
            this.ProductionDataCB = new Database_();
            this.ProductionDataCB.BuildConnection();


            //GenerateFilterString();
            //List<string> a = new List<string> { "testing"};
            //List<string> b = new List<string> { "testing" };
            //List<string> c = new List<string>();
            //myFilters = new Filters(a, b, c, "", "");

            //AddDefectSummeryDatamatrix();
            //AddDefectSummeryOCR();
            //AddProductionSummery();
            //AddInformation();
            //AddDefectSummeryBarcode();
            //AddDefectSummeryArtWork();
            //AddDefectSummeryPharmacode();
            //AddDefectSummeryOCV();

            this.PopulateReport();
        }


        private void PopulateReport()
        {
            //GenerateFilterString();
            List<string> a = new List<string> { "gg", "testing" };

            List<string> b = new List<string> { "uy", "testing" };
            List<string> c = new List<string>();
            myFilters = new Filters(a, b, c, "", "");
            //string qry = "SELECT Id FROM span_db.production " + this.myFilters.formatString;
            //this.ProductionDataCB.SelectMultiple(qry, ref productIds);


            foreach(var recipe in myFilters.GetRecipe())
            {
                foreach(var batch in myFilters.GetBatch())
                {
                    string checkQry = "SELECT COUNT(Id) FROM span_db.production WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
                    if (this.ProductionDataCB.SelectGetCount(checkQry)>0)
                    {
                        this.AddInformation(recipe, batch);
                        this.AddProductionSummery(recipe, batch);

                        //
                        //Check if Datamatrix has the entry
                        string currentQry = "SELECT COUNT(Id) FROM span_db_codebeat_codereader.datamatrix_production_defects WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
                        if (this.ProductionDataCB.SelectGetCount(currentQry) > 0)
                        {
                            this.AddDefectSummeryDatamatrix(recipe, batch);
                        }

                        //
                        //Check if OCR has entry for given recipe and batach
                        currentQry = "SELECT COUNT(Id) FROM span_db_codebeat_codereader.ocr_production_defects WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
                        if (this.ProductionDataCB.SelectGetCount(currentQry) > 0)
                        {
                            this.AddDefectSummeryOCR(recipe, batch);
                        }

                        //
                        //Check if Artwork has entry for given recipe and batach
                        currentQry = "SELECT COUNT(Id) FROM span_db_codebeat_codereader.artwork_production_defects WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
                        if (this.ProductionDataCB.SelectGetCount(currentQry) > 0)
                        {
                            //this.AddDefectSummeryOCR();
                        }

                        //
                        //Check if Barcode has entry for given recipe and batach
                        currentQry = "SELECT COUNT(Id) FROM span_db_codebeat_codereader.barcode_production_defects WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
                        if (this.ProductionDataCB.SelectGetCount(currentQry) > 0)
                        {
                            //this.AddDefectSummeryOCR();
                        }

                        //
                        //Check if Pharmacode has entry for given recipe and batach
                        currentQry = "SELECT COUNT(Id) FROM span_db_codebeat_codereader.pharmacode_production_defects WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
                        if (this.ProductionDataCB.SelectGetCount(currentQry) > 0)
                        {
                            //this.AddDefectSummeryOCR();
                        }

                        this.AddPageBreak();
                    }
                }
            }
            int k = this.productIds.Count;

            int d = 9;


        }

        private void AddPageBreak()
        {
            this.MainDetail.HeightF += 700F;

            XRPageBreak MyPageBreak = new XRPageBreak();
            this.MainDetail.Controls.Add(MyPageBreak);
            MyPageBreak.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X, this.myPoint.Y);
            //
            //
            //Update current point of canvas
            this.myPoint.Y = this.myPoint.Y + 10F;
            this.MainDetail.HeightF = this.myPoint.Y;
        }

        private void AddInformation(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            Dictionary<string, string> data = new Dictionary<string, string>();
            string getQry = " SELECT MIN(InspectionStartTime), MAX(InspectionStopTime), InspectedBy FROM span_db.production WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
            this.ProductionDataCB.Select(getQry, ref data);

            XRPanel BatchInformationPanel = new XRPanel();
            this.MainDetail.Controls.Add(BatchInformationPanel);
            BatchInformationPanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            BatchInformationPanel.WidthF = this.currentWidth;
            //
            //Lable
            //
            XRLabel BatchHeader = new XRLabel();
            BatchInformationPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH INFORMATION";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            BatchInformationPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 30F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 180F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell cell0 = new XRTableCell();
            XRTableCell cell1 = new XRTableCell();
            cell0.Text = "Batch Name";
            cell0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell0.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell1.Text = batch;
            cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            cell2.Text = "Machine ID";
            cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell2.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell3.Text = "----";
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            /*-----Adding 3rd row-----*/
            XRTableRow row2 = new XRTableRow();

            XRTableCell cell4 = new XRTableCell();
            XRTableCell cell5 = new XRTableCell();
            cell4.Text = "Camera";
            cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell4.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell5.Text = "----";
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row2.Cells.AddRange(new XRTableCell[] { cell4, cell5 });
            BatchInformationTable.Rows.Add(row2);
            //
            //
            /*-----Adding 4th row-----*/
            XRTableRow row3 = new XRTableRow();

            XRTableCell cell6 = new XRTableCell();
            XRTableCell cell7 = new XRTableCell();
            cell6.Text = "Recipe Name";
            cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell6.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell7.Text = recipe;
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row3.Cells.AddRange(new XRTableCell[] { cell6, cell7 });
            BatchInformationTable.Rows.Add(row3);
            //
            //
            /*-----Adding 5th row-----*/
            XRTableRow row4 = new XRTableRow();

            XRTableCell cell8 = new XRTableCell();
            XRTableCell cell9 = new XRTableCell();
            cell8.Text = "Batch Start Date & Time";
            cell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell8.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell9.Text = data["MIN(InspectionStartTime)"];
            cell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row4.Cells.AddRange(new XRTableCell[] { cell8, cell9 });
            BatchInformationTable.Rows.Add(row4);
            //
            //
            /*-----Adding 6th row-----*/
            XRTableRow row5 = new XRTableRow();

            XRTableCell cell10 = new XRTableCell();
            XRTableCell cell11 = new XRTableCell();
            cell10.Text = "Batch Stop Date & Time";
            cell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell10.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell11.Text = data["MAX(InspectionStopTime)"];
            cell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row5.Cells.AddRange(new XRTableCell[] { cell10, cell11 });
            BatchInformationTable.Rows.Add(row5);
            //
            //
            /*-----Adding 7th row-----*/
            XRTableRow row6 = new XRTableRow();

            XRTableCell cell12 = new XRTableCell();
            XRTableCell cell13 = new XRTableCell();
            cell12.Text = "Batch Started By";
            cell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell12.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell13.Text = data["InspectedBy"];
            cell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row6.Cells.AddRange(new XRTableCell[] { cell12, cell13 });
            BatchInformationTable.Rows.Add(row6);
            //
            //
            /*-----Adding 8th row-----*/
            XRTableRow row7 = new XRTableRow();

            XRTableCell cell14 = new XRTableCell();
            XRTableCell cell15 = new XRTableCell();
            cell14.Text = "Report Time";
            cell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell14.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //cell15.Text = "----";
            cell15.ExpressionBindings.Add(new ExpressionBinding("Text", "Now()"));
            cell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row7.Cells.AddRange(new XRTableCell[] { cell14, cell15 });
            BatchInformationTable.Rows.Add(row7);
            //
            //
            /*-----Adding 9th row-----*/
            XRTableRow row8 = new XRTableRow();

            XRTableCell cell16 = new XRTableCell();
            XRTableCell cell17 = new XRTableCell();
            cell16.Text = "Printed By";
            cell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell16.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell17.Text = "----";
            cell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row8.Cells.AddRange(new XRTableCell[] { cell16, cell17 });
            BatchInformationTable.Rows.Add(row8);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //
            BatchInformationPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + BatchInformationPanel.HeightF;
            this.MainDetail.HeightF = this.myPoint.Y;
        }

        private void AddProductionSummery(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            Dictionary<string, string> data = new Dictionary<string, string>();
            string getQry = "SELECT SUM(Accept), SUM(Reject), SUM(Total) FROM span_db.production WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
            this.ProductionDataCB.Select(getQry, ref data);

            XRPanel BatchProductionSummeryPanel = new XRPanel();
            this.MainDetail.Controls.Add(BatchProductionSummeryPanel);
            BatchProductionSummeryPanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            BatchProductionSummeryPanel.WidthF = this.currentWidth;
            //
            //Label
            //
            XRLabel BatchHeader = new XRLabel();
            BatchProductionSummeryPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH PRODUCTION SUMMARY";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            BatchProductionSummeryPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 30F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 80F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell cell0 = new XRTableCell();
            XRTableCell cell1 = new XRTableCell();
            cell0.Text = "Good Blisters(A)";
            cell0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell0.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell1.Text = data["SUM(Accept)"];
            cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            cell2.Text = "Rejected Blisters(B)";
            cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell2.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell3.Text = data["SUM(Reject)"];
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            /*-----Adding 3rd row-----*/
            XRTableRow row2 = new XRTableRow();

            XRTableCell cell4 = new XRTableCell();
            XRTableCell cell5 = new XRTableCell();
            cell4.Text = "Total Blisters(A+B)";
            cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell4.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell5.Text = data["SUM(Total)"];
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row2.Cells.AddRange(new XRTableCell[] { cell4, cell5 });
            BatchInformationTable.Rows.Add(row2);
            //
            //
            /*-----Adding 4th row-----*/
            XRTableRow row3 = new XRTableRow();

            XRTableCell cell6 = new XRTableCell();
            XRTableCell cell7 = new XRTableCell();
            cell6.Text = "Rejection[(B)/(A+B)*100](%)]";
            cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell6.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            cell7.Text = "NULL";
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row3.Cells.AddRange(new XRTableCell[] { cell6, cell7 });
            BatchInformationTable.Rows.Add(row3);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //Update current point of canvas
            BatchProductionSummeryPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + BatchProductionSummeryPanel.HeightF;
            this.MainDetail.HeightF = this.myPoint.Y;
        }

        private void AddDefectSummeryDatamatrix(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            Dictionary<string, string> data = new Dictionary<string, string>();
            string getQry = "SELECT SUM(CodeNotFound), SUM(ValueMismatch), SUM(LowPrintQuality) FROM span_db_codebeat_codereader.datamatrix_production_defects WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
            this.ProductionDataCB.Select(getQry, ref data);

            XRPanel DefectSummeryDatamatrixPanel = new XRPanel();
            this.MainDetail.Controls.Add(DefectSummeryDatamatrixPanel);
            DefectSummeryDatamatrixPanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            DefectSummeryDatamatrixPanel.WidthF = this.currentWidth;
            //
            //Lable
            //
            XRLabel BatchHeader = new XRLabel();
            DefectSummeryDatamatrixPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH PRODUCTION DEFECTS SUMMARY - DATAMATRIX";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            DefectSummeryDatamatrixPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 30F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 60F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell r0C0 = new XRTableCell();
            XRTableCell r0C1 = new XRTableCell();
            XRTableCell r0C2 = new XRTableCell();
            r0C0.WidthF = 10F;
            r0C0.BackColor = System.Drawing.Color.DarkOliveGreen;
            r0C1.Text = "CodeNotFound(%)";
            r0C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r0C1.WidthF = r0C1.WidthF - 10F;
            r0C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r0C2.Text = data["SUM(CodeNotFound)"];
            r0C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { r0C0, r0C1, r0C2 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell r1C0 = new XRTableCell();
            XRTableCell r1C1 = new XRTableCell();
            XRTableCell r1C2 = new XRTableCell();
            r1C0.WidthF = 10F;
            r1C0.BackColor = System.Drawing.Color.Aqua;
            r1C1.Text = "ValueMismatch(%)";
            r1C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r1C1.WidthF = r1C1.WidthF - 10F;
            r1C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r1C2.Text = data["SUM(ValueMismatch)"];
            r1C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { r1C0, r1C1, r1C2 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            /*-----Adding 3rd row-----*/
            XRTableRow row2 = new XRTableRow();

            XRTableCell r2C0 = new XRTableCell();
            XRTableCell r2C1 = new XRTableCell();
            XRTableCell r2C2 = new XRTableCell();
            r2C0.WidthF = 10F;
            r2C0.BackColor = System.Drawing.Color.MistyRose;
            r2C1.Text = "LowPrintQuality(%)";
            r2C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r2C1.WidthF = r2C1.WidthF - 10F;
            r2C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r2C2.Text = data["SUM(LowPrintQuality)"];
            r2C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row2.Cells.AddRange(new XRTableCell[] { r2C0, r2C1, r2C2 });
            BatchInformationTable.Rows.Add(row2);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //
            //Updating current point in canvas
            DefectSummeryDatamatrixPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + DefectSummeryDatamatrixPanel.HeightF;
            this.MainDetail.HeightF = this.myPoint.Y;
        }

        private void AddDefectSummeryOCR(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            Dictionary<string, string> data = new Dictionary<string, string>();
            string getQry = "SELECT SUM(CodeNotFound), SUM(LineCountMismatch), SUM(CharCountMismatch), SUM(LowScore) FROM span_db_codebeat_codereader.ocr_production_defects WHERE ModelName = '" + recipe + "' AND BatchNo = '" + batch + "';";
            this.ProductionDataCB.Select(getQry, ref data);

            XRPanel DefectSummeryOCRPanel = new XRPanel();
            this.MainDetail.Controls.Add(DefectSummeryOCRPanel);
            DefectSummeryOCRPanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            DefectSummeryOCRPanel.WidthF = this.currentWidth;
            //
            //Lable
            //
            XRLabel BatchHeader = new XRLabel();
            DefectSummeryOCRPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH PRODUCTION DEFECTS SUMMARY - OCR";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            DefectSummeryOCRPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 30F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 80F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell r0C0 = new XRTableCell();
            XRTableCell r0C1 = new XRTableCell();
            XRTableCell r0C2 = new XRTableCell();
            r0C0.WidthF = 10F;
            r0C0.BackColor = System.Drawing.Color.DarkOliveGreen;
            r0C1.Text = "CodeNotFound(%)";
            r0C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r0C1.WidthF = r0C1.WidthF - 10F;
            r0C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r0C2.Text = data["SUM(CodeNotFound)"];
            r0C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { r0C0, r0C1, r0C2 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell r1C0 = new XRTableCell();
            XRTableCell r1C1 = new XRTableCell();
            XRTableCell r1C2 = new XRTableCell();
            r1C0.WidthF = 10F;
            r1C0.BackColor = System.Drawing.Color.Aqua;
            r1C1.Text = "LineCountMismatch(%)";
            r1C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r1C1.WidthF = r1C1.WidthF - 10F;
            r1C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r1C2.Text = data["SUM(LineCountMismatch)"];
            r1C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { r1C0, r1C1, r1C2 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            /*-----Adding 3rd row-----*/
            XRTableRow row2 = new XRTableRow();

            XRTableCell r2C0 = new XRTableCell();
            XRTableCell r2C1 = new XRTableCell();
            XRTableCell r2C2 = new XRTableCell();
            r2C0.WidthF = 10F;
            r2C0.BackColor = System.Drawing.Color.MistyRose;
            r2C1.Text = "CharCountMismatch(%)";
            r2C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r2C1.WidthF = r2C1.WidthF - 10F;
            r2C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r2C2.Text = data["SUM(CharCountMismatch)"];
            r2C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row2.Cells.AddRange(new XRTableCell[] { r2C0, r2C1, r2C2 });
            BatchInformationTable.Rows.Add(row2);
            //
            //
            /*-----Adding 4rd row-----*/
            XRTableRow row3 = new XRTableRow();

            XRTableCell r3C0 = new XRTableCell();
            XRTableCell r3C1 = new XRTableCell();
            XRTableCell r3C2 = new XRTableCell();
            r3C0.WidthF = 10F;
            r3C0.BackColor = System.Drawing.Color.MistyRose;
            r3C1.Text = "LowScore(%)";
            r3C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r3C1.WidthF = r3C1.WidthF - 10F;
            r3C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r3C2.Text = data["SUM(LowScore)"];
            r3C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row3.Cells.AddRange(new XRTableCell[] { r3C0, r3C1, r3C2 });
            BatchInformationTable.Rows.Add(row3);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //
            //Updating current point in canvas
            DefectSummeryOCRPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + DefectSummeryOCRPanel.HeightF;
            this.MainDetail.HeightF = this.myPoint.Y;
        }

        private void AddDefectSummeryBarcode(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            XRPanel DefectSummeryBarcodePanel = new XRPanel();
            this.MainDetail.Controls.Add(DefectSummeryBarcodePanel);
            DefectSummeryBarcodePanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            DefectSummeryBarcodePanel.WidthF = this.currentWidth;
            //
            //Lable
            //
            XRLabel BatchHeader = new XRLabel();
            DefectSummeryBarcodePanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            BatchHeader.Text = "BATCH PRODUCTION DEFECTS SUMMARY - BARCODE";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            DefectSummeryBarcodePanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, BatchHeader.HeightF);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 60F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 96F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell r0C0 = new XRTableCell();
            XRTableCell r0C1 = new XRTableCell();
            XRTableCell r0C2 = new XRTableCell();
            r0C0.WidthF = 10F;
            r0C0.BackColor = System.Drawing.Color.DarkOliveGreen;
            r0C1.Text = "CodeNotFound(%)";
            r0C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r0C1.WidthF = r0C1.WidthF - 10F;
            r0C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r0C2.Text = "----";
            r0C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { r0C0, r0C1, r0C2 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell r1C0 = new XRTableCell();
            XRTableCell r1C1 = new XRTableCell();
            XRTableCell r1C2 = new XRTableCell();
            r1C0.WidthF = 10F;
            r1C0.BackColor = System.Drawing.Color.Aqua;
            r1C1.Text = "ValueMismatch(%)";
            r1C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r1C1.WidthF = r1C1.WidthF - 10F;
            r1C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r1C2.Text = "----";
            r1C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { r1C0, r1C1, r1C2 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            /*-----Adding 3rd row-----*/
            XRTableRow row2 = new XRTableRow();

            XRTableCell r2C0 = new XRTableCell();
            XRTableCell r2C1 = new XRTableCell();
            XRTableCell r2C2 = new XRTableCell();
            r2C0.WidthF = 10F;
            r2C0.BackColor = System.Drawing.Color.MistyRose;
            r2C1.Text = "LowPrintQuality(%)";
            r2C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r2C1.WidthF = r2C1.WidthF - 10F;
            r2C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r2C2.Text = "----";
            r2C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row2.Cells.AddRange(new XRTableCell[] { r2C0, r2C1, r2C2 });
            BatchInformationTable.Rows.Add(row2);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //Updating current point in canvas
            DefectSummeryBarcodePanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + DefectSummeryBarcodePanel.HeightF + 10F;
            this.MainDetail.HeightF = this.myPoint.Y;
        }

        private void AddDefectSummeryArtWork(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            XRPanel DefectSummeryArtworkPanel = new XRPanel();
            this.MainDetail.Controls.Add(DefectSummeryArtworkPanel);
            DefectSummeryArtworkPanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            DefectSummeryArtworkPanel.WidthF = this.currentWidth;
            //
            //Label
            //
            XRLabel BatchHeader = new XRLabel();
            DefectSummeryArtworkPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            BatchHeader.Text = "BATCH DEFECT SUMMARY - ARTWORK";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            DefectSummeryArtworkPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, BatchHeader.HeightF);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 20F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 96F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell r0C0 = new XRTableCell();
            XRTableCell r0C1 = new XRTableCell();
            XRTableCell r0C2 = new XRTableCell();
            r0C0.WidthF = 10F;
            r0C0.BackColor = System.Drawing.Color.DarkOliveGreen;
            r0C1.Text = "Not Matched";
            r0C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r0C1.WidthF = r0C1.WidthF - 10F;
            r0C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r0C2.Text = "----";
            r0C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { r0C0, r0C1, r0C2 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //Updating current point in canvas
            DefectSummeryArtworkPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + DefectSummeryArtworkPanel.HeightF + 10F;
            this.MainDetail.HeightF = this.myPoint.Y;
        }

        private void AddDefectSummeryPharmacode(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            XRPanel DefectSummeryPharmacodePanel = new XRPanel();
            this.MainDetail.Controls.Add(DefectSummeryPharmacodePanel);
            DefectSummeryPharmacodePanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            DefectSummeryPharmacodePanel.WidthF = this.currentWidth;
            //
            //Lable
            //
            XRLabel BatchHeader = new XRLabel();
            DefectSummeryPharmacodePanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            BatchHeader.Text = "BATCH PRODUCTION DEFECTS SUMMARY - PHARMACODE";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            DefectSummeryPharmacodePanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, BatchHeader.HeightF);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 100F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 96F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell r0C0 = new XRTableCell();
            XRTableCell r0C1 = new XRTableCell();
            XRTableCell r0C2 = new XRTableCell();
            r0C0.WidthF = 10F;
            r0C0.BackColor = System.Drawing.Color.DarkOliveGreen;
            r0C1.Text = "CodeNotFound";
            r0C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r0C1.WidthF = r0C1.WidthF - 10F;
            r0C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r0C2.Text = "----";
            r0C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { r0C0, r0C1, r0C2 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell r1C0 = new XRTableCell();
            XRTableCell r1C1 = new XRTableCell();
            XRTableCell r1C2 = new XRTableCell();
            r1C0.WidthF = 10F;
            r1C0.BackColor = System.Drawing.Color.Aqua;
            r1C1.Text = "ValueMismatch";
            r1C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r1C1.WidthF = r1C1.WidthF - 10F;
            r1C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r1C2.Text = "----";
            r1C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { r1C0, r1C1, r1C2 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            /*-----Adding 3rd row-----*/
            XRTableRow row2 = new XRTableRow();

            XRTableCell r2C0 = new XRTableCell();
            XRTableCell r2C1 = new XRTableCell();
            XRTableCell r2C2 = new XRTableCell();
            r2C0.WidthF = 10F;
            r2C0.BackColor = System.Drawing.Color.MistyRose;
            r2C1.Text = "HeightMismatch";
            r2C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r2C1.WidthF = r2C1.WidthF - 10F;
            r2C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r2C2.Text = "----";
            r2C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row2.Cells.AddRange(new XRTableCell[] { r2C0, r2C1, r2C2 });
            BatchInformationTable.Rows.Add(row2);
            //
            //
            /*-----Adding 4rd row-----*/
            XRTableRow row3 = new XRTableRow();

            XRTableCell r3C0 = new XRTableCell();
            XRTableCell r3C1 = new XRTableCell();
            XRTableCell r3C2 = new XRTableCell();
            r3C0.WidthF = 10F;
            r3C0.BackColor = System.Drawing.Color.MistyRose;
            r3C1.Text = "ColorMismatch";
            r3C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r3C1.WidthF = r3C1.WidthF - 10F;
            r3C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r3C2.Text = "----";
            r3C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row3.Cells.AddRange(new XRTableCell[] { r3C0, r3C1, r3C2 });
            BatchInformationTable.Rows.Add(row3);
            //
            //
            /*-----Adding 5rd row-----*/
            XRTableRow row4 = new XRTableRow();

            XRTableCell r4C0 = new XRTableCell();
            XRTableCell r4C1 = new XRTableCell();
            XRTableCell r4C2 = new XRTableCell();
            r4C0.WidthF = 10F;
            r4C0.BackColor = System.Drawing.Color.MistyRose;
            r4C1.Text = "StandardsMismatch";
            r4C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r4C1.WidthF = r4C1.WidthF - 10F;
            r4C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r4C2.Text = "----";
            r4C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row4.Cells.AddRange(new XRTableCell[] { r4C0, r4C1, r4C2 });
            BatchInformationTable.Rows.Add(row4);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //
            //Updating current point in canvas
            DefectSummeryPharmacodePanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + DefectSummeryPharmacodePanel.HeightF + 10F;
            this.MainDetail.HeightF = this.myPoint.Y;

        }

        private void AddDefectSummeryOCV(string recipe, string batch)
        {
            this.MainDetail.HeightF += 700F;

            XRPanel DefectSummeryOCVPanel = new XRPanel();
            this.MainDetail.Controls.Add(DefectSummeryOCVPanel);
            DefectSummeryOCVPanel.LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X,
                                                                this.myPoint.Y);
            DefectSummeryOCVPanel.WidthF = this.currentWidth;
            //
            //Lable
            //
            XRLabel BatchHeader = new XRLabel();
            DefectSummeryOCVPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            BatchHeader.Text = "BATCH PRODUCTION DEFECTS SUMMARY - OCV";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(180, 218, 237);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 30F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            DefectSummeryOCVPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, BatchHeader.HeightF);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 80F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 96F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //Start Initialization of table
            BatchInformationTable.BeginInit();
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row0 = new XRTableRow();

            XRTableCell r0C0 = new XRTableCell();
            XRTableCell r0C1 = new XRTableCell();
            XRTableCell r0C2 = new XRTableCell();
            r0C0.WidthF = 10F;
            r0C0.BackColor = System.Drawing.Color.DarkOliveGreen;
            r0C1.Text = "CodeNotFound";
            r0C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r0C1.WidthF = r0C1.WidthF - 10F;
            r0C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r0C2.Text = "----";
            r0C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { r0C0, r0C1, r0C2 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell r1C0 = new XRTableCell();
            XRTableCell r1C1 = new XRTableCell();
            XRTableCell r1C2 = new XRTableCell();
            r1C0.WidthF = 10F;
            r1C0.BackColor = System.Drawing.Color.Aqua;
            r1C1.Text = "LineCountMismatch";
            r1C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r1C1.WidthF = r1C1.WidthF - 10F;
            r1C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r1C2.Text = "----";
            r1C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { r1C0, r1C1, r1C2 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            /*-----Adding 3rd row-----*/
            XRTableRow row2 = new XRTableRow();

            XRTableCell r2C0 = new XRTableCell();
            XRTableCell r2C1 = new XRTableCell();
            XRTableCell r2C2 = new XRTableCell();
            r2C0.WidthF = 10F;
            r2C0.BackColor = System.Drawing.Color.MistyRose;
            r2C1.Text = "CharCountMismatch";
            r2C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r2C1.WidthF = r2C1.WidthF - 10F;
            r2C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r2C2.Text = "----";
            r2C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row2.Cells.AddRange(new XRTableCell[] { r2C0, r2C1, r2C2 });
            BatchInformationTable.Rows.Add(row2);
            //
            //
            /*-----Adding 4rd row-----*/
            XRTableRow row3 = new XRTableRow();

            XRTableCell r3C0 = new XRTableCell();
            XRTableCell r3C1 = new XRTableCell();
            XRTableCell r3C2 = new XRTableCell();
            r3C0.WidthF = 10F;
            r3C0.BackColor = System.Drawing.Color.MistyRose;
            r3C1.Text = "LowScore";
            r3C1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            r3C1.WidthF = r3C1.WidthF - 10F;
            r3C1.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            r3C2.Text = "----";
            r3C2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row3.Cells.AddRange(new XRTableCell[] { r3C0, r3C1, r3C2 });
            BatchInformationTable.Rows.Add(row3);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //
            //Updating current point in canvas
            DefectSummeryOCVPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.myPoint.Y = this.myPoint.Y + DefectSummeryOCVPanel.HeightF + 10F;
            this.MainDetail.HeightF = this.myPoint.Y;

        }

    }
}