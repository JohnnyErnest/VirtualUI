using Horsey;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using UnityEngine;

public class ORMScript : MonoBehaviour {
    public class SQLiteTest : SQLResult
    {
        public long? rowid { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    // Use this for initialization
    void Start () {

        string connectionStringSQLite = @"Data Source=C:\Users\JohnErnest\Documents\TestSQLite.db;Version=3;";

        DatabaseTableInfo sqliteTable = new DatabaseTableInfo()
        {
            TableName = "TestTable"
        };

        HorseyTableSQLLite reportLite = new HorseyTableSQLLite(connectionStringSQLite, sqliteTable);
        reportLite.GetAll<SQLiteTest, SQLiteConnection>();
        foreach (var row in reportLite.SelectAllCommand.RetrievalResult as SQLiteTest[])
        {
            Debug.Log(string.Format("{0}\t{1}\t{2}\t{3}", row.rowid, row.FirstName, row.LastName, row.Email));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
