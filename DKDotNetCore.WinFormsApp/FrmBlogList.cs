using DKDotNetCore.Shared;
using DKDotNetCore.WinFormsApp.Models;
using DKDotNetCore.WinFormsApp.Queries;

namespace DKDotNetCore.WinFormsApp
{
    public partial class frmBlogList : Form
    {
        private readonly DappperService _dappperService;
        //private const int _edit = 1;
        //private const int _delete = 2;
        public frmBlogList()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dappperService = new DappperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            //int blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["ColId"].Value);
            var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["id"].Value);
            var blogAuthor = dgvData.Rows[e.RowIndex].Cells["colTitle"].Value.ToString;

            if (e.ColumnIndex == (int) EnumFormControlType.Edit)
            {
                FrmBlog frm = new FrmBlog(blogId: blogId);
                frm.ShowDialog();

                ShowBlogs();

            } else if (e.ColumnIndex == (int) EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure u want to delete this", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);
            }
        }

        private void DeleteBlog(int id)
        {
            string query = @"Delete from [dbo].[Table_Blog] where BlogId = @BlogId";
            int result = _dappperService.Execute(query, new { BlogId = id });

            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            MessageBox.Show(message);
        }

        private void frmBlogList_Load(object sender, EventArgs e)
        {
            ShowBlogs();
        }

        private void ShowBlogs()
        {
            List<BlogModel> lst = _dappperService.Query<BlogModel>(BlogQuery.Blogs);
            dgvData.DataSource = lst;
            //MessageBox.Show("id: " + lst[0].BlogId);
        }
    }
}
