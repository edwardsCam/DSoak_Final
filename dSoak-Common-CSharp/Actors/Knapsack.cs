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
		public List<SharedObjects.Balloon> balloons { get; set; }
		public List<SharedObjects.Umbrella> umbrellas { get; set; }

		#endregion

		#region Constructor

		public Knapsack()
		{
			pennies = new List<SharedObjects.Penny>();
			balloons = new List<SharedObjects.Balloon>();
			umbrellas = new List<SharedObjects.Umbrella>();
		}

		#endregion

		#region Public Methods

		public short numFilledBalloons()
		{
			short count = 0;
			foreach (SharedObjects.Balloon b in balloons.ToList())
				if (b.IsValid && b.UnitsOfWater > 0)
					count++;
			return count;
		}

		public short numUnraisedUmbrellas()
		{
			short count = 0;
			foreach (SharedObjects.Umbrella u in umbrellas)
				if (u.IsValid)
					count++;
			return (short)(umbrellas.Count() - (int)count);
		}

		public void clear()
		{
			pennies = null;
			balloons = null;
			umbrellas = null;
		}

		#endregion
	}
}
