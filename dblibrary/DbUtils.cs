using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DS.Showdown.DbLibrary
{
	public class DbUtils
	{
		static SQLiteConnection conn = null;
        static string path = null;
        static string connString = null;

        public static void SetPath(string Path)
        {
            path = Path;
        }

        public static void SetConnectionString(string connStr)
        {
            connString = connStr;
        }

        public static void StartTransaction()
        {
            ExecuteNonQuery("begin transaction;");
        }

        public static void Commit()
        {
            ExecuteNonQuery("commit transaction;");
        }

        public static void Rollback()
        {
            ExecuteNonQuery("rollback transaction;");
        }

        public static SQLiteConnection GetConnection()
		{
			if (conn == null)
			{

                if (connString == null)
                {
                    LoadConnectionString();
                }

                if (connString != null)
                {
                    conn = new SQLiteConnection(connString);
                }
                else
                {
                    if (path != null)
                    {
                        conn = new SQLiteConnection(path);
                    }
                    else
                    {
                        conn = new SQLiteConnection(@"Data Source=D:\github-repos\showdownsharp\db\showdown.db");
                    }
                }

			}

			return conn;
		}

        private static void LoadConnectionString()
        {
            connString = ConfigurationManager.ConnectionStrings["dblibrary.showdownConnectionString"].ConnectionString;
        }


		public static DataTable GetDataTable(string sql)
		{

			DataTable dt = new DataTable();
            SQLiteConnection cnn = null;

            try
            {

                cnn = GetConnection();

                cnn.Open();

                SQLiteCommand mycommand = new SQLiteCommand(cnn);

                mycommand.CommandText = sql;

                SQLiteDataReader reader = mycommand.ExecuteReader();

                dt.Load(reader);

                reader.Close();

            }
            catch (SQLiteException exc)
            {

                string msg = exc.Message;

                // Catching exceptions is for communists

            }

            catch (Exception ex)
            {
                 
                string msg = ex.Message;

                // Catching exceptions is for communists

            }
            finally
            {
                cnn.Close();
            }

			return dt;

		}

		public static int ExecuteNonQuery(string sql)
		{
			int rowsUpdated = 0;

			try
			{

				SQLiteConnection cnn = GetConnection();

				cnn.Open();

				SQLiteCommand mycommand = new SQLiteCommand(cnn);

				mycommand.CommandText = sql;

				rowsUpdated = mycommand.ExecuteNonQuery();

				cnn.Close();

			}
			catch (SQLiteException exc)
			{
				string msg = exc.Message;

				// Catching exceptions is for communists

			}
			catch (Exception ex)
			{
				string msg = ex.Message;

				// Catching exceptions is for communists

			}

			return rowsUpdated;

		}

		public static string ExecuteScalar(string sql)
		{

            SQLiteConnection cnn = GetConnection();

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


	}
}
