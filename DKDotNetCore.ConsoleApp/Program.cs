using DKDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource = "DESKTOP-07LAQVO";
//stringBuilder.InitialCatalog = "DotnetTrainingBatch4";
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "sa@123";

//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//connection.Open();
//Console.WriteLine("Connection opened");

//String query = "select * from table_blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);

//connection.Close();
//Console.WriteLine("Connection closed");

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog Id => " + dr["BlogId"]);
//    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
//    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
//    Console.WriteLine("---------------------------------");
//}

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Read();
//adoDotNetExample.Create("myTitle", "myAuthor", "myContent");

Console.ReadKey();
