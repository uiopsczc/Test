using System;
using System.Collections.Generic;

namespace CsCat
{
	public class AutoGenLineInfoUtil
	{
		public static List<AutoGenLineInfo> ToAutoGenLineInfoList(List<string> lineList, string cfgPartStartsWith)
		{
			List<AutoGenLineInfo> autoGenLineInfoList = new List<AutoGenLineInfo>();
			for (int i = 0; i < lineList.Count; i++)
			{
				var line = lineList[i];
				var (mainPart, cfgPart) = line.GetLineAutoGenLineInfoParts(cfgPartStartsWith);
				autoGenLineInfoList.Add(new AutoGenLineInfo(mainPart, cfgPart, cfgPartStartsWith));
			}
			return autoGenLineInfoList;
		}

		public static int IndexOf(List<AutoGenLineInfo> autoGenLineInfoList, AutoGenLineInfo matchAutoGenLineInfo, int startCheckIndex)
		{
			for (int index = startCheckIndex; index < autoGenLineInfoList.Count; index++)
			{
				var autoGenLineInfo = autoGenLineInfoList[index];
				if (autoGenLineInfo.IsMatch(matchAutoGenLineInfo))
					return index;
			}
			return -1;
		}

		public static List<string> ToLineList(List<AutoGenLineInfo> autoGenLineInfoList)
		{
			List<string> lineList = new List<string>();
			for (int index = 0; index < autoGenLineInfoList.Count; index++)
			{
				var autoGenLineInfo = autoGenLineInfoList[index];
				string lineContent = autoGenLineInfo.ToLine();
				lineList.Add(lineContent);
			}
			return lineList;
		}
		
		public static int CheckInsert(AutoGenLineInfo toInsertLineInfo, List<AutoGenLineInfo> lineInfoList, int startCheckIndex)
		{
			int insertLineIndex = IndexOf(lineInfoList, toInsertLineInfo, startCheckIndex);
			//如果lineList中没有content的内容的行，则直接插入
			if (insertLineIndex < 0)
			{
				lineInfoList.Insert(startCheckIndex, toInsertLineInfo);
				insertLineIndex = startCheckIndex;
			}
			return insertLineIndex;
		}

		public static int CheckInsert(List<AutoGenLineInfo> toInsertLineInfoList, List<AutoGenLineInfo> lineInfoList,
			int startCheckIndex)
		{
			if (toInsertLineInfoList.Count == 1)
				return CheckInsert(toInsertLineInfoList[0], lineInfoList, startCheckIndex);
			//			var lastLineInsertIndex = startCheckIndex;
			//			for (int i = 0; i < toInsertLineInfoList.Count; i++)
			//			{
			//				var toInsertLineInfo = toInsertLineInfoList[i];
			//				startCheckIndex = CheckInsert(toInsertLineInfo, orgLineInfoList, startCheckIndex) + 1;
			//				if (i == toInsertLineInfoList.Count - 1) //最后一次的时候，startCheckIndex - 1才是最后一行插入的lineIndex
			//					lastLineInsertIndex = startCheckIndex - 1;
			//			}
			//			return lastLineInsertIndex;
			//			var startInsertIndex = IndexOf(orgLineInfoList, toInsertLineInfoList[0], startCheckIndex);
			//			startInsertIndex = startInsertIndex == -1 ? startCheckIndex : startInsertIndex;

//			var lastLineInsertIndex = startCheckIndex;
			int startIndex = IndexOf(lineInfoList, toInsertLineInfoList[0], startCheckIndex);
			if (startIndex == -1)
			{
				startIndex = startCheckIndex;
				lineInfoList.Insert(startIndex, toInsertLineInfoList[0]);
			}

			int endIndex = IndexOf(lineInfoList, toInsertLineInfoList[toInsertLineInfoList.Count - 1],
				startIndex + 1);
			if (endIndex == -1)
			{
				int nextInsertIndex = startIndex + 1;
				for (int i = 1; i < toInsertLineInfoList.Count; i++)
				{
					var toInsertLineInfo = toInsertLineInfoList[i];
					var insertAtIndex = IndexOf(lineInfoList, toInsertLineInfo, startCheckIndex);
					if (insertAtIndex != -1)
					{
						if (insertAtIndex + 1 > nextInsertIndex)
							nextInsertIndex = insertAtIndex + 1;
						continue;
					}
					lineInfoList.Insert(nextInsertIndex, toInsertLineInfo);
					endIndex = nextInsertIndex;
					nextInsertIndex++;
				}

			}
			else
			{
				int nextInsertIndex = startIndex + 1;
				for (int i = 1; i < toInsertLineInfoList.Count; i++)
				{
					var toInsertLineInfo = toInsertLineInfoList[i];
					var insertAtIndex = IndexOf(lineInfoList, toInsertLineInfo, startCheckIndex);
					if (insertAtIndex != -1 && insertAtIndex <= endIndex)
					{
						if (insertAtIndex + 1 > nextInsertIndex)
							nextInsertIndex = insertAtIndex + 1;
						continue;
					}

					lineInfoList.Insert(nextInsertIndex, toInsertLineInfo);
					nextInsertIndex++;
					endIndex++;
				}
			}

			for (int i = endIndex; i >= startIndex; i--)
			{
				var lineInfo = lineInfoList[i];
				if (lineInfo.IsDeleteIfNotExist() && IndexOf(toInsertLineInfoList,lineInfo, 0) == -1)
				{
					lineInfoList.RemoveAt(i);
					endIndex--;
				}
			}
			return endIndex;
		}

		
}
}