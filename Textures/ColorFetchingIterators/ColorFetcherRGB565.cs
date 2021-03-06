﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyoutaTools.Textures.ColorFetchingIterators {
	public class ColorFetcherRGB565 : IColorFetchingIterator {
		private Stream SourceStream;
		private long PixelCount;

		public ColorFetcherRGB565( Stream stream, long width, long height ) {
			SourceStream = stream;
			PixelCount = width * height;
		}

		public IEnumerator<Color> GetEnumerator() {
			for ( long i = 0; i < PixelCount; ++i ) {
				yield return ColorFromRGB565( SourceStream.ReadUInt16() );
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public static Color ColorFromRGB565( ushort color ) {
			int b = (int)( ( ( color & 0x001F )       ) << 3 );
			int g = (int)( ( ( color & 0x07E0 ) >>  5 ) << 2 );
			int r = (int)( ( ( color & 0xF800 ) >> 11 ) << 3 );
			return Color.FromArgb( r, g, b );
		}
	}
}
