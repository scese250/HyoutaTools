﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HyoutaTools.Tales.Vesperia.T8BTEMST {
	public class T8BTEMST {
		public T8BTEMST( String filename, Util.Endianness endian, Util.Bitness bits ) {
			using ( Stream stream = new System.IO.FileStream( filename, FileMode.Open, System.IO.FileAccess.Read ) ) {
				if ( !LoadFile( stream, endian, bits ) ) {
					throw new Exception( "Loading T8BTEMST failed!" );
				}
			}
		}

		public T8BTEMST( Stream stream, Util.Endianness endian, Util.Bitness bits ) {
			if ( !LoadFile( stream, endian, bits ) ) {
				throw new Exception( "Loading T8BTEMST failed!" );
			}
		}

		public List<Enemy> EnemyList;
		public Dictionary<uint, Enemy> EnemyIdDict;

		private bool LoadFile( Stream stream, Util.Endianness endian, Util.Bitness bits ) {
			string magic = stream.ReadAscii( 8 );
			if ( magic != "T8BTEMST" ) {
				throw new Exception( "Invalid magic." );
			}
			uint enemyCount = stream.ReadUInt32().FromEndian( endian );
			uint refStringStart = stream.ReadUInt32().FromEndian( endian );

			EnemyList = new List<Enemy>( (int)enemyCount );
			for ( uint i = 0; i < enemyCount; ++i ) {
				Enemy s = new Enemy( stream, refStringStart, endian, bits );
				EnemyList.Add( s );
			}

			EnemyIdDict = new Dictionary<uint, Enemy>( EnemyList.Count );
			foreach ( Enemy e in EnemyList ) {
				EnemyIdDict.Add( e.InGameID, e );
			}

			return true;
		}
	}
}
