/***************************************************************************
 *                             SaveStrategy.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id$
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using Server;

namespace Server
{
	public abstract class SaveStrategy
	{
		public static SaveStrategy Acquire()
		{
			if (Core.MultiProcessor)
			{
				int processorCount = Core.ProcessorCount;

				//TODO: Upon realworld verification of DynamicSaveStrategy, change this method around.
				if (processorCount > 16)
				{
					return new DynamicSaveStrategy();
				}
				else
				{
					return new DualSaveStrategy();
				}
			}
			else
			{
				return new StandardSaveStrategy();
			}
		}

		public abstract string Name { get; }
		public abstract void Save(SaveMetrics metrics, bool permitBackgroundWrite);

		public abstract void ProcessDecay();
	}
}