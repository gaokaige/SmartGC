using SmartGC.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormMain : Form
    {
        DataTable dtCardInfo, dtCommodityInfo;


        public FormMain()
        {
            InitializeComponent();
            dgvCardInfo.AutoGenerateColumns = false;
            dgvCommodity.AutoGenerateColumns = false;
        }

        private void tsmiSetting_Click(object sender, EventArgs e)
        {
            FormConfig frmSettings = new FormConfig();
            frmSettings.ShowDialog();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new FormAbout();
            frmAbout.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            pagerCardInfo.OnPageChanged += new EventHandler(pagerCardInfo_OnPageChanged);
            LoadCardInfo();
        }

        private void pagerCardInfo_OnPageChanged(object sender, EventArgs e)
        {
            LoadCardInfo();
        }

        private void LoadCardInfo()
        {
            dtCardInfo = Common.GetCardInfoData("json");

            DataTable dt = dtCardInfo.Clone();

            DataRow[] rows = dtCardInfo.Select("Index>" + (pagerCardInfo.PageIndex - 1) * pagerCardInfo.PageSize + "and Index<=" + pagerCardInfo.PageIndex * pagerCardInfo.PageSize);

            foreach (DataRow row in rows)
            {
                dt.Rows.Add(row.ItemArray);
            }

            dgvCardInfo.DataSource = dt;
            pagerCardInfo.DrawControl(dtCardInfo.Rows.Count);
        }

        private void dgvCardInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                DataTable dt = dtCardInfo.Clone();
                DataRow[] rows = dtCardInfo.Select("Index=" + dgvCardInfo.Rows[e.RowIndex].Cells[0].Value);
                FormCardInfo frm = new FormCardInfo(rows[0]);
                frm.ShowDialog();
            }
        }
    }
}
