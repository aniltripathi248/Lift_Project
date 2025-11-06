using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// BackgroundWorker lets you perform work in the background without freezing the user interface.

namespace Lift_Project
{
	public class DBContext
	{
		string _connectionString = @"Server=AJAY; Database= liftDemo; Trusted_Connection = True; TrustServerCertificate = True;";

		public void InsertLogsIntoDB(DataTable dt)
		{
			try
			{
				using(SqlConnection conn = new SqlConnection(_connectionString))
				{
					string insertQuery = @"Insert into logs (LogTime, EventDescription) values (@Time, @Log)";

					using(SqlDataAdapter adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = new SqlCommand(insertQuery, conn);
						adapter.InsertCommand.Parameters.Add("@Time", SqlDbType.DateTime, 0, "LogTime");
						adapter.InsertCommand.Parameters.Add("@Log", SqlDbType.VarChar, 20, "EventDescription");

						conn.Open();

						adapter.Update(dt);
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error saving logs to DB: " + ex.Message);
			}
		}

		public void loadLogsFromDB(DataTable dt, DataGridView dataGridView)
		{
			string query = @"Select LogTime, EventDescription from logs order by LogTime desc";

			try
			{
				using(SqlConnection conn = new SqlConnection(_connectionString))
				{
					using(SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
					{
						dt.Rows.Clear();
						adapter.Fill(dt);

						dataGridView.Rows.Clear();

						foreach (DataRow row in dt.Rows)
						{
							string currentTime = Convert.ToDateTime(row["LogTime"]).ToString("hh:mm:ss");
							string events = row["EventDescription"]?.ToString() ?? string.Empty;

							dataGridView.Rows.Add(currentTime, events);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading logs from DB: " + ex.Message);
			}
		}
	}
}
