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
            /*-----Adding a tabel-----*/
            XRTable BatchInformationTable = new XRTable();
            BatchInformationPanel.Controls.Add(BatchInformationTable);
            BatchInformationTable.LocationF = new DevExpress.Utils.PointFloat(0F, 40F + 10F);
            BatchInformationTable.SizeF = new SizeF(this.currentWidth, 180F);
            BatchInformationTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            BatchInformationTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            BatchInformationTable.Font = new DevExpress.Drawing.DXFont("Arial Unicode MS", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            //
            //
            //Start Initalization of tabel
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
            cell14.Text = "Reprot Time";
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

    }
}
