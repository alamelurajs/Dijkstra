using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Dijkstra
{
    #region
  
    class Node
    {
        // Each node will have a name and a list of neighbour node and its distance
        private string Name;
        private Dictionary<Node, int> Neighbors;

        public Node(string NodeName)
        {
            this.Name = NodeName;
            Neighbors = new Dictionary<Node, int>();
        }


        public void AddNeighbour(Node n, int dist)
        {
            Neighbors.Add(n, dist);
        }

        public string getName()
        {
            return Name;
        }

        public Dictionary<Node, int> getNeighbors()
        {
            return Neighbors;
        }
    }
    class Graph
    {
        // a graph is a list of nodes 
        private List<Node> Nodes;

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public void Add(Node n)
        {
            Nodes.Add(n);
        }

        public void Remove(Node n)
        {
            Nodes.Remove(n);
        }

        public List<Node> GetNodes()
        {
            return Nodes.ToList();
        }

        public int getCount()
        {
            return Nodes.Count;
        }
    }
   public  class ShortestPathData
    {
        //Result data class which would have the list of node names and the total distance
        public List<string> NodeNames { get; set; }
        public int totalDistance { get; }

        public ShortestPathData(List<string> nodeNames, int totalDistance)
        {
            NodeNames = nodeNames;
            this.totalDistance = totalDistance;
        }
    }
    class DistanceCalculator
    {
        Dictionary<Node, int> Distances;
        Dictionary<Node, Node> Routes;
        Graph graph;
        List<Node> AllNodes;

        public DistanceCalculator(Graph g)
        {
            this.graph = g;
            this.AllNodes = g.GetNodes();
           Distances = SetDistances();
           Routes = SetRoutes();
        }
        private Dictionary<Node, int> SetDistances()
        {
            Dictionary<Node, int> Distances = new Dictionary<Node, int>();

            foreach (Node n in graph.GetNodes())
            {
                Distances.Add(n, int.MaxValue);
            }
            return Distances;
        }

        private Dictionary<Node, Node> SetRoutes()
        {
            Dictionary<Node, Node> Routes = new Dictionary<Node, Node>();

            foreach (Node n in graph.GetNodes())
            {
                Routes.Add(n, null);
            }
            return Routes;
        }
        public ShortestPathData ShortestPath(Node Source, Node Destination, Graph graphNodes)
        {
            Distances[Source] = 0;

            while (AllNodes.ToList().Count != 0)
            {
                Node LeastDistanceNode = getLeastDistanceNode();
                ExamineConnections(LeastDistanceNode);
                AllNodes.Remove(LeastDistanceNode);
            }           
            List<string> nodeNamesToDestination = GetNodeNames(Destination);
            ShortestPathData shortestPathData = new ShortestPathData(nodeNamesToDestination, Distances[Destination]);
            return shortestPathData;
        }
      
        private void ExamineConnections(Node n)
        {
            foreach (var neighbor in n.getNeighbors())
            {
                if (Distances[n] + neighbor.Value < Distances[neighbor.Key])
                {
                    Distances[neighbor.Key] = neighbor.Value + Distances[n];
                    Routes[neighbor.Key] = n;
                }
            }
        }

        private Node getLeastDistanceNode()
        {
            Node LeastDistance = AllNodes.FirstOrDefault();

            foreach (var n in AllNodes)
            {
                if (Distances[n] < Distances[LeastDistance])
                    LeastDistance = n;
            }

            return LeastDistance;
        }
        

        public List<string> GetNodeNames(Node d)
        {
            var routeNames = new List<string>();
            routeNames.Add(d.getName());           
            if (Routes[d] != null)
                routeNames.AddRange(GetNodeNames(Routes[d]));
            return routeNames;
        }
    }

   
    #endregion

    class Program
    {

        public static void Main(string[] args)
        {
            // initialising Data as per the graph
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
            //change the from and to below
            ShortestPathData result =  dis.ShortestPath(a, d,graph);
            Console.WriteLine("Shortest distance from {0} to {1} is {2}",a.getName(),d.getName(),result.totalDistance);
            result.NodeNames.Reverse();
            Console.WriteLine("Shortest route is : " + string.Join(", ", (result.NodeNames))) ;
        }
    }
}
