using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace LibReports.Production
{
    public partial class Production : DevExpress.XtraReports.UI.XtraReport
    {
        private PointF unboundPoint;
        private float currentWidth;

        public Production()
        {
            InitializeComponent();

            this.unboundPoint = new PointF(0, 0);
            //this.ReportUnit = ReportUnit.HundredthsOfAnInch;
            //this.Margins.Left = 50F;
            //this.Margins.Right = 50F;
            this.currentWidth = this.RootReport.PageWidth - this.Margins.Left - this.Margins.Right;

            AddInformation();
            AddProductionSummery();
            AddDefectSummeryCavityWise();
            AddDefectSummeryBlisterWise();
        }

        private void AddInformation()
        {
            this.UnboundDetail.HeightF += 700F;

            XRPanel BatchInformationPanel = new XRPanel();
            this.UnboundDetail.Controls.Add(BatchInformationPanel);
            BatchInformationPanel.LocationF = new DevExpress.Utils.PointFloat(this.unboundPoint.X,
                                                                this.unboundPoint.Y);
            BatchInformationPanel.WidthF = this.currentWidth;
            //
            //Lable
            //
            XRLabel BatchHeader = new XRLabel();
            BatchInformationPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH INFORMATION";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(43, 129, 181);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 40F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            BatchInformationPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 40F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 180F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 11F, DevExpress.Drawing.DXFontStyle.Bold);
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
            cell0.Text = "Batch No";
            cell0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell1.Text = "----";
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
            cell7.Text = "----";
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
            cell9.Text = "----";
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
            cell11.Text = "----";
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
            cell13.Text = "----";
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
            cell15.Text = "----";
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
            this.unboundPoint.Y = this.unboundPoint.Y + BatchInformationPanel.HeightF;
            this.UnboundDetail.HeightF = this.unboundPoint.Y;
        }

        private void AddProductionSummery()
        {
            this.UnboundDetail.HeightF += 700F;

            XRPanel BatchProductionSummeryPanel = new XRPanel();
            this.UnboundDetail.Controls.Add(BatchProductionSummeryPanel);
            BatchProductionSummeryPanel.LocationF = new DevExpress.Utils.PointFloat(this.unboundPoint.X,
                                                                this.unboundPoint.Y);
            BatchProductionSummeryPanel.WidthF = this.currentWidth;
            //
            //Label
            //
            XRLabel BatchHeader = new XRLabel();
            BatchProductionSummeryPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH PRODUCTION SUMMARY";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(43, 129, 181);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 40F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            BatchProductionSummeryPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 40F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 120F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 11F);
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
            cell1.Text = "----";
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
            cell4.Text = "Total Blisters(A+B)";
            cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            cell6.Text = "Rejection[(B)/(A+B)*100](%)]";
            cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell7.Text = "----";
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
            this.unboundPoint.Y = this.unboundPoint.Y + BatchProductionSummeryPanel.HeightF;
            this.UnboundDetail.HeightF = this.unboundPoint.Y;

        }

        private void AddDefectSummeryCavityWise()
        {
            this.UnboundDetail.HeightF += 700F;

            XRPanel BatchDefectSummeryCWPanel = new XRPanel();
            this.UnboundDetail.Controls.Add(BatchDefectSummeryCWPanel);
            BatchDefectSummeryCWPanel.LocationF = new DevExpress.Utils.PointFloat(this.unboundPoint.X,
                                                                this.unboundPoint.Y);
            BatchDefectSummeryCWPanel.WidthF = this.currentWidth;
            //BatchDefectSummeryCWPanel.Borders = DevExpress.XtraPrinting.BorderSide.All;
            //BatchDefectSummeryCWPanel.BorderWidth = 2;
            //
            //
            //Label
            XRLabel BatchHeader = new XRLabel();
            BatchDefectSummeryCWPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH PRODUCTION DEFECT SUMMARY (CAVITY WISE)";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(43, 129, 181);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 40F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            BatchDefectSummeryCWPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 40F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 280F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Times New Roman", 11F);
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
            cell0.Text = "Area";
            cell0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell1.Text = "----";
            cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 2nd row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            cell2.Text = "Width";
            cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            cell4.Text = "Length";
            cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            cell6.Text = "Shape";
            cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell7.Text = "----";
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row3.Cells.AddRange(new XRTableCell[] { cell6, cell7 });
            BatchInformationTable.Rows.Add(row3);
            //
            //
            /*-----Adding 5th row-----*/
            XRTableRow row4 = new XRTableRow();

            XRTableCell cell8 = new XRTableCell();
            XRTableCell cell9 = new XRTableCell();
            cell8.Text = "Convexity";
            cell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell9.Text = "----";
            cell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row4.Cells.AddRange(new XRTableCell[] { cell8, cell9 });
            BatchInformationTable.Rows.Add(row4);
            //
            //
            /*-----Adding 6th row-----*/
            XRTableRow row5 = new XRTableRow();

            XRTableCell cell10 = new XRTableCell();
            XRTableCell cell11 = new XRTableCell();
            cell10.Text = "DarkSpot";
            cell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell11.Text = "----";
            cell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row5.Cells.AddRange(new XRTableCell[] { cell10, cell11 });
            BatchInformationTable.Rows.Add(row5);
            //
            //
            /*-----Adding 7th row-----*/
            XRTableRow row6 = new XRTableRow();

            XRTableCell cell12 = new XRTableCell();
            XRTableCell cell13 = new XRTableCell();
            cell12.Text = "BrightSpot";
            cell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell13.Text = "----";
            cell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row6.Cells.AddRange(new XRTableCell[] { cell12, cell13 });
            BatchInformationTable.Rows.Add(row6);
            //
            //
            /*-----Adding 8th row-----*/
            XRTableRow row7 = new XRTableRow();

            XRTableCell cell14 = new XRTableCell();
            XRTableCell cell15 = new XRTableCell();
            cell14.Text = "EdgeSpot";
            cell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell15.Text = "----";
            cell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row7.Cells.AddRange(new XRTableCell[] { cell14, cell15 });
            BatchInformationTable.Rows.Add(row7);
            //
            //
            /*-----Adding 9th row-----*/
            XRTableRow row8 = new XRTableRow();

            XRTableCell cell16 = new XRTableCell();
            XRTableCell cell17 = new XRTableCell();
            cell16.Text = "ProductColor";
            cell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell17.Text = "----";
            cell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row8.Cells.AddRange(new XRTableCell[] { cell16, cell17 });
            BatchInformationTable.Rows.Add(row8);
            //
            //
            /*-----Adding 10th row-----*/
            XRTableRow row9 = new XRTableRow();

            XRTableCell cell18 = new XRTableCell();
            XRTableCell cell19 = new XRTableCell();
            cell18.Text = "ColorDistribution";
            cell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell19.Text = "----";
            cell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row9.Cells.AddRange(new XRTableCell[] { cell18, cell19 });
            BatchInformationTable.Rows.Add(row9);
            //
            //
            /*-----Adding 11th row-----*/
            XRTableRow row10 = new XRTableRow();

            XRTableCell cell20 = new XRTableCell();
            XRTableCell cell21 = new XRTableCell();
            cell20.Text = "Orientation";
            cell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell21.Text = "----";
            cell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row10.Cells.AddRange(new XRTableCell[] { cell20, cell21 });
            BatchInformationTable.Rows.Add(row10);
            //
            //
            /*-----Adding 12th row-----*/
            XRTableRow row11 = new XRTableRow();

            XRTableCell cell22 = new XRTableCell();
            XRTableCell cell23 = new XRTableCell();
            cell22.Text = "ForeignObjectInCavity";
            cell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell23.Text = "----";
            cell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row11.Cells.AddRange(new XRTableCell[] { cell22, cell23 });
            BatchInformationTable.Rows.Add(row11);
            //
            //
            /*-----Adding 13th row-----*/
            XRTableRow row12 = new XRTableRow();

            XRTableCell cell24 = new XRTableCell();
            XRTableCell cell25 = new XRTableCell();
            cell24.Text = "EmptyCavity";
            cell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell25.Text = "----";
            cell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row12.Cells.AddRange(new XRTableCell[] { cell24, cell25 });
            BatchInformationTable.Rows.Add(row12);
            //
            //
            /*-----Adding 14th row-----*/
            XRTableRow row13 = new XRTableRow();

            XRTableCell cell26 = new XRTableCell();
            XRTableCell cell27 = new XRTableCell();
            cell26.Text = "PatternMatch";
            cell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell27.Text = "----";
            cell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row13.Cells.AddRange(new XRTableCell[] { cell26, cell27 });
            BatchInformationTable.Rows.Add(row13);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //
            //Updating current point in canvas
            BatchDefectSummeryCWPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.unboundPoint.Y = this.unboundPoint.Y + BatchDefectSummeryCWPanel.HeightF;
            this.UnboundDetail.HeightF = this.unboundPoint.Y;
        }

        private void AddDefectSummeryBlisterWise()
        {
            this.UnboundDetail.HeightF += 700F;

            XRPanel BatchDefectSummeryBWPanel = new XRPanel();
            this.UnboundDetail.Controls.Add(BatchDefectSummeryBWPanel);
            BatchDefectSummeryBWPanel.LocationF = new DevExpress.Utils.PointFloat(this.unboundPoint.X,
                                                                this.unboundPoint.Y);
            BatchDefectSummeryBWPanel.WidthF = this.currentWidth;
            //
            //
            //Label
            XRLabel BatchHeader = new XRLabel();
            BatchDefectSummeryBWPanel.Controls.Add(BatchHeader);
            BatchHeader.LocationF = new DevExpress.Utils.PointFloat(0F, 10F);
            BatchHeader.Text = "BATCH PRODUCTION DEFECT SUMMARY (BLISTER WISE)";
            BatchHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            BatchHeader.BackColor = System.Drawing.Color.FromArgb(109, 179, 220);
            BatchHeader.SizeF = new SizeF(this.currentWidth, 40F);
            BatchHeader.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 14F);
            //
            //
            /*-----Adding a table-----*/
            XRTable BatchInformationTable = new XRTable();
            BatchDefectSummeryBWPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 40F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 40F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 11F, DevExpress.Drawing.DXFontStyle.Bold);
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
            //colCell.WidthF = 10F;
            //colCell.BackColor = Color.Aqua;
            //colCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5);
            //cell0.WidthF =  (float)(this.currentWidth/2F) - colCell.WidthF;
            cell0.Text = "Empty Blister";
            cell0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell1.Text = "----";
            cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            BatchInformationTable.Rows.Add(row0);
            //
            //
            /*-----Adding 1st row-----*/
            XRTableRow row1 = new XRTableRow();

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            cell2.Text = "ForeignObjectOnBaseFoil";
            cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            cell3.Text = "----";
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3 });
            BatchInformationTable.Rows.Add(row1);
            //
            //
            //
            BatchInformationTable.EndInit();
            //
            //
            //Updating current point in canvas
            BatchDefectSummeryBWPanel.SizeF = new SizeF(this.currentWidth, BatchHeader.HeightF + BatchInformationTable.HeightF + 10F);
            this.unboundPoint.Y = this.unboundPoint.Y + BatchDefectSummeryBWPanel.HeightF;
            this.UnboundDetail.HeightF = this.unboundPoint.Y;

        }
    }
}
