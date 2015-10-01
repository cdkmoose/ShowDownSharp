using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.Engines
{
	public enum Base
	{
		First,
		Second,
		Third,
		Home
	}

	public class BaseRunnerUpdateArgs : EventArgs
	{
		private Base updateBase;
		private string runnerName;

		public Base UpdateBase
		{
			get
			{
				return updateBase;
			}

			set
			{
				updateBase = value;
			}
		}

		public string RunnerName
		{
			get
			{
				return runnerName;
			}

			set
			{
				runnerName = value;
			}
		}
	}
}
