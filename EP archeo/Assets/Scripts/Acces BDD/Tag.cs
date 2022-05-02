using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

namespace bdd_ep
{

    /// <summary>
    /// Manage tags describing archaeological objects
    /// </summary>
    public class Tag
    {
        public int _idTag;
        public string _tagName;
        public string _parentName;

        /// <summary>
        /// Create a tag
        /// </summary>
        /// <param name="idTag">Tag id</param>
        /// <param name="tagName">Tag's name</param>
        /// <param name="parentName">Parent tag's name (may be empty)</param>
        private Tag(int idTag, string tagName, string parentName)
        {
            this._idTag = idTag;
            this._tagName = tagName;
            this._parentName = parentName;
        }

        /// <summary>
        /// Associate a archaeological object to a tag
        /// </summary>
        /// <param name="idObj">Archaeological object id (integer)</param>
        /// <param name="idTag">Tag id</param>
        /// <returns>True if and only if everything went well</returns>
        public static bool AssociateObjectToTag(int idObj, int idTag)
        {
            if ((Database.DataReader($"SELECT * FROM tag WHERE id_tag = {idTag};").Rows.Count == 0) ||
                (Database.DataReader($"SELECT * FROM obj WHERE id_obj = {idObj};").Rows.Count == 0) || (Database
                    .DataReader($"SELECT * FROM tag_map WHERE id_tag = {idTag} AND id_obj={idObj};").Rows
                    .Count != 0)) return false;
            Database.DataWriter($"INSERT INTO tag_map (id_tag, id_obj) VALUES ({idTag},{idObj});");
            return true;
        }

        /// <summary>
        /// Create a new tag
        /// </summary>
        /// <param name="tagName">New tag's name</param>
        /// <param name="parentName">Parent tag's name (may be empty)</param>
        /// <returns>The new tag. Return null if something went wrong.</returns>
#nullable enable
        public static Tag? CreateNewTag(string tagName, string parentName)
        {
            tagName = Database.DoubleApostropheAndRemoveSemiColumn(tagName);
            parentName = Database.DoubleApostropheAndRemoveSemiColumn(parentName);
            // If the tag exist : do not insert anything
            if (Database.DataReader($"SELECT * FROM tag WHERE tag_name = '{tagName}' AND parent_tag = '{parentName}';")
                    .Rows.Count != 0) return null;
            Database.DataWriter($"INSERT INTO tag (tag_name, parent_tag) VALUES ('{tagName}','{parentName}');");

            // Get tag id in Db
            var idTagInDb = int.Parse(Database
                .DataReader($"SELECT id_tag FROM tag WHERE tag_name = '{tagName}' AND parent_tag = '{parentName}';")
                .Rows[0][0].ToString()!);

            return new Tag(idTagInDb, tagName, parentName);
        }

        /// <summary>
        /// Delete a tag from the database
        /// </summary>
        /// <param name="tagName">Tag name to delete</param>
        public static void DeleteTag(string tagName)
        {
            tagName = Database.DoubleApostropheAndRemoveSemiColumn(tagName);
            // Delete all children
            var tagsToDelete = Database.DataReader($"SELECT * FROM tag WHERE parent_tag = '{tagName}';");
            foreach (DataRow tag in tagsToDelete.Rows)
            {
                var tagNameInString = tag["tag_name"].ToString();
                // Delete all children
                if (tagNameInString != null)
                    DeleteTag(tagNameInString);
            }

            // Delete the tag
            Database.DataWriter($"DELETE FROM tag WHERE tag_name = '{tagName}'");
        }


        /// <summary>
        /// Declare paternity between two tags
        /// </summary>
        /// <param name="parentName">parent tag's name</param>
        /// <param name="childName">child tag's name</param>
        /// <returns>False if and only if nothing as be done : parent or child does not exist in the database</returns>
        public static bool DeclareTagPaternity(string parentName, string childName)
        {
            parentName = Database.DoubleApostropheAndRemoveSemiColumn(parentName);
            childName = Database.DoubleApostropheAndRemoveSemiColumn(childName);
            // Of ascending or descendant does not exist : do nothing and return false
            if (Database.DataReader($"SELECT * FROM tag WHERE tag_name = '{childName}';").Rows.Count == 0 ||
                Database.DataReader($"SELECT * FROM tag WHERE tag_name = '{parentName}';").Rows.Count == 0)
                return false;
            Database.DataWriter($"UPDATE tag SET parent_tag = '{parentName}' WHERE tag_name = '{childName}';");
            return true;
        }

