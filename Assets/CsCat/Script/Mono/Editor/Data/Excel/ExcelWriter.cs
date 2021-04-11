using UnityEngine;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace CsCat
{
  //有问题，用的时候需要修改测试
  public class ExcelWriter
  {
    public static void Save()
    {
      IWorkbook book;
      using (FileStream file = new FileStream(Application.dataPath + "/Test.xlsx", FileMode.Open, FileAccess.ReadWrite))
      {
        book = new HSSFWorkbook(file);
        ISheet sheet = book.GetSheetAt(0);
        //book.GetSheetAt(0).GetRow(0).GetCell(0).SetCellType(CellType.String);
        //book.GetSheetAt(0).GetRow(0).GetCell(0).SetCellValue("权");
        IRow eRow = sheet.CreateRow(sheet.LastRowNum + 1);
        eRow.CreateCell(1).SetCellValue("kkk");

        Debug.LogWarning(sheet.LastRowNum);

        file.Close();
      }

      using (MemoryStream ms = new MemoryStream())
      {
        book.Write(ms); // 保存HSSFWorkbook到数据流中

        // 写入excel文件
        using (FileStream fs = new FileStream(Application.dataPath + "/Test.xlsx", FileMode.OpenOrCreate,
          FileAccess.ReadWrite))
        {
          byte[] data = ms.ToArray();
          fs.Write(data, 0, data.Length);
          fs.Flush();
          fs.Close();
        }

        book = null;
      }
    }

    public static void Write(string inputPath, string sheetName, string id, string columnName, string value,
      int dataStartRowNum, int headerRowNum)
    {
      if (inputPath.Contains("__bak__"))
      {
        return;
      }

      FileInfo fileInfo = new FileInfo(inputPath);
      if (!fileInfo.Exists)
      {
        Debug.LogErrorFormat("file: {0} not exist!", inputPath);
        return;
      }

      using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
      {
        IWorkbook workbook = null;
        if (".xlsx".Equals(fileInfo.Extension))
          workbook = new HSSFWorkbook(fileStream);


        if (workbook != null)
        {
          for (int i = 0; i < workbook.NumberOfSheets; i++)
          {
            ISheet sheetAt = workbook.GetSheetAt(i);

            string _sheetName = sheetAt.SheetName;
            if (_sheetName.Equals(sheetName))
            {
              Write(sheetAt, id, columnName, value, dataStartRowNum, headerRowNum);
              break;
            }
          }

          fileStream.Close();
        }

        if (workbook != null)
        {
          using (MemoryStream ms = new MemoryStream())
          {
            workbook.Write(ms); // 保存HSSFWorkbook到数据流中
            // 写入excel文件
            using (FileStream fs = new FileStream(inputPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
              byte[] data = ms.ToArray();
              fs.Write(data, 0, data.Length);
              fs.Flush();
              fs.Close();
            }

            workbook = null;
          }
        }
      }
    }

    public static void Write(ISheet sheet, string id, string columnName, string value, int dataStartRowNum,
      int headerRowNum)
    {
      List<string> headerColumNames = ExcelUtil.GetSheetHeaderNames(sheet, headerRowNum);
      for (int curRowNum = dataStartRowNum; curRowNum <= sheet.LastRowNum; curRowNum++)
      {
        IRow curRow = sheet.GetRow(curRowNum);
        ICell idCell = curRow.GetCell(0);
        if (ExcelUtil.GetCellValue(idCell).Equals(id))
        {
          for (int curCellNum = 0; curCellNum < curRow.LastCellNum; curCellNum++)
          {
            ICell icell = curRow.GetCell(curCellNum);
            if (icell == null)
            {
              icell = curRow.CreateCell(curCellNum);
            }

            if (headerColumNames[curCellNum].Equals(columnName))
            {
              icell.SetCellValue(value);
              return;
            }
          }
        }
      }



      //否则新建一行输入
      IRow eRow = sheet.CreateRow(sheet.LastRowNum + 1);
      for (int curCellNum = 0; curCellNum < headerColumNames.Count; curCellNum++)
      {
        ICell icell = eRow.CreateCell(curCellNum);

        if (headerColumNames[curCellNum].ToLower() == ExcelConst.Id_Text)
        {
          icell.SetCellValue(id);
        }
        else if (headerColumNames[curCellNum] == columnName)
        {
          icell.SetCellValue(value);
        }
      }
    }
  }


}