using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using SimpleSQL;
using RNGesus.Tools;


namespace RNGesus.database {
    public static class DatabaseManager {

        private static object Lock = new object();


        /// <summary>
        /// Unified function to delete a table entry in a db table.
        /// </summary>
        /// <typeparam name="T">The class that represents the datastructure for a table</typeparam>
        /// <param name="table_Id">The id of the entry to delete</param>
        /// <returns>Number of deleted rows.</returns>
        public static int DeleteEntry<T>(int table_Id) where T : TableBaseData {
            T item = Activator.CreateInstance<T>();
            item.Id = table_Id;
            return GameManager.Instance.databaseManager.Delete<T>(item);
        }

        /// <summary>
        /// Unified function to delete a table entry in a db table.
        /// </summary>
        /// <typeparam name="T">The class that represents the datastructure for a table</typeparam>
        /// <param name="table_Id">The id of the entry to delete</param>
        /// <returns>Number of deleted rows.</returns>
        public static int DeleteEntry<T>(T tableEntry) where T : TableBaseData {
            return GameManager.Instance.databaseManager.Delete<T>(tableEntry);
        }


        /// <summary>
        /// Delete multiple Entires from the db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int DeleteEntry<T>(string where, int limit = 1) where T : TableBaseData {
            string SQL = "DELETE FROM " + (typeof(T).Name) + " WHERE " + where + " LIMIT = " + limit.ToString();
            return GameManager.Instance.databaseManager.Execute(SQL);
        }


        /// <summary>
        /// Update a single entry specified by the id in the structure class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public static void Update<T>(T item) where T : TableBaseData {

            GameManager.Instance.databaseManager.UpdateTable(item);
        }


        /// <summary>
        /// Insert a new row to the database table that mirrors T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static long InsertEntry<T>(T item) where T : TableBaseData {
            long id;
            lock (Lock) {
                GameManager.Instance.databaseManager.Insert(item, out id);
            }
            return id;
        }


        /// <summary>
        /// Select a single entry specified by the id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T SelectEntry<T>(int id) where T : TableBaseData, new() {
            string SQL = "SELECT * FROM " + (typeof(T).Name) + " WHERE Id = " + id.ToString();
            List<T> items = new List<T>();
            items = GameManager.Instance.databaseManager.Query<T>(SQL);
            if (items.Count > 0) {
                return items[0];
            }
            return null;
        }

        /// <summary>
        /// Select a single entry specified by a customizable where
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static T SelectEntry<T>(string where) where T : TableBaseData, new() {
            string SQL = "SELECT * FROM " + (typeof(T).Name) + " WHERE " + where;
            byte[] bytes = Encoding.Default.GetBytes(SQL);
            SQL = Encoding.ASCII.GetString(bytes);
            List<T> items = new List<T>();
            items = GameManager.Instance.databaseManager.Query<T>(SQL);
            if (items.Count > 0) {
                return items[0];
            }
            return null;
        }

        /// <summary>
        /// Select multiple Entire given a where string for the sql query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<T> SelectMultipleEntries<T>(string where) where T : TableBaseData, new() {
            string SQL = "SELECT * FROM " + (typeof(T).Name) + " WHERE " + where;
            byte[] bytes = Encoding.Default.GetBytes(SQL);
            SQL = Encoding.ASCII.GetString(bytes);
            List<T> items = new List<T>();
            items = GameManager.Instance.databaseManager.Query<T>(SQL);
            if (items.Count > 0) {
                return items;
            }
            return null;
        }

        /// <summary>
        /// Get the whole Table as a List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> SelectAll<T>(string order = "") where T : TableBaseData, new() {
            string SQL = "SELECT * FROM " + (typeof(T).Name);
            if (order.Length > 0) {
                SQL += " ORDER BY " + order;
            }
            return GameManager.Instance.databaseManager.Query<T>(SQL);
        }

        /// <summary>
        /// Simple Row Count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetRowCount<T>(string group = "") where T : TableBaseData, new() {
            string SQL = "SELECT * FROM " + (typeof(T).Name);
            if (group.Length > 0) {
                SQL += " GROUP BY " + group;
            }
            SimpleDataTable table = GameManager.Instance.databaseManager.QueryGeneric(SQL);
            return table.rows.Count;
        }

    }


    /// <summary>
    /// This is the base class for all table relevant data constructs.
    /// Have your table structure data class always inherit from this one!
    /// 
    /// IMPORTANT: All tables need an Id field as an autoincrement primary key!
    /// 
    /// </summary>
    public class TableBaseData {
        // The Id field is set as the primary key in the SQLite database,
        // so we reflect that here with the PrimaryKey attribute
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

    }
}