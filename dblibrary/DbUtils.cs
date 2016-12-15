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
        static SQLiteTransaction trans = null;
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
            if (conn != null)
            {
                conn.Open();
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                //ExecuteNonQuery("begin transaction;");
                System.Diagnostics.Debug.WriteLine("Start Transaction");
            }
        }

        public static void Commit()
        {
            if (trans != null)
            {
                trans.Commit();
                trans.Dispose();
                //ExecuteNonQuery("commit transaction;");
                System.Diagnostics.Debug.WriteLine("End Transaction");
                conn.Close();
            }
        }

        public static void Rollback()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
                //ExecuteNonQuery("rollback transaction;");
                System.Diagnostics.Debug.WriteLine("Rollback Transaction");

                conn.Close();
            }
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
                System.Diagnostics.Debug.WriteLine(msg);

            }

            catch (Exception ex)
            {
                 
                string msg = ex.Message;
                System.Diagnostics.Debug.WriteLine(msg);
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
            bool needsClose = false;

			try
			{

				SQLiteConnection cnn = GetConnection();

                if (cnn.State != ConnectionState.Open)
                {
                    needsClose = true;
                    cnn.Open();
                }

				SQLiteCommand mycommand = new SQLiteCommand(cnn);

				mycommand.CommandText = sql;

				rowsUpdated = mycommand.ExecuteNonQuery();

                if (needsClose)
				    cnn.Close();

			}
			catch (SQLiteException exc)
			{
				string msg = exc.Message;
                System.Diagnostics.Debug.WriteLine(msg);
            }
			catch (Exception ex)
			{
				string msg = ex.Message;
                System.Diagnostics.Debug.WriteLine(msg);
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
