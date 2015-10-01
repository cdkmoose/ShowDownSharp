using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBTestBed
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public static DataTable GetDataTable (string sql)

		{

			DataTable dt = new DataTable();

			try

			{

				SQLiteConnection cnn = new SQLiteConnection(@"Data Source=C:\Projects\showdownsharp\db\showdown.db");

				cnn.Open();

				SQLiteCommand mycommand = new SQLiteCommand(cnn);

				mycommand.CommandText = sql;

				SQLiteDataReader reader = mycommand.ExecuteReader();

				dt.Load(reader);

				reader.Close();

				cnn.Close();

			} catch {

			// Catching exceptions is for communists

			}

		return dt;

		}
		
		public static int ExecuteNonQuery(string sql)

		{

			SQLiteConnection cnn = new SQLiteConnection(@"Data Source=C:\Projects\showdownsharp\db\showdown.db");

			cnn.Open();

			SQLiteCommand mycommand = new SQLiteCommand(cnn);

			mycommand.CommandText = sql;

			int rowsUpdated = mycommand.ExecuteNonQuery();

			cnn.Close();

			return rowsUpdated;

		}

		public static string ExecuteScalar(string sql)

		{

			SQLiteConnection cnn = new SQLiteConnection(@"Data Source=C:\Projects\showdownsharp\db\showdown.db");

			cnn.Open();

			SQLiteCommand mycommand = new SQLiteCommand(cnn);

			mycommand.CommandText = sql;

			object value = mycommand.ExecuteScalar();

			cnn.Close();

			if (value != null)

			{

				return value.ToString();

			}

			return "";

		}

		private void getDataButton_Click(object sender, EventArgs e)
		{
			dataGridView1.DataSource = GetDataTable("select * from teams;");
		}
	}
}