using DKDotNetCore.Shared;
using DKDotNetCore.WinFormsApp.Models;
using DKDotNetCore.WinFormsApp.Queries;

namespace DKDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DappperService _dappperService;
        private int _BlogId;

        public FrmBlog()
        {
            InitializeComponent();
            _dappperService = new DappperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        public FrmBlog(int blogId)
        {
            InitializeComponent();
            _dappperService = new DappperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            _BlogId = blogId;
            var model = _dappperService.QueryFirstOrDefault<BlogModel>("select * from table_blog where blogId = @blogId", new { BlogId = _BlogId });

            txtTitle.Text = model.BlogTitle;
            txtAuthor.Text = model.BlogAuthor;
            txtAuthor.Text = model.BlogAuthor;
            txtContent.Text = model.BlogContent;

            btnSave.Visible = false;
            btnUpdate.Visible = true;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var item = new BlogModel
                {
                    BlogId = _BlogId,
                    BlogTitle = txtTitle.Text,
                    BlogAuthor = txtAuthor.Text,
                    BlogContent = txtContent.Text
                };

                string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

                int result = _dappperService.Execute(query, item);
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";
                MessageBox.Show(message);

                this.Close();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
    }
}
