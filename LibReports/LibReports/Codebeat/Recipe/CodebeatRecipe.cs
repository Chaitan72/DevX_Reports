using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.Office.Utils;
using DevExpress.XtraReports.UI;

namespace LibReports.Codebeat.Recipe
{
    public partial class CodebeatRecipe : DevExpress.XtraReports.UI.XtraReport
    {
        private PointF myPoint;
        private float currnetPageWidth;
        private XRControlStyleSheet adda;
        public CodebeatRecipe()
        {
            InitializeComponent();

            this.myPoint = new PointF(0, 0);
            this.currnetPageWidth = this.RootReport.PageWidth - this.Margins.Left - this.Margins.Right;

            AddPharamcodeBlock();
        }

        public float getHeightInHunOfInch(string path)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            return (float)Units.PixelsToHundredthsOfInch(img.Height, img.VerticalResolution);
        }


        /**
        * @author chaitan
        * usage -> to add pharamacode block to report 
        */
        private void AddPharamcodeBlock()
        {
            //DbManage pData = new DbManage();
            //pData.make_connection();

            this.MainDetail.HeightF += 700F;

            XRPanel PharmacodePanel = new XRPanel()
            {
                LocationF = new DevExpress.Utils.PointFloat(this.myPoint.X, this.myPoint.Y),
                WidthF = this.currnetPageWidth
            };
            this.MainDetail.Controls.Add(PharmacodePanel);
            //
            //
            //Adding Label of the block
            XRLabel blockName = new XRLabel()
            {
                Text = "Block 1 - PHARMACODE",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.SkyBlue,
                Font = new DevExpress.Drawing.DXFont("Ariel Unicode MS", 16F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1
            };
            PharmacodePanel.Controls.Add(blockName);
            blockName.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            blockName.SizeF = new SizeF(this.currnetPageWidth, 40F);
            //
            //
            //Adding a pictureBox
            XRPictureBox pic = new XRPictureBox();
            PharmacodePanel.Controls.Add(pic);
            pic.LocationF = new DevExpress.Utils.PointFloat(0, blockName.HeightF);
            string imgLoc = "C:\\Users\\SPAN_CHAITANYA\\Desktop\\Report_Samples\\images--chaitanya\\r45678_0_product_cavity.bmp";
            pic.ImageUrl = imgLoc;
            pic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pic.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            pic.SizeF = new SizeF(this.currnetPageWidth, 150F);
            pic.Borders = DevExpress.XtraPrinting.BorderSide.All;
            pic.BorderWidth = 1;
            //
            //
            //Adding Tale
            XRTable table = new XRTable();
            PharmacodePanel.Controls.Add(table);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, pic.HeightF + blockName.HeightF);
            table.SizeF = new SizeF(this.currnetPageWidth, 250F);
            //table.WidthF = this.currnetPageWidth;
            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.BorderWidth = 1;
            table.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 96F);
            table.Font = new DevExpress.Drawing.DXFont("Ariel Unicode MS", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            //
            //
            //
            table.BeginInit();
            //
            //
            //--- Adding 1st row ---
            XRTableRow row0 = new XRTableRow();
            row0.HeightF = 30F;
            row0.Font = new DevExpress.Drawing.DXFont("Ariel Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            row0.BackColor = System.Drawing.Color.SkyBlue;
            row0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell r0C0 = new XRTableCell();
            r0C0.Text = "TAUGHT VALUE";
            //r0C0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            row0.Cells.Add(r0C0);
            table.Rows.Add(row0);
            //
            //
            // --- Adding 2nd row ---
            XRTableRow row1 = new XRTableRow();
            //row1.HeightF = 20F;

            XRTableCell r1C0 = new XRTableCell();
            r1C0.Text = "998998";
            r1C0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            row1.Cells.Add(r1C0);
            table.Rows.Add(row1);
            //
            //
            // --- Adding 3rd row ---
            XRTableRow row2 = new XRTableRow();
            row2.HeightF = 30F;
            row2.Font = new DevExpress.Drawing.DXFont("Ariel Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            row2.BackColor = System.Drawing.Color.SkyBlue;

            XRTableCell r2C0 = new XRTableCell();
            r2C0.Text = "PROPERTY";
            r2C0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            row2.Cells.Add(r2C0);
            table.Rows.Add(row2);
            //
            //
            // --- Adding 4th row ---
            XRTableRow row3 = new XRTableRow();
            //row3.HeightF = 20F;

            XRTableCell r3C0 = new XRTableCell() {
                        Text = "Orientation",
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                         };
            XRTableCell r3C1 = new XRTableCell() {
                        Text = "Picket Fence",
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft};
            row3.Cells.AddRange(new XRTableCell[] { r3C0, r3C1 });
            table.Rows.Add(row3);
            //
            //
            // --- Adding 5th row ---
            XRTableRow row4 = new XRTableRow();
            //row4.HeightF = 20F;

            XRTableCell r4C0 = new XRTableCell()
            {
                Text = "Direction",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };
            XRTableCell r4C1 = new XRTableCell()
            {
                Text = "Left to Right",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };

            row4.Cells.AddRange(new XRTableCell[] { r4C0, r4C1 });
            table.Rows.Add(row4);
            //
            //
            // --- Adding 6th row ---
            XRTableRow row5 = new XRTableRow();
            //row5.HeightF = 20F;

            XRTableCell r5C0 = new XRTableCell()
            {
                Text = "Foreground",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };
            XRTableCell r5C1 = new XRTableCell()
            {
                Text = "Dark",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };

            row5.Cells.AddRange(new XRTableCell[] { r5C0, r5C1 });
            table.Rows.Add(row5);
            //
            //
            // --- Adding 7th row ---
            XRTableRow row6 = new XRTableRow();
            row6.HeightF = 30F;
            row6.Font = new DevExpress.Drawing.DXFont("Ariel Unicode MS", 14F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Pixel);
            row6.BackColor = System.Drawing.Color.SkyBlue;

            XRTableCell r6C0 = new XRTableCell();
            r6C0.Text = "VALUES";
            r6C0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            row6.Cells.Add(r6C0);
            table.Rows.Add(row6);
            //
            //
            // --- Adding 8th row ---
            XRTableRow row7 = new XRTableRow();
            //row7.HeightF = 20F;

            XRTableCell r7C0 = new XRTableCell()
            {
                Text = "Standards",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };
            XRTableCell r7C1 = new XRTableCell()
            {
                Text = "Off",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };

            row7.Cells.AddRange(new XRTableCell[] { r7C0, r7C1 });
            table.Rows.Add(row7);
            //
            //
            // --- Adding 9th row ---
            XRTableRow row8 = new XRTableRow();
            //row8.HeightF = 20F;

            XRTableCell r8C0 = new XRTableCell()
            {
                Text = "Extended Search(Px)",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };
            XRTableCell r8C1 = new XRTableCell()
            {
                Text = "70",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };

            row8.Cells.AddRange(new XRTableCell[] { r8C0, r8C1 });
            table.Rows.Add(row8);
            //
            //
            // --- Adding 10th row ---
            XRTableRow row9 = new XRTableRow();
            //row9.HeightF = 20F;

            XRTableCell r9C0 = new XRTableCell()
            {
                Text = "Length(%)",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };
            XRTableCell r9C1 = new XRTableCell()
            {
                Text = "50",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };

            row9.Cells.AddRange(new XRTableCell[] { r9C0, r9C1 });
            table.Rows.Add(row9);
            //
            //
            // --- Adding 11th row ---
            XRTableRow row10 = new XRTableRow();
            //row10.HeightF = 20F;

            XRTableCell r10C0 = new XRTableCell()
            {
                Text = "Color",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };
            XRTableCell r10C1 = new XRTableCell()
            {
                Text = "50",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };

            row10.Cells.AddRange(new XRTableCell[] { r10C0, r10C1 });
            table.Rows.Add(row10);
            //
            //
            //
            //table.HeightF = row0.HeightF + row1.HeightF + row2.HeightF + row3.HeightF + row4.HeightF + row5.HeightF
            //    + row6.HeightF + row7.HeightF + row8.HeightF + row9.HeightF + row10.HeightF;
                //+ row6.HeightF + row7.HeightF + row8.HeightF + row2.HeightF
            float a = table.WidthF;
            float b = table.HeightF;
            table.EndInit();
            //
            //
            //
            PharmacodePanel.SizeF = new SizeF(this.currnetPageWidth, blockName.HeightF+pic.HeightF+table.HeightF);
            //PharmacodePanel.Borders = DevExpress.XtraPrinting.BorderSide.All;
            //PharmacodePanel.BorderWidth = 2;
            //PharmacodePanel.BorderColor = System.Drawing.Color.Red;
            this.myPoint.Y = this.myPoint.Y + PharmacodePanel.HeightF;
            this.MainDetail.HeightF = this.myPoint.Y;










        }
    }
}   