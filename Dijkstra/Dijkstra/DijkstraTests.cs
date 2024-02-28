
using Xunit;
using Dijkstra;


namespace DijkstraTests
{
    public class DijkstraTests
    {
        [Fact]
        public void FindShortestPathFromAtoD_ReturnsExpectedResult()
        {
            // Arrange
            Graph graph = new Graph();

            Node a = new Node("A");
            Node b = new Node("B");
            Node c = new Node("C");
            Node d = new Node("D");
            Node e = new Node("E");
            Node f = new Node("F");
            Node g = new Node("G");
            Node h = new Node("H");
            Node i = new Node("I");

            graph.Add(a);
            graph.Add(b);
            graph.Add(c);
            graph.Add(d);
            graph.Add(e);
            graph.Add(f);
            graph.Add(g);
            graph.Add(h);
            graph.Add(i);

            a.AddNeighbour(b, 4);
            a.AddNeighbour(c, 6);
            b.AddNeighbour(a, 4);
            b.AddNeighbour(f, 2);

            c.AddNeighbour(d, 8);
            c.AddNeighbour(a, 6);

            d.AddNeighbour(c, 8);
            d.AddNeighbour(e, 4);
            d.AddNeighbour(g, 1);

            e.AddNeighbour(b, 2);
            e.AddNeighbour(f, 3);
            e.AddNeighbour(i, 8);
            e.AddNeighbour(d, 4);

            f.AddNeighbour(b, 2);
            f.AddNeighbour(e, 3);
            f.AddNeighbour(g, 4);
            f.AddNeighbour(h, 6);

            g.AddNeighbour(f, 4);
            g.AddNeighbour(h, 5);
            g.AddNeighbour(d, 1);
            g.AddNeighbour(i, 5);

            h.AddNeighbour(f, 6);
            h.AddNeighbour(g, 5);

            i.AddNeighbour(e, 8);
            i.AddNeighbour(g, 5);

            DistanceCalculator dis = new DistanceCalculator(graph);
            ShortestPathData result = dis.ShortestPath(a, d, graph);
            
           
            int expectedResultTotalDistance = 11;

            // Act
            int actualResult = result.totalDistance;

            // Assert
            Assert.Equal(expectedResultTotalDistance, actualResult);
        }
    }
}
