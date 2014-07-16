﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyoutaTools.Tales.Vesperia.T8BTEMST {
	public class Enemy {
		public uint[] Data;
		public float[] DataFloat;

		public uint NameStringDicID;
		public uint InGameID;
		public string RefString;

		public uint IconID;

		public Enemy( System.IO.Stream stream, uint refStringStart ) {
			uint entryLength = stream.ReadUInt32().SwapEndian();
			Data = new uint[entryLength / 4];
			DataFloat = new float[entryLength / 4];
			Data[0] = entryLength;

			for ( int i = 1; i < Data.Length; ++i ) {
				Data[i] = stream.ReadUInt32().SwapEndian();
				DataFloat[i] = Data[i].UIntToFloat();
			}

			NameStringDicID = Data[2];
			InGameID = Data[5];
			IconID = Data[57];
			
			uint refStringLocation = Data[6];
			long pos = stream.Position;
			stream.Position = refStringStart + refStringLocation;
			RefString = stream.ReadAsciiNullterm();
			stream.Position = pos;
		}

		public override string ToString() {
			return RefString;
		}
	}
}
