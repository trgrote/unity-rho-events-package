using UnityEditor;
using System.Reflection;
using System;
using System.Linq;

namespace rho
{
    public class EnumEventEditor<T> : EventEditor<T> where T : System.Enum
    {
        protected T _testParam;

        protected bool _isFlag;

		protected virtual void OnEnable()
		{
			_isFlag = typeof(T).GetCustomAttributes<FlagsAttribute>().Any();
		}

		protected override T GetTestParam()
		{
			return _isFlag ?
				_testParam = (T) EditorGUILayout.EnumFlagsField("Value", _testParam) :
				_testParam = (T) EditorGUILayout.EnumPopup("Value", _testParam)
			;
		}
    }
}