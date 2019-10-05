using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using Npgsql;

namespace CrudApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindingSource bs1 = new BindingSource();
            try
            {
                dgvemployees.AutoGenerateColumns = true;

                string sql = "select first_name, last_name, address from employee;";
                using (var connection = new NpgsqlConnection("host=localhost;Username=postgres;Password=pass;Database=cruddb"))
                {
                    connection.Open();
                    var employees = connection.Query(sql).ToList();
                    bs1.DataSource = employees;
                    dgvemployees.DataSource = bs1;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void Btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "insert into employee (first_name,last_name, address) values (@first,@last,@address);";
                using (var connection = new NpgsqlConnection("host=localhost;Username=postgres;Password=pass;Database=cruddb"))
                {
                    connection.Open();
                    var affectedrows = connection.Execute(sql, new {
                        first = txtfirstname.Text,
                        last = txtlastname.Text,
                        address = txtaddress.Text
                    });
                    string outputstr = affectedrows.ToString();
                    MessageBox.Show(outputstr, "CRUD App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        
        }

        private void Btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                txtfirstname.Text = "";
                txtlastname.Text = "";
                txtaddress.Text = "";   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "delete from  employee where first_name = @first;";
                using (var connection = new NpgsqlConnection("host=localhost;Username=postgres;Password=pass;Database=cruddb"))
                {
                    connection.Open();
                    var affectedrows = connection.Execute(sql, new
                    {
                        first = txtfirstname.Text
                    });
                    string outputstr = affectedrows.ToString();
                    MessageBox.Show(outputstr, "coder", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtfirstname.Text = "";
                    txtlastname.Text = "";
                    txtaddress.Text = "";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
