using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MuonTraSach.GUI;

namespace MuonTraSach
{
    public partial class frmMain : Form
    {
        bool sidebarExpand;
        bool menuExpand = false;
        public frmMain()
        {
            InitializeComponent();

            // wire sidebar buttons to open corresponding forms in panelMain
            btnHome.Click += BtnHome_Click;
            btnSearch.Click += BtnSearch_Click;
            btnDashboard.Click += BtnDashboard_Click;

            // management submenu buttons
            btnUser.Click += btnUser_Click; // designer already wires this to toggle menu; keep it but ensure opening form also happens
            btnBook.Click += BtnBook_Click;
            btnAuthor.Click += BtnAuthor_Click;
            btnList.Click += BtnList_Click;
            btnStaff.Click += BtnStaff_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sidebar.Width = sidebar.MinimumSize.Width;
            sidebarExpand = false;
            menuContainer.Height = menuContainer.MinimumSize.Height;
            menuExpand = false;
            headerTimer.Start();

            // Optionally show dashboard on start
            OpenChildForm(new FrmDashboard());
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 50;

                if (sidebar.Width <= sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 50;

                if (sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void menuTimer_Tick(object sender, EventArgs e)
        {
            if (menuExpand)
            {
                menuContainer.Height -= 50;
                if (menuContainer.Height <= menuContainer.MinimumSize.Height)
                {
                    menuExpand = false;
                    menuTimer.Stop();
                }
            }
            else
            {
                menuContainer.Height += 50;
                if (menuContainer.Height >= menuContainer.MaximumSize.Height)
                {
                    menuExpand = true;
                    menuTimer.Stop();
                }
            }
        }

        // Keep toggle behavior and also open the QLUser form
        private void btnUser_Click(object sender, EventArgs e)
        {
            menuTimer.Start();

            // open user management form inside panelMain
            OpenChildForm(new FrmQLUser());
        }

        private void headerTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Opens a Form inside the central panel (panelMain).
        /// Clears any existing controls and hosts the child form as a borderless, docked control.
        /// </summary>
        /// <param name="childForm">The form to show inside panelMain.</param>
        private void OpenChildForm(Form childForm)
        {
            if (childForm == null) return;

            // Dispose previous child form controls (if any)
            foreach (Control c in panelMain.Controls)
            {
                c.Dispose();
            }
            panelMain.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.Show();
            childForm.BringToFront();
        }

        // Button click handlers to open respective forms
        private void BtnHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmDashboard());
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTimKiem());
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmDashboard());
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQLSach());
        }

        private void BtnAuthor_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQLTacGia());
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQLDanhMuc());
        }

        private void BtnStaff_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQLNhanVien());
        }
    }
}