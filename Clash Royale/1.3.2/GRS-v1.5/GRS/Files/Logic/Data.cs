using System;
using System.Collections.Generic;
using System.Reflection;
using CRS.Files.CSV;

namespace CRS.Files.Logic
{
	// Token: 0x0200000C RID: 12
	internal class Data
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public Data(CSVRow row, DataTable dt)
		{
			this.m_vCSVRow = row;
			this.m_vDataTable = dt;
			this.m_vGlobalID = GlobalID.CreateGlobalID(dt.GetTableIndex() + 1, dt.GetItemCount());
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public int GetDataType()
		{
			return this.m_vDataTable.GetTableIndex();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AE1 File Offset: 0x00000CE1
		public int GetGlobalID()
		{
			return this.m_vGlobalID;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AE9 File Offset: 0x00000CE9
		public int GetInstanceID()
		{
			return GlobalID.GetInstanceID(this.m_vGlobalID);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AF6 File Offset: 0x00000CF6
		public string GetName()
		{
			return this.m_vCSVRow.GetName();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B04 File Offset: 0x00000D04
		public static void LoadData(Data obj, Type objectType, CSVRow row)
		{
			foreach (PropertyInfo propertyInfo in objectType.GetProperties())
			{
				if (propertyInfo.PropertyType.IsGenericType)
				{
					Type typeFromHandle = typeof(List<>);
					Type[] genericArguments = propertyInfo.PropertyType.GetGenericArguments();
					Type type = typeFromHandle.MakeGenericType(genericArguments);
					object obj2 = Activator.CreateInstance(type);
					MethodInfo method = type.GetMethod("Add");
					string memberName = ((DefaultMemberAttribute)obj2.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), true)[0]).MemberName;
					PropertyInfo property = obj2.GetType().GetProperty(memberName);
					for (int j = row.GetRowOffset(); j < row.GetRowOffset() + row.GetArraySize(propertyInfo.Name); j++)
					{
						string text = row.GetValue(propertyInfo.Name, j - row.GetRowOffset());
						if (text == string.Empty && j != row.GetRowOffset())
						{
							text = property.GetValue(obj2, new object[] { j - row.GetRowOffset() - 1 }).ToString();
						}
						if (text == string.Empty)
						{
							object obj3 = (genericArguments[0].IsValueType ? Activator.CreateInstance(genericArguments[0]) : string.Empty);
							method.Invoke(obj2, new object[] { obj3 });
						}
						else
						{
							method.Invoke(obj2, new object[] { Convert.ChangeType(text, genericArguments[0]) });
						}
					}
					propertyInfo.SetValue(obj, obj2);
				}
				else
				{
					propertyInfo.SetValue(obj, (row.GetValue(propertyInfo.Name, 0) == string.Empty) ? null : Convert.ChangeType(row.GetValue(propertyInfo.Name, 0), propertyInfo.PropertyType), null);
				}
			}
		}

		// Token: 0x0400000F RID: 15
		private readonly int m_vGlobalID;

		// Token: 0x04000010 RID: 16
		protected CSVRow m_vCSVRow;

		// Token: 0x04000011 RID: 17
		protected DataTable m_vDataTable;
	}
}
