using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WindowsFormsApplication1
{
    public partial class Contin : DevExpress.XtraReports.UI.XtraReport
    {
        public Contin()
        {
            InitializeComponent();

            XRTable myTabel = new XRTable();
            this.MyGroupHeader.Controls.Add(myTabel);
            myTabel.LocationF = new PointF(0, 0);
            myTabel.SizeF = new SizeF(this.RootReport.PageWidth - this.Margins.Right - this.Margins.Left, 20F);
            myTabel.Borders = DevExpress.XtraPrinting.BorderSide.All;
            

            myTabel.BeginInit();

            XRTableRow row0 = new XRTableRow();
            myTabel.Controls.Add(row0);

            XRTableCell cell0 = new XRTableCell() {
                        WidthF = 20F};
            XRPictureBox pBox = new XRPictureBox()
            {
                BackColor = System.Drawing.Color.LightSkyBlue,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderColor = System.Drawing.Color.White,
                BorderWidth = 5F
            };
            cell0.Controls.Add(pBox);

            XRTableCell cell1 = new XRTableCell() {
                        Text = "PlaceHolder",
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter};
            XRTableCell cell2 = new XRTableCell()
            {
                Text = "PlaceHolder",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            };

            row0.Controls.Add(cell0);
            row0.Controls.Add(cell1);
            row0.Controls.Add(cell2);

            myTabel.EndInit();




        }

    }
}
