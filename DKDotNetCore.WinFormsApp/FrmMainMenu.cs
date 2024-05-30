using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DKDotNetCore.WinFormsApp
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlog frmBlog = new FrmBlog();
            frmBlog.ShowDialog();
        }

        private void blogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBlogList frmBlogList = new frmBlogList();
            frmBlogList.ShowDialog();
        }
    }
}
