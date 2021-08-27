using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirectoryReader
{
    class HTMLFile
    {
        public string html { get; private set; }

        public HTMLFile() { ADDTable(); }
        public HTMLFile(List<string> colunms) { ADDTable(colunms); }
        public void ADDTable()
        {
            if (html != "") html += "</table>";
            html += "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:arial'>";
        }
        public void ADDTable(List<string> colunms)
        {
            if (html != "") html += "</table>";
            html += "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:arial'>";
            ADDColunms(colunms);
        }
        public void ADDColunms(List<string> colunms)
        {
            html += "<tr>";
            foreach (string colunmname in colunms)
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + colunmname + "</th>";
            html += "</tr>";
        }
        public void ADDRow(List<string> row)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr>");
            foreach (string cell in row)
                builder.Append("<td style='width:120px;border: 1px solid #ccc'>" + cell + "</td>");
            builder.Append("</tr>");
            html += builder;
        }
        public void ADDRows(List<objDirFile> rows)
        {
            StringBuilder builder = new StringBuilder();
            foreach (objDirFile row in rows)
            {
                builder.Append("<tr>");
                foreach (string cell in row.InfoList())
                    builder.Append("<td style='width:120px;border: 1px solid #ccc'>" + cell + "</td>");
                builder.Append("</tr>");
            }
            html += builder;
        }
        public void SaveFile(string pathsave, string filename)
        {
            html += "</table>";
            File.WriteAllText(pathsave + "\\" + filename + ".html", html);
        }
    }
}
