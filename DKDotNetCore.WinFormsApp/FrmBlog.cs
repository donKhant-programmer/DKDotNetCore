using DKDotNetCore.Shared;
using DKDotNetCore.WinFormsApp.Models;
using DKDotNetCore.WinFormsApp.Queries;
using System.Windows.Forms;

namespace DKDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DappperService _dappperService;

        public FrmBlog()
        {
            InitializeComponent();
            _dappperService = new DappperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = txtTitle.Text;
                blog.BlogAuthor = txtAuthor.Text;
                blog.BlogContent = txtContent.Text;

                int result = _dappperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Saving successful!" : "Saving failed";

                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;

                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);

                ClearControls();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BlogLst blog = new BlogLst();
            blog.Tag = this;
            blog.Show(this);
            Hide();
        }

        private void ClearControls()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();

            txtTitle.Focus();
        }
    }
}
