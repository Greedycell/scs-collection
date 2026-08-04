using System;
using System.Collections.Generic;
using System.Reflection;
using UCS.Logic;

namespace UCS.Files
{
	// Token: 0x0200002E RID: 46
	internal class Data
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000D13D File Offset: 0x0000B33D
		public Data(CSVRow row, DataTable dt)
		{
			this.m_vCSVRow = row;
			this.m_vDataTable = dt;
			this.m_vGlobalID = GlobalID.CreateGlobalID(dt.GetTableIndex() + 1, dt.GetItemCount());
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000D16C File Offset: 0x0000B36C
		public int GetDataType()
		{
			return this.m_vDataTable.GetTableIndex();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000D179 File Offset: 0x0000B379
		public int GetGlobalID()
		{
			return this.m_vGlobalID;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000D181 File Offset: 0x0000B381
		public int GetInstanceID()
		{
			return GlobalID.GetInstanceID(this.m_vGlobalID);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000D18E File Offset: 0x0000B38E
		public string GetName()
		{
			return this.m_vCSVRow.GetName();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000D19C File Offset: 0x0000B39C
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
				else if (row.GetValue(propertyInfo.Name, 0) == string.Empty)
				{
					propertyInfo.SetValue(obj, null, null);
				}
				else
				{
					propertyInfo.SetValue(obj, Convert.ChangeType(row.GetValue(propertyInfo.Name, 0), propertyInfo.PropertyType), null);
				}
			}
		}

		// Token: 0x04000168 RID: 360
		private readonly int m_vGlobalID;

		// Token: 0x04000169 RID: 361
		protected CSVRow m_vCSVRow;

		// Token: 0x0400016A RID: 362
		protected DataTable m_vDataTable;
	}
}
