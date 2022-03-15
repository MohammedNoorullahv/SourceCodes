using OptimizerAddOn.Dashboard;
using OptimizerAddOn.HR;
using OptimizerAddOn.Issues;
using OptimizerAddOn.Packing;
using OptimizerAddOn.Transfers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizerAddOn.MDI
{
    public partial class FrmMDI : Form
    {
        private int childFormNumber = 0;
        string ipAddress = "";

        public FrmUnAssignedStockTransfer frmUnAssignedStockTransfer;
        public FrmAssignedStockTransfer frmAssignedStockTransfer;
        public FrmIssues frmIssues;

        public FrmGeneratePacking frmGeneratePacking;
        public FrmTempeScanning frmTempeScanning;

        public FrmSalaryImport frmSalaryImport;

        public FrmOrderOutstanding frmOrderOutstanding;

        public FrmMDI()
        {
            InitializeComponent();

            frmUnAssignedStockTransfer = new FrmUnAssignedStockTransfer();
            frmAssignedStockTransfer = new FrmAssignedStockTransfer();
            frmIssues = new FrmIssues();
            frmGeneratePacking = new FrmGeneratePacking();
            frmTempeScanning = new FrmTempeScanning();
            frmSalaryImport = new FrmSalaryImport();
            frmOrderOutstanding = new FrmOrderOutstanding();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

       
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void cbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMDI_Load(object sender, EventArgs e)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));

            //nBarMain.Visible = false;
            //return;

            tbIPAddress.Text = ipAddress;
            tbSystemName.Text = Dns.GetHostName();
            
            string sSystemName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            MdlApp.sSystemId = ipAddress;

            //int index1 = sSystemName.IndexOf('\\');
            //tbSystemName.Text = sSystemName.Substring(0, index1);

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(MdlApp.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from SystemMapping Where SystemName = @mSystemName And IPAddress = @mIPAddress";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@mSystemName", SqlDbType.VarChar)).Value = tbSystemName.Text;
                cmd.Parameters.Add(new SqlParameter("@mIPAddress", SqlDbType.VarChar)).Value = ipAddress;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                int NRowCount;
                NRowCount = Convert.ToInt32(dt.Rows.Count);
                //i = 0;

                if (Convert.ToInt32(dt.Rows.Count) == 0)
                {
                    MessageBox.Show("This user / system is not Mapped to any store");
                    nBarMain.Visible = false;
                }
                else
                {
                    MdlApp.sUnitCode = ds.Tables[0].Rows[0]["Unit"].ToString();

                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "Select * from UserLog Where Status = 'LoggedIn' And (LoggedIn_Date = @mLoggedIn_Date) And MachineName = @mMachineName";
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Parameters.Add(new SqlParameter("@mLoggedIn_Date", SqlDbType.DateTime)).Value = DateTime.Now.Date;
                    cmd2.Parameters.Add(new SqlParameter("@mMachineName", SqlDbType.VarChar)).Value = tbSystemName.Text;

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    adapter2.Fill(ds2);

                    DataTable dt2 = new DataTable();
                    adapter2.Fill(dt2);

                    NRowCount = Convert.ToInt32(dt2.Rows.Count);
                    if (NRowCount == 0)
                    {
                        //MessageBox.Show("This user is not logged in ERP Module");
                        nBarMain.Visible = false;
                    }
                    else
                    {
                        tbAPLogin.Text = ds2.Tables[0].Rows[0]["UserId"].ToString();
                        MdlApp.sUserName = ds2.Tables[0].Rows[0]["UserId"].ToString() + " / " + 
                            ds2.Tables[0].Rows[0]["EMPName"].ToString() + " / " + ds2.Tables[0].Rows[0]["EmpCode"].ToString();
                        //UserId / EMPName / EmpCode
                        //MachineName / MachineId
                        MdlApp.sEnteredOnMachineID = ds2.Tables[0].Rows[0]["MachineName"].ToString() + " / " +
                            ds2.Tables[0].Rows[0]["MachineId"].ToString();
                        
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["LocationCount"]) > 1)
                        {
                            tbStoreCode.Text = "Multiple Store Access";
                            MdlApp.nStoreCount = Convert.ToInt32(ds.Tables[0].Rows[0]["LocationCount"]);
                        }
                        else
                        {
                            tbStoreCode.Text = ds.Tables[0].Rows[0]["Location"].ToString();
                            MdlApp.nStoreCount = 1;
                        }
                        nBarMain.Visible = true;
                        nBarMain.BringToFront();
                    }
                }
            }
        }

        #region "STORE TRANSACTION MENUS"
        private void SMUnAssignedStck_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            //FrmUnAssignedStockTransfer frmUnAssignedStockTransfer = new FrmUnAssignedStockTransfer();
            var frm = this.frmUnAssignedStockTransfer;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
            ToolsSendToBack();
        }

        private void SMSalesOrder_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmAssignedStockTransfer frmAssignedStockTransfer = new FrmAssignedStockTransfer();
            var frm = this.frmAssignedStockTransfer;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
            ToolsSendToBack();
        }

        private void SMJobcardIssue_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmIssues frmIssues = new FrmIssues();
            var frm = this.frmIssues;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
            ToolsSendToBack();
        }
        #endregion

        #region "PACKING MENUS"

        private void smGeneratePackingList_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmGeneratePacking frmGeneratePacking = new FrmGeneratePacking();
            var frm = this.frmGeneratePacking;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
            ToolsSendToBack();
        }

        private void smTempeScanning_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmTempeScanning frmTempeScanning = new FrmTempeScanning();
            var frm = this.frmTempeScanning;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
            ToolsSendToBack();
        }
        #endregion

        #region "HR MENUS"

        private void SMImportSalary_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmSalaryImport frmSalaryImport = new FrmSalaryImport();
            var frm = this.frmSalaryImport;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
            ToolsSendToBack();
        }

        #endregion

        #region "DASHBOARD"

        private void SMOrderOutstanding_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmOrderOutstanding frmOrderOutstanding = new FrmOrderOutstanding();
            var frm = this.frmOrderOutstanding;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
            frm.Show();
            ToolsSendToBack();
        }
        #endregion

        #region FUNCTIONS
        public void ToolsBringToFront()
        {
            plMdi.Visible = true;
            plMdi.BringToFront();
        }

        public void ToolsSendToBack()
        {
            plMdi.Visible = false;
        }


        #endregion

        private void plMdi_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
