using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


public class XmlHelper
{
    private const string _filePath = "D:\\PlayerScoreTable.xml";


    public static XmlDocument ReadXml(string xmlPath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(xmlPath))
        {
            xmlDoc.Load(xmlPath);
        }
        else
        {
            XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "GB2312", null);
            xmlDoc.AppendChild(xmlDec);
            var root = xmlDoc.CreateElement("ScoreTable");
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlPath);
        }
        return xmlDoc;
    }


    public static void WriteXml(string xmlPath, List<ScoreRow> scoreRows)
    {
        var xmlDoc = ReadXml(xmlPath);
        var root = xmlDoc.DocumentElement;

        if (root == null)
        {
            root = xmlDoc.CreateElement("ScoreTable");
            xmlDoc.AppendChild(root);
        }

        for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
        {
            root.RemoveChild(root.ChildNodes[i]);
        }

        foreach (var scoreRow in scoreRows)
        {
            var scoreRowElement = AddXmlElement(xmlDoc, root, "ScoreRow", "");
            AddXmlElement(xmlDoc, scoreRowElement, nameof(scoreRow.Id), scoreRow.Id.ToString());
            AddXmlElement(xmlDoc, scoreRowElement, nameof(scoreRow.PlayerName), scoreRow.PlayerName);
            AddXmlElement(xmlDoc, scoreRowElement, nameof(scoreRow.Score), scoreRow.Score.ToString());
            AddXmlElement(xmlDoc, scoreRowElement, nameof(scoreRow.PlayTime), scoreRow.PlayTime.ToString());
        }

        xmlDoc.Save(xmlPath);
    }


    public static XmlElement AddXmlElement(XmlDocument xmlDoc, XmlElement xmlElement, string elementName, string elementValue)
    {
        var childElement = xmlDoc.CreateElement(elementName);
        childElement.InnerText = elementValue;
        xmlElement.AppendChild(childElement);
        return childElement;
    }


    public static List<ScoreRow> LoadScoreTable()
    {
        var scoreTable = new List<ScoreRow>();
        var xmlDoc = ReadXml(_filePath);
        var scoreRowNodes = xmlDoc.GetElementsByTagName("ScoreRow");

        foreach (XmlNode scoreRowNode in scoreRowNodes)
        {
            var scoreRow = new ScoreRow();
            var fuck = scoreRowNode.SelectSingleNode(nameof(scoreRow.Id)).InnerText;
            scoreRow.Id = int.Parse(scoreRowNode.SelectSingleNode(nameof(scoreRow.Id)).InnerText);
            scoreRow.PlayerName = scoreRowNode.SelectSingleNode(nameof(scoreRow.PlayerName)).InnerText;
            scoreRow.Score = int.Parse(scoreRowNode.SelectSingleNode(nameof(scoreRow.Score)).InnerText);
            scoreRow.PlayTime = DateTime.Parse(scoreRowNode.SelectSingleNode(nameof(scoreRow.PlayTime)).InnerText);
            scoreTable.Add(scoreRow);
        }

        return scoreTable;
    }


    public static void SaveScoreTable(List<ScoreRow> scoreTable)
    {
        WriteXml(_filePath, scoreTable);
    }
}