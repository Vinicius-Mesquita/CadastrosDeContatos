using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial.DbConnection
{
	public class DbConn : Smart.Database.DbConn
	{
		public override string BuildConnString()
		{
			this.ProviderName = "System.Data.SqlClient";
			return "Data Source=172.16.0.185\\DESENVOLVIMENTO;Initial Catalog=db_vinicius.m;User ID=squadjedis;password=selbricoh";
		}
	}
}
