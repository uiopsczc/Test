from openpyxl import Workbook, load_workbook

class ExcelUtil(object):
  # https://openpyxl.readthedocs.io/en/stable/
  @staticmethod
  def ReadExcelAsLineList(file_path, start_row=1, sheet_index=0):# row_num base on 1
    wrokbook = load_workbook(file_path, data_only=True)
    sheet = wrokbook.worksheets[sheet_index]
    max_row = sheet.max_row
    max_column = sheet.max_column
    line_list = []
    for row in range(1, max_row + 1):  # 行
      if row < start_row:
        continue
      line = []
      for column in range(1, max_column + 1):  # 列
        value = sheet.cell(row=row, column=column).value
        line.append(value)
      if line[0] and line[0] != "":
        line_list.append(line)
    return line_list

  @staticmethod
  def WriteExcelFromLineList(file_path, line_list, start_row=1, sheet_index=0):
    workbook = Workbook()
    sheet = workbook.worksheets[sheet_index]
    row = start_row
    for line in line_list:
      column = 1
      for field in line:
        sheet.cell(row=row, column=column).value = field
        column += 1
      row += 1
    workbook.save(file_path)

  #删除空的，没有数据的行
  @staticmethod
  def ClearExcelEmptyRows(file_path, start_row=1, sheet_index=0):
    workbook = load_workbook(file_path, data_only=True)  # data_only,读取公式的结果，而不是公式本身
    sheet = workbook.worksheets[sheet_index]
    max_row = sheet.max_row
    max_column = sheet.max_column
    for row in range(max_row + 1, 0, -1):  # 行
      if row < start_row:
        continue
      has_data = False
      for column in list(range(1, max_column + 1)):  # 行
        if sheet.cell(row, column).value != None:
          has_data = True
          break
      if has_data == False:
        sheet.delete_rows(row)
    workbook.save(file_path)
