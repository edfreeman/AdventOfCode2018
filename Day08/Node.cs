using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public class Node
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int NumberOfSubtrees { get; set; }
        public List<int> ChildIds { get; set; }
        public int NumberOfMetadataEntries { get; set; }
        public List<int> MetadataEntries { get; set; }
    }
}
