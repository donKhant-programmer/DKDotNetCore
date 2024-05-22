using DKDotNetCore.Shared;
using DKDotNetCore.WinFormsApp.Models;
using DKDotNetCore.WinFormsApp.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DKDotNetCore.WinFormsApp
{
    public partial class BlogLst : Form
    {
        private readonly DappperService _dappperService;

        public BlogLst()
        {
            InitializeComponent();
            _dappperService = new DappperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            var lst = _dappperService.Query<BlogModel>(BlogQuery.Blogs).ToList();

            lstView.Columns.Add("Title");
            lstView.Columns.Add("Author");
            lstView.Columns.Add("Content");

            foreach (var item in lst)
            {
                lstView.Items.Add(item.BlogTitle, item.BlogAuthor, item.BlogContent);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
