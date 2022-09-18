using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph1{
    class Program{
        class Node<T> {
            public T name;
            public Dictionary<T, int> adjacents;
            public Node(T value){
                this.name = value;
                this.adjacents = new Dictionary<T, int>();
            }
            public void AddAdjacent(T name, int weight){
                if (!adjacents.ContainsKey(name)) adjacents.Add(name, weight);
            }
            public void RemoveAddAdjacent(T name){
                if (adjacents.ContainsKey(name)) adjacents.Remove(name);
            }
        }

        class Graph<T> {
            Dictionary<T, Node<T>> dict = new Dictionary<T, Node<T>>();
            public void AddNode(T name){
                if (!dict.ContainsKey(name)) dict.Add(name, new Node<T>(name));
            }
            public void AddEdge(T start, T end, int w){
                bool changed = dict.ContainsKey(start) && dict.ContainsKey(end);
                if (changed) {
                    dict[start].AddAdjacent(end, w);
                    dict[end].AddAdjacent(start, w);
                }
            }
            public void PrintGraph(){
                foreach (T key in dict.Keys) {
                    Console.Write($"{key} -> ");
                    foreach (T name in dict[key].adjacents.Keys)
                        Console.Write($"{name}({dict[key].adjacents[name]}) ");
                    Console.WriteLine();
                }
            }
            public void RemoveNode(T name){
                if (dict.ContainsKey(name)) 
                    foreach (T tmp in dict.Keys) 
                        if(dict[tmp].adjacents.ContainsKey(name)) 
                            dict[tmp].RemoveAddAdjacent(name);
                dict.Remove(name);
            }
            public void RemoveEdge(T start, T end){
                bool check = dict.ContainsKey(start) && dict.ContainsKey(end);
                if (check) {
                    dict[start].adjacents.Remove(end);
                    dict[end].adjacents.Remove(start);
                }
            }
        }



        static void Main(string[] args){
            Graph<string> g = new Graph<string>();
            // A ------- B
            // |    |    | \
            // |----E----|- F
            // |    |    | /
            // C ------- D
            string[] tmp = {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F"};
            foreach (string i in tmp) g.AddNode(i);
            string[] edges = {
                "A:B:1",
                "A:E:5",
                "B:E:7",
                "B:F:4",
                "B:D:3",
                "C:E:5",
                "C:D:9",
                "D:F:3",
                "E:F:2"};
            foreach (string i in edges){
                string[] tmp2 = i.Split(':');
                g.AddEdge(tmp2[0], tmp2[1], int.Parse(tmp2[2]));
            }
            g.PrintGraph();
        }

    }
}
