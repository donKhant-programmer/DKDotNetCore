using DKDotNetCore.Shared;
using DKDotNetCore.WinFormsApp;

namespace DKDotNetCore.WinFormsAppSqlInjection
{
    public partial class Form1 : Form
    {
        private readonly DappperService _dappperService;
        public Form1()
        {
            InitializeComponent();
            _dappperService = new DappperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string query = $"select * from table_user where email = '{txtEmail.Text.Trim()}' and password = '{txtPassword.Text.Trim()}'";
            string query = $"select * from table_user where email = @Email and password = @Password";
            var model = _dappperService.QueryFirstOrDefault<UserModel>(query, new
            {
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Text.Trim()
            });

            if (model is null)
            {
                MessageBox.Show("User doesn't exist!");
                return;
            }

            MessageBox.Show("Is Admin: " + model.Email);
        }

    }

    public class UserModel
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
