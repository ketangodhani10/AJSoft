using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
    public class CommonBL
    {
        public bool isNewEntry(int entityId)
        {
            if (entityId <= 0)
                return true;
            else
                return false;
        }

        public bool isNewEntry(Guid? entityId)
        {
            if (entityId == Guid.Empty)
                return true;
            else
                return false;
        }


        //Check if given path directories exists, if not then create it
        public void EnsureDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete all files under directory
        public void EnsureEmptyDirectory(string path)
        {
            try
            {
                Array.ForEach(Directory.GetFiles(path), System.IO.File.Delete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete Directory and all sub directory
        public void DeleteDirectory(string path, bool subDirectories = false)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, subDirectories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // If File with same name already exists then appends #no to file name
        public string GetUniqueDocumentName(string fileName, string dirPath)
        {
            try
            {
                bool isFileExists = true;
                int i = 0;
                string filepath = Path.Combine(dirPath, fileName);
                while (isFileExists)
                {
                    if (File.Exists(filepath))
                    {
                        i += 1;
                        filepath = Path.Combine(dirPath, Path.GetFileNameWithoutExtension(fileName)) + "_" + i.ToString() + Path.GetExtension(fileName);
                    }
                    else
                        isFileExists = false;

                }

                return filepath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CreateCSVTextFile<T>(List<T> data, string seperator = ",")
        {
            var properties = typeof(T).GetProperties();
            var result = new StringBuilder();

            var headers = string.Join(seperator, properties.Select(p => p.Name));
            result.AppendLine(headers);

            foreach (var row in data)
            {
                var values = properties.Select(p => p.GetValue(row, null)).Select(v => StringToCSVCell(Convert.ToString(v)));
                var line = string.Join(seperator, values);
                result.AppendLine(line);
            }

            return result.ToString();
        }

        private static string StringToCSVCell(string str)
        {
            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }

        public User CurrentUser;
        public void SetCurrentUser(string Email)
        {
            CurrentUser = new UserBL().GetByEmail(Email);
        }

    }
}
