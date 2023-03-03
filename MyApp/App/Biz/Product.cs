using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace MyApp.App.Biz
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime InsertWhen { get; set; }
        public DateTime? UpdateWhen { get; set; } // Check for null type

        public string ConnectionString = "Data Source=DESKTOP\\MSSQLSERVER01;Initial Catalog=Capstone;Integrated Security=True;Trust Server Certificate=true";

        public Product Read(int prodId) {
            try {
                string sql = @"SELECT name, imgUrl, cost, price, description, insertWhen, updateWhen FROM Products WHERE productId = @prodId";
                SqlConnection conn = new SqlConnection(ConnectionString);

                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.Parameters.Add("@prodId", SqlDbType.Int).Value = prodId;

                var reader = sqlCmd.ExecuteReader();
                int index = 0;

                Product product = new Product();
                reader.Read();

                Name = reader.GetString(index++);
                ImgUrl = reader.GetString(index++);
                Cost = reader.GetDecimal(index++);
                Price = reader.GetDecimal(index++);
                Description = reader.GetString(index++);
                InsertWhen = reader.GetDateTime(index++);

                object sqlDateTime = reader[index++]; //https://tinyurl.com/bdfy5wnv
                if (sqlDateTime == DBNull.Value) {
                    UpdateWhen = null;
                } else {
                    UpdateWhen = Convert.ToDateTime(sqlDateTime);
                }
                reader.Close();

                return product;
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return new Product();
            }
        }

        public bool Insert() {
            try {
                Product product = new Product();

                string sql = @"INSERT INTO Products (name, imgUrl, cost, price, description, insertWhen, updateWhen) " +
                    "VALUES (@name, @url, @cost, @price, @desc, @ins, @upd)";
                SqlConnection conn = new SqlConnection(ConnectionString);

                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar).Value = Name;
                sqlCmd.Parameters.Add("@url", SqlDbType.VarChar).Value = ImgUrl;
                sqlCmd.Parameters.Add("@cost", SqlDbType.Money).Value = Cost;
                sqlCmd.Parameters.Add("@price", SqlDbType.Money).Value = Price;
                sqlCmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = Description;
                sqlCmd.Parameters.Add("@ins", SqlDbType.DateTime).Value = InsertWhen;

                if (UpdateWhen != null) {
                    sqlCmd.Parameters.Add("@upd", SqlDbType.DateTime).Value = UpdateWhen;
                } else {
                    sqlCmd.Parameters.Add("@upd", SqlDbType.DateTime).Value = DBNull.Value;
                }
                sqlCmd.CommandType = CommandType.Text;
                int affected = sqlCmd.ExecuteNonQuery();

                return true;

            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return false;
            }
            
        }

    }
}