        /// <summary>
        /// Import a set of tags from a csv file
        /// </summary>
        /// <param name="pathToFile">Path to the csv file</param>
        /// <returns>True if and only if everything went well</returns>
        public static bool ImportCsvToDataBase(string pathToFile)
        {
            // Regex to delete all char before the first capital letter
            const string filterRegex = @"(.*)[A-Z]";

            // Store in an array
            var globalList = new List<string[]>();

            // Read the csv file

            using (StreamReader sr = File.OpenText(pathToFile))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var rows = s.Split(';');

                    // Delete the indexes
                    rows = DeleteRegexInArray(rows, filterRegex)!;

                    // Add line in the list
                    if (rows != null)
                    {
                        globalList.Add(rows);
                    }
                }
            }

            InsertListToDatabase(globalList);
            return false;
        }

        /// <summary>
        /// Insert a list of arrays (Catégorie fonctionnelle;Sous-catégorie fonctionnelle;Groupe fonctionnel) into the database
        /// </summary>
        /// <param name="stringsList">List of arrays to insert</param>
        private static void InsertListToDatabase(List<string[]> stringsList)
        {
            // Variables in CSV file
            var categorieFonctionnelle = "";
            var sousCategorieFonctionnelle = "";
            var groupeFonctionnel = "";

            // Delete table header
            var firstItemOfList = stringsList.GetRange(0, 1);
            var header1 = firstItemOfList.GetRange(0, 1).ToArray();
            if (header1.Equals("Catégorie fonctionnelle"))
                stringsList.Remove(firstItemOfList[0]);

            // Iterate on the array and insert into database
            var globalArray = stringsList.ToArray();
            foreach (var t in globalArray)
            {
                for (var j = 0; j < t.Length; j++)
                {
                    // If we find a new categorie fonctionnelle
                    if (j == 0 && t[j] != "" && t[j] != categorieFonctionnelle)
                    {
                        categorieFonctionnelle = t[j];
                        CreateNewTag(categorieFonctionnelle, "");
                    }

                    // If we find a new sous-categorie fonctionnelle
                    if (j == 1 && t[j] != "" && t[j] != sousCategorieFonctionnelle)
                    {
                        sousCategorieFonctionnelle = t[j];
                        CreateNewTag(sousCategorieFonctionnelle, categorieFonctionnelle);
                    }

                    // If we find a new groupe fonctionnel
                    if (j == 2 && t[j] != "" && t[j] != groupeFonctionnel)
                    {
                        groupeFonctionnel = t[j];
                        CreateNewTag(groupeFonctionnel, sousCategorieFonctionnelle);
                    }
                }
            }
        }

        /// <summary>
        /// Delete an expression asking to the first group of a regex in each cell of a table
        /// </summary>
        /// <param name="rows">Array of strings</param>
        /// <param name="filterRegex">Regex with one or more groups</param>
        /// <returns>Array of strings where each occurence of the regex has been delete</returns>
        private static string[]? DeleteRegexInArray(string[] rows, string filterRegex)
        {
            for (var i = 0; i < rows.Length; i++)
            {
                // Only where the string is not empty
                if (rows[i].Equals("")) continue;
                foreach (Match match in Regex.Matches(rows[i], filterRegex))
                {
                    if (match.Groups[1].Value != "")
                    {
                        // Delete the first group
                        rows[i] = rows[i].Replace(match.Groups[1].Value, "");
                    }
                }
            }

            return rows;
        }

        /// <summary>
        /// Get all objects id associated to the tag
        /// </summary>
        /// <param name="idTag">Tag id</param>
        /// <returns>List of all objects id associated to the tag</returns>
        public static List<int> GetObjectsAssociatedWithTag(int idTag)
        {
            var queryResult = Database.DataReader($"SELECT id_obj FROM tag_map WHERE id_tag = {idTag}");
            var listToReturn = new List<int>();
            foreach (DataRow row in queryResult.Rows)
            {
                listToReturn.Add(int.Parse(row["id_obj"].ToString()!));
            }
            return listToReturn;
        }
    }
}