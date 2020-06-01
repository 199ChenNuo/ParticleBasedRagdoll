using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;

public class ExcelLoger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateExcel(string fileName)
    {
        string outPutDir = Application.dataPath + "\\" + fileName + ".xlsx";
        Debug.Log(outPutDir);
        FileInfo newFile = new FileInfo(outPutDir);
        if (newFile.Exists)
        {
            newFile.Delete();  // ensures we create a new workbook   
            Debug.Log("删除表");
            newFile = new FileInfo(outPutDir);
        }
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
            worksheet.Cells[1, 1].Value = "序号";
            worksheet.Cells[1, 2].Value = "时间";
            worksheet.Cells[1, 3].Value = "位置";
            worksheet.Cells[2, 1].Value = 1;
            package.Save();
            Debug.Log("导出Excel成功");
        }
    }


    public FileInfo getFile(string filename)
    {
        string outPutDir = Application.dataPath + "\\" + filename + ".xlsx";
        FileInfo newFile = new FileInfo(outPutDir);
        return newFile;
    }

    public void addCol(string filename, int index, string name)
    {
        FileInfo file = getFile(filename);
        addCol(file, index, name);
    }
    private void addCol(FileInfo file, int index, string name)
    {
        ExcelPackage package = new ExcelPackage(file);
        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
        sheet.Cells[1, index].Value = name;
        package.Save();
    }

    public void writeValue(string filename, int row, int col, float value)
    {
        FileInfo file = getFile(filename);
        writeValue(file, row, col, value);
    }
    public void writeValue(FileInfo file, int row, int col, float value)
    {
        ExcelPackage package = new ExcelPackage(file);
        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
        sheet.Cells[row, col].Value = value;
        package.Save();
    }

    public void writeValue(string filename, int row, int col, int value)
    {
        FileInfo file = getFile(filename);
        writeValue(file, row, col, value);
    }
    public void writeValue(FileInfo file, int row, int col, int value)
    {
        ExcelPackage package = new ExcelPackage(file);
        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
        sheet.Cells[row, col].Value = value;
        package.Save();
    }

    public void writeValue(string filename, int row, int col, string value)
    {
        FileInfo file = getFile(filename);
        writeValue(file, row, col, value);
    }
    public void writeValue(FileInfo file, int row, int col, string value)
    {
        ExcelPackage package = new ExcelPackage(file);
        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
        sheet.Cells[row, col].Value = value;
        package.Save();
    }

    public void writeValue(string filename, int row, int col, double value)
    {
        FileInfo file = getFile(filename);
        writeValue(file, row, col, value);
    }
    public void writeValue(FileInfo file, int row, int col, double value)
    {
        ExcelPackage package = new ExcelPackage(file);
        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
        sheet.Cells[row, col].Value = value;
        package.Save();
    }
}
