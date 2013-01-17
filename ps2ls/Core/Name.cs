using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ps2ls.Cryptography;

namespace ps2ls.Core
{
    public struct Name : IComparable<Name>
    {
        private String _string;
        private UInt32 hash;

        public Name(String _string)
        {
            this._string = _string;
            hash = Jenkins.OneAtATime(_string);
        }

        Int32 IComparable<Name>.CompareTo(Name other)
        {
            return hash.CompareTo(other.hash);
        }
    }
}
