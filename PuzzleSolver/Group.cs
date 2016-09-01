using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolver
{
    class Group
    {
        private List<Block> _groupMembers;
        private int _id;

        public List<Block> Members
        {
            get { return _groupMembers; }
        }

        public Group(int id)
        {
            _id = id;
            _groupMembers = new List<Block>();
        }

        public void Add(Block block)
        {
            _groupMembers.Add(block);
        }
    }
}
