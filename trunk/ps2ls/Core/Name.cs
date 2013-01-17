using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ps2ls.Cryptography;

namespace ps2ls.Core
{
    public struct Name : IComparable<Name>
    {
        private String string_;
        private UInt32 hash;

        public String String { get { return string_; } }
        public UInt32 Hash { get { return hash; } }

        public Name(String string_)
        {
            this.string_ = string_;
            hash = Jenkins.OneAtATime(string_);
        }

        Int32 IComparable<Name>.CompareTo(Name other)
        {
            return hash.CompareTo(other.hash);
        }
    }
}
