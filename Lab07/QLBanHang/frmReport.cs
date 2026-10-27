using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QLBanHang
{
    public class frmReport : Form
    {
        public string HoTenKH { get; set; }
        public List<HangChonMua> DSMua { get; set; }

        private ReportViewer reportViewer1;

        public frmReport(string hoten, List<HangChonMua> dsmua)
        {
            Text = "Hóa đơn bán hàng";
            ClientSize = new Size(820, 620);
            StartPosition = FormStartPosition.CenterParent;

            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            Controls.Add(reportViewer1);

            HoTenKH = hoten;
            DSMua = dsmua;

            Load += frmReport_Load;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            // Tham số bình thường
            ReportParameter[] param = new ReportParameter[1];
            param[0] = new ReportParameter("txtHoTenKH", HoTenKH);

            reportViewer1.LocalReport.ReportEmbeddedResource = "rptBanHang.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsHangChonMua", DSMua));
            reportViewer1.LocalReport.SetParameters(param);

            this.reportViewer1.RefreshReport();
        }
    }
}
