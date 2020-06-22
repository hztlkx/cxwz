using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Web.UI.WebControls;
using Webdiyer.WebControls.Mvc;
using System.Web.Mvc;

namespace YcTeam.MVCSite
{
    public class PagerHelper
    {
        public static void SetPageIndex<T>(PagedList<T> model,int pageIndex,int pageSize)
        {
            string fieldName = "Index";
            for (int i = 0; i < model.Count; i++)
            {
                var item = model[i];
                //MemberInfo[] members = item.GetType().GetMember(fieldName);

                //[获取属性]GetProperty（）：具有访问器 get/set 方法;
                //[获取字段]GetField（）
                PropertyInfo piIndex = item.GetType().GetProperty(fieldName);//Index属性
                int index = (i + 1) + (pageIndex-1) * pageSize;
                if (piIndex != null)
                {
                    piIndex.SetValue(item, index, null);
                }
            }
        }

        #region 动态创建属性
        /// <summary>
        /// 动态添加属性
        /// </summary>
        public class DynamicModel : DynamicObject
        {
            public string PropertyName { get; set; }

            public Dictionary<string, object> DicProperty  = new Dictionary<string, object>();


            public int Count
            {
                get
                {
                    return DicProperty.Count;
                }
            }

            /// <summary>
            /// 调用动态创建的属性
            /// </summary>
            /// <param name="fieldName"></param>
            /// <param name="value"></param>
            public void GetMember(string fieldName,Object value)
            {
                dynamic dynamicModel = new DynamicModel();
                dynamicModel.PropertyName = fieldName;
                dynamicModel.Property = value;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                string name = binder.Name;
                return DicProperty.TryGetValue(name, out result);
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                if (binder.Name == "Property")
                {
                    DicProperty[binder.Name] = value;
                }
                else
                {
                    DicProperty[PropertyName] = value;
                }

                return true;
            }
        }
        #endregion
    }
}