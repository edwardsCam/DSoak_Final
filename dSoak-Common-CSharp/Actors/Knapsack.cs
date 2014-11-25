using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The knapsack holds resources

namespace Actors
{
	public class Knapsack
	{

		#region Public Properties

		public List<SharedObjects.Penny> pennies { get; set; }
		public SharedObjects.Balloon balloon { get; set; }
		public SharedObjects.Umbrella umbrella { get; set; }

		#endregion

		#region Constructor

		public Knapsack()
		{
			pennies = new List<SharedObjects.Penny>();
			balloon = null;
			umbrella = null;
		}

		#endregion

		#region Public Methods

		public void clear()
		{
			pennies = null;
			balloon = null;
			umbrella = null;
		}

		#endregion
	}
}
