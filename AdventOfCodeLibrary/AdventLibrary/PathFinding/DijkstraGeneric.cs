using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AStarSharp;
using static System.Formats.Asn1.AsnWriter;

namespace AdventLibrary.PathFinding
{
    internal class Score<Tnode, Tedge>
    {
        public Score(
            int distance,
            int index,
            Tnode node,
            Tnode previous,
            Tedge edge)
        {
            Distance = distance;
            Index = index;
            Node = node;
            PreviousNode = previous;
            Edge = edge;
        }

        public int Distance { get; set; }

        // this might become a generic as well
        public int Index { get; set; }

        public Tnode Node { get; set; }

        public Tnode PreviousNode { get; set; }

        public Tedge Edge { get; set; }
    }

    internal class Node { }

    internal class Edge { }

    public class DijkstraGeneric<Tnode, Tedge>()
    {
        /*
        Arguments:
            * node: a hashable object representing the starting node
            * neighbors: a function to compute the neighbors of a node
                (more below).
            * maxitems=10000: optional bound to limit the number
                of computation steps in the case of a very large
                or infinite graph.
            * maxdist=None: optionaly a maximal distance from the initial
                node where to stop the computation.
            * target=None: optional target node. The algorithm stops once
                a shortest path to this target has been found.*/
        public DijkstraGeneric(Tnode node, int maxItems = 10000, int maxDist = 0, Tnode target = default) :this()
        {
            var root = node;
            var scores = new List<Score<Tnode,Tedge>>();
            ScoresLookup = new Dictionary<Tnode, Score<Tnode,Tedge>>();
            var score = new Score<Tnode, Tedge>(0, /*huh*/0, node, default, default);
            ScoresLookup.Add(node, score);
            Priority = new PriorityQueue<Score<Tnode, Tedge>, int>();
            Priority.Enqueue(score, 0);
            History = new HashSet<Tnode>();

            // I don't think I need this, just pop the start on the priorty queue
            // param, return
            Func<List<List<string>>, List<string>> func = (results) =>
            {
                while (Priority.Count > 0)
                {
                    var currentScore = Priority.Dequeue();
                    var currentNode = currentScore.Node;
                    if (!History.Contains(currentNode))
                    {
                        History.Add(currentNode);
                        //maybe have a method here to find neighbours?

                    }
                }
                return null;
            };

            // function for evaluating each node in queue
            Func<Tnode, Func<int>, string> func2 = (currentNode, GetDistance) =>
            {
                History.Add(currentNode);
                var currentScore = ScoresLookup[currentNode];
                // this needs to be a function
                var neighbours = new List<(int distance, Tnode node, Tedge edge)>();
                foreach (var neigh in neighbours)
                {
                    if (!ScoresLookup.ContainsKey(neigh.node))
                    {
                        ScoresLookup.Add(neigh.node, new Score<Tnode, Tedge>(
                            currentScore.Distance,
                            0,
                            neigh.node,
                            currentNode,
                            neigh.edge));
                        continue;
                    }
                    var neighbourScore = ScoresLookup[neigh.node];
                    if (currentScore.Distance + neigh.distance < neighbourScore.Distance)
                    {
                        ScoresLookup[neigh.node].Distance = currentScore.Distance + neigh.distance;
                    }

                    //add to a list and return the list to add it to the queue
                }
                return null;
            };
                /*
            for s in seq:
            expanded_nodes.add(s.node)
            sd = s.dist
            for d, neigh, edge in neighbors(s.node):
                t = scores.get(neigh, None)
                if t and(t.dist <= sd + d):
                    # can't improve known shortest path to neighbor
                    continue
                t = scores[neigh] = Score(
                    sd + d, next(cnt), neigh, s.node, edge)
                hq.heappush(heap, t)
            };*/
        }

        internal PriorityQueue<Score<Tnode, Tedge>, int> Priority { get; set; }

        internal HashSet<Tnode> History { get; set; }

        internal Dictionary<Tnode, Score<Tnode, Tedge>> ScoresLookup { get; set; }

        /*
         *
        def expand_queue():
            # generates the items of the priority queue in order
            while heap:
                s = hq.heappop(heap)
                if s.node not in expanded_nodes:
                    # When the algorithm is pruned,
                    # this may mark some unexpanded nodes as
                    # optimal for the is_shortest() method.
                    # Must be done here because s can be suppressed
                    # by downstream filters
                    self.threshold = s.dist
                    yield s
        */
    }

