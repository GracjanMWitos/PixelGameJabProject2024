using System.Collections.Generic;

public interface GraphNode
{
    struct ConnectionInfo
    {
        public GridTile tile;
        public float Distance;
    }
    IEnumerable<ConnectionInfo> GetConnections();
}