    /*

    def __init__(
        self, node, neighbors, maxitems=10000, maxdist=None, target=None):
        """Create a Dijkstra instance from an initial node

        Arguments:
            * node: a hashable object representing the starting node
            * neighbors: a function to compute the neighbors of a node
                (more below).
            * maxitems = 10000: optional bound to limit the number
                of computation steps in the case of a very large
                or infinite graph.
            * maxdist = None: optionaly a maximal distance from the initial
                node where to stop the computation.
            * target = None: optional target node.The algorithm stops once
                a shortest path to this target has been found.


        The 'neighbors' argument must be a function of a single
        node argument, returning or generating a sequence
        of tuples with 3 elements
            (distance, node, edge)
        'distance' is a nonnegative number to a neigboring 'node', a
        hashable python object. 'edge' is an abitrary object that is
        not used by the algorithm but can be stored as client data
        in the result. This value can be None.

        Instantiating a Dijkstra instance runs immediately Dijkstra's
        algorithm to compute the shortest path from the initial node
        to the nodes discovered by successive calls of the
        neighbors() function.


        The instance itself is a dictionary that maps nodes to
        'Score' objects.These are namedtuples with fields
            * dist: the distance from the initial node to this node
            * index: a number indicating the rank of creation of this
                    Score object
            * node: the node in question
            * prev: the previous node in the shortest discovered path
            * edge: the data provided by the neighbors() function in the
                    transition from the previous node to this one

        Due to the 'maxitems', 'maxdist' and 'target' mechanisms used to
        optionally stop the algorithm, it may occur that the shortest
        distance to some nodes of the graph is shorter than the
        distance actually computed. A method is provided:


            self.is_shortest(node)

        returns True if the found distance is guaranteed to be the
        shortest distance from the initial node. Internally, there
        is a value self.threshold such that the distance is
        guaranteed to be optimal if and only if it is smaller or
        equal to the threshold.


        To retrieve paths from the initial node to a given node,
        a method is provided:


            self.rev_path_to(node)


        this method generates the sequence of nodes leading from
        the initial node to this node in reverse order. The Score
        objects stored in self can be used to obtain details about
        the path.


        Remark: node objects must all be different from None.
        """
        cnt = itt.count()
        self.root = node
        scores = self
        scores[node] = Score(0, next(cnt), node, None, None)
        # initialize a priority queue of Score instances
        heap = [scores[node]]
        expanded_nodes = set()

        def expand_queue():
            # generates the items of the priority queue in order
            while heap:
                s = hq.heappop(heap)
                if s.node not in expanded_nodes:
                    # When the algorithm is pruned,
                    # this may mark some unexpanded nodes as
                    # optimal for the is_shortest() method.
                    # Must be done here because s can be suppressed
                    # by downstream filters
                    self.threshold = s.dist
                    yield s

        seq = expand_queue()
        if maxitems is not None:
            seq = itt.islice(seq, 0, maxitems)
        if maxdist is not None:
            seq = itt.takewhile((lambda x: x.dist < maxdist), seq)
        if target is not None:
            seq = itt.takewhile((lambda x: x.node != target), seq)

        for s in seq:
            expanded_nodes.add(s.node)
            sd = s.dist
            for d, neigh, edge in neighbors(s.node):
                t = scores.get(neigh, None)
                if t and (t.dist <= sd + d):
                    # can't improve known shortest path to neighbor
                    continue
                t = scores[neigh] = Score(
                    sd + d, next(cnt), neigh, s.node, edge)
                hq.heappush(heap, t)

    def is_shortest(self, node):
        """Indicates if the shortest distance to the node is known"""
        return self[node].dist <= self.threshold

    def rev_path_to(self, node):
        """Generator of the shortest path found to the node.

        The path is generated in reverse order as a sequence of
        Score objects.
        """
        s = self[node]
        while s:
            yield s.node
            s = self.get(s.prev, None)

if __name__ == '__main__':
    """As an example, we generate a shortest path of transpositions
    leading from one permutation of 7 elements to another one.


    Of course this problem is very simple to solve by other means
    but it is interesting to see Dijkstra's algorithm find the
    solution blindly.
    """
    perm = (0, 1, 2, 3, 4, 5, 6) # a permutation

    def neighs(sigma): # generate neighbors by permuting 2 elements
        s = list(sigma)
        for j in range(1, len(sigma)):
            for i in range(j):
                s[i], s[j] = s[j], s[i]
                yield 1, tuple(s), (i, j)
                s[i], s[j] = s[j], s[i]

    # run the shortest path algorithm
    d = Dijkstra(perm, neighs, maxdist=6)

    target = (3, 5, 1, 2, 6, 4, 0)
    print(
        f"Distance to target {target} is known to be shortest?"
        f" {d.is_shortest(target)}")
    for k in d.rev_path_to(target):
        print(k, d[k].edge)*/
}